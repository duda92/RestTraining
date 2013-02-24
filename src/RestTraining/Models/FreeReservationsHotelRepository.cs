using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Domain;

namespace RestTraining.Api.Models
{ 
    public class FreeReservationsHotelRepository : IFreeReservationsHotelRepository
    {
        readonly RestTrainingApiContext context = new RestTrainingApiContext();

        public IQueryable<FreeReservationsHotel> All
        {
            get
            {
                return context.FreeReservationsHotels.
                    Include(x => x.HotelNumbers.Select(y => y.WindowViews)).
                    Include(x => x.HotelNumbers.Select(y => y.IncludeItems));
            }
        }

        public IQueryable<FreeReservationsHotel> AllIncluding(params Expression<Func<FreeReservationsHotel, object>>[] includeProperties)
        {
            IQueryable<FreeReservationsHotel> query = context.FreeReservationsHotels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public FreeReservationsHotel Find(int id)
        {
            return context.FreeReservationsHotels.Include(x => x.HotelNumbers).SingleOrDefault(x => x.Id == id);
        }

        public void InsertOrUpdate(FreeReservationsHotel freereservationshotel)
        {
            context.PreInsertOrUpdateHotel(freereservationshotel); 
            if (freereservationshotel.Id == default(int)) {
                // New entity
                context.FreeReservationsHotels.Add(freereservationshotel);
            } else {
                // Existing entity
                context.Entry(freereservationshotel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var freereservationshotel = context.FreeReservationsHotels.Find(id);
            context.FreeReservationsHotels.Remove(freereservationshotel);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IFreeReservationsHotelRepository : IDisposable
    {
        IQueryable<FreeReservationsHotel> All { get; }
        IQueryable<FreeReservationsHotel> AllIncluding(params Expression<Func<FreeReservationsHotel, object>>[] includeProperties);
        FreeReservationsHotel Find(int id);
        void InsertOrUpdate(FreeReservationsHotel freereservationshotel);
        void Delete(int id);
        void Save();
    }
}