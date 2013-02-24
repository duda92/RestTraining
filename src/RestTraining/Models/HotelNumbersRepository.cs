using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Domain;

namespace RestTraining.Api.Models
{
    public class HotelNumbersRepository : IHotelNumbersRepository
    {
        readonly RestTrainingApiContext context = new RestTrainingApiContext();

        public IQueryable<HotelNumber> All
        {
            get
            {
                return context.HotelNumbers.Include(x => x.IncludeItems).
                    Include(x => x.WindowViews);
            }
        }

        public IQueryable<HotelNumber> AllIncluding(params Expression<Func<HotelNumber, object>>[] includeProperties)
        {
            IQueryable<HotelNumber> query = context.HotelNumbers;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public HotelNumber Find(int id)
        {
            var hotelNumber = context.HotelNumbers.Include(x => x.IncludeItems).
                Include(x => x.WindowViews).SingleOrDefault(x => x.Id == id);
            if (hotelNumber != null)
            {
                
            }
            return hotelNumber;
        }

        public void InsertOrUpdate(HotelNumber hotelNumber)
        {
            context.PreInsertOrUpdateHotelNumber(hotelNumber);
            if (hotelNumber.Id == default(int))
            {
                context.HotelNumbers.Add(hotelNumber);
            }
            else
            {
                UpdateWithNestedCollections(hotelNumber);
            }
        }

        public void InsertOrUpdate(HotelNumber hotelNumber, int hotelId)
        {
            context.PreInsertOrUpdateHotelNumber(hotelNumber);
            if (hotelNumber.Id == default(int))
            {
                hotelNumber.HotelId = hotelId;
                context.HotelNumbers.Add(hotelNumber);
            }
            else
            {
                UpdateWithNestedCollections(hotelNumber);
            }
        }

        public void Delete(int id)
        {
            var hotelNumber = context.HotelNumbers.Find(id);
            context.HotelNumbers.Remove(hotelNumber);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        private void UpdateWithNestedCollections(HotelNumber hotelNumber)
        {
            var previousHotelNumber =
                context.HotelNumbers.Include(x => x.IncludeItems)
                       .Include(x => x.WindowViews)
                       .Single(x => x.Id == hotelNumber.Id);
            while (previousHotelNumber.IncludeItems.Any(p => !hotelNumber.IncludeItems.Any(up => up.Id == p.Id && p.Id != 0)))
                for (int i = 0; i < previousHotelNumber.IncludeItems.Count; i++)
                {
                    var includedItem = previousHotelNumber.IncludeItems[i];
                    if (hotelNumber.IncludeItems.All(p => p.Id != includedItem.Id))
                        previousHotelNumber.IncludeItems.Remove(includedItem);
                }

            foreach (var includedItem in previousHotelNumber.IncludeItems)
            {
                if (includedItem.Id != default(int))
                {
                    var includedItem1 = includedItem;
                    context.Entry(includedItem)
                           .CurrentValues.SetValues(hotelNumber.IncludeItems.SingleOrDefault(p => p.Id == includedItem1.Id));
                }
            }
            for (int i = 0; i < hotelNumber.IncludeItems.Count; i++)
            {
                var includedItem = hotelNumber.IncludeItems[i];
                if (includedItem.Id == default(int))
                    previousHotelNumber.IncludeItems.Add(includedItem);
            }

            previousHotelNumber.WindowViews.Clear();
            previousHotelNumber.WindowViews = hotelNumber.WindowViews;

            context.Entry(previousHotelNumber).State = EntityState.Modified;
        }

    }


    public interface IHotelNumbersRepository : IDisposable
    {
        IQueryable<HotelNumber> All { get; }
        IQueryable<HotelNumber> AllIncluding(params Expression<Func<HotelNumber, object>>[] includeProperties);
        HotelNumber Find(int id);
        void InsertOrUpdate(HotelNumber hotelNumber);
        void InsertOrUpdate(HotelNumber hotelNumber, int hotelId); 
        void Delete(int id);
        void Save();
    }
}