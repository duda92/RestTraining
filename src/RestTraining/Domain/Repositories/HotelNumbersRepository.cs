using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;

namespace RestTraining.Api.Domain.Repositories
{
    public class HotelNumbersRepository : IHotelNumbersRepository
    {
        readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private IHotelNumbersUpdateService _hotelNumbersUpdateService;

        public HotelNumbersRepository(IHotelNumbersUpdateService hotelNumbersUpdateService)
        {
            _hotelNumbersUpdateService = hotelNumbersUpdateService;
        }

        public IQueryable<HotelNumber> All
        {
            get
            {
                return _context.HotelNumbers.Include(x => x.IncludeItems).
                    Include(x => x.WindowViews);
            }
        }

        public IQueryable<HotelNumber> AllIncluding(params Expression<Func<HotelNumber, object>>[] includeProperties)
        {
            IQueryable<HotelNumber> query = _context.HotelNumbers;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public HotelNumber Find(int id)
        {
            var hotelNumber = _context.HotelNumbers.Include(x => x.IncludeItems).
                Include(x => x.WindowViews).SingleOrDefault(x => x.Id == id);
            if (hotelNumber != null)
            {
                
            }
            return hotelNumber;
        }

        public void InsertOrUpdate(HotelNumber hotelNumber)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotelNumber(_context, hotelNumber);
            if (hotelNumber.Id == default(int))
            {
                _context.HotelNumbers.Add(hotelNumber);
            }
            else
            {
                UpdateWithNestedCollections(hotelNumber);
            }
        }

        public void InsertOrUpdate(HotelNumber hotelNumber, int hotelId)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotelNumber(_context, hotelNumber);
            if (hotelNumber.Id == default(int))
            {
                hotelNumber.HotelId = hotelId;
                _context.HotelNumbers.Add(hotelNumber);
            }
            else
            {
                UpdateWithNestedCollections(hotelNumber);
            }
        }

        public void Delete(int id)
        {
            var hotelNumber = _context.HotelNumbers.Find(id);
            _context.HotelNumbers.Remove(hotelNumber);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private void UpdateWithNestedCollections(HotelNumber hotelNumber)
        {
            var previousIncludedItems =
                _context.HotelNumbers.Include(x => x.IncludeItems).Single(x => x.Id == hotelNumber.Id).IncludeItems.ToList();
            foreach (var previousIncludedItem in previousIncludedItems)
            {
                _context.Entry(previousIncludedItem).State = EntityState.Deleted;
            }
            _context.SaveChanges();

            var previous = _context.HotelNumbers.Single(x => x.Id == hotelNumber.Id);
            foreach (var vw in previous.WindowViews)
            {
                previous.WindowViews.Remove(_context.WindowViews.Single(y => vw.Type == y.Type));
            }
            _context.SaveChanges(); 
            
            _context.Entry(_context.HotelNumbers.Single(x => x.Id == hotelNumber.Id)).State = EntityState.Detached;

            foreach (var includedItem in hotelNumber.IncludeItems)
            {
                _context.IncludeItems.Add(includedItem);
            }

            _context.HotelNumbers.Attach(hotelNumber);
            //_context.Entry(hotelNumber).CurrentValues.SetValues(hotelNumber);

            //var hotelNumber_ = _context.HotelNumbers.Include(x => x.WindowViews).FirstOrDefault(u => u.Id == hotelNumber.Id);
            //hotelNumber_.WindowViews.RemoveAll(x => hotelNumber.WindowViews.Single(y => y.Type == x.Type) != null);
            //hotelNumber_.WindowViews.AddRange(x => hotelNumber.WindowViews.Single(y => y.Type == x.Type) != null);

            //var previousHotelNumber =
            //    _context.HotelNumbers.Include(x => x.IncludeItems)
            //           .Include(x => x.WindowViews)
            //           .Single(x => x.Id == hotelNumber.Id);
            //while (previousHotelNumber.IncludeItems.Any(p => !hotelNumber.IncludeItems.Any(up => up.Id == p.Id && p.Id != 0)))
            //    for (int i = 0; i < previousHotelNumber.IncludeItems.Count; i++)
            //    {
            //        var includedItem = previousHotelNumber.IncludeItems[i];
            //        if (hotelNumber.IncludeItems.All(p => p.Id != includedItem.Id))
            //            previousHotelNumber.IncludeItems.Remove(includedItem);
            //    }

            //foreach (var includedItem in previousHotelNumber.IncludeItems)
            //{
            //    if (includedItem.Id != default(int))
            //    {
            //        var includedItem1 = includedItem;
            //        _context.Entry(includedItem)
            //               .CurrentValues.SetValues(hotelNumber.IncludeItems.SingleOrDefault(p => p.Id == includedItem1.Id));
            //    }
            //}
            //for (int i = 0; i < hotelNumber.IncludeItems.Count; i++)
            //{
            //    var includedItem = hotelNumber.IncludeItems[i];
            //    if (includedItem.Id == default(int))
            //        previousHotelNumber.IncludeItems.Add(includedItem);
            //}

            //previousHotelNumber.WindowViews.Clear();
            //previousHotelNumber.WindowViews = hotelNumber.WindowViews;
        }
    }
}