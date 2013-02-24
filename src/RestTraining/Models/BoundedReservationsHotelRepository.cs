using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Domain;

namespace RestTraining.Api.Models
{ 
    public class BoundedReservationsHotelRepository : IBoundedReservationsHotelRepository
    {
        RestTrainingApiContext context = new RestTrainingApiContext();

        public IQueryable<BoundedReservationsHotel> All
        {
            get 
            {
                return context.BoundedReservationsHotels.
                    Include(x => x.HotelNumbers.Select(y => y.WindowViews)).
                    Include(x => x.HotelNumbers.Select(y => y.IncludeItems)).
                    Include(x => x.BoundedPeriods);
            }
        }

        public IQueryable<BoundedReservationsHotel> AllIncluding(params Expression<Func<BoundedReservationsHotel, object>>[] includeProperties)
        {
            IQueryable<BoundedReservationsHotel> query = context.BoundedReservationsHotels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoundedReservationsHotel Find(int id)
        {
            return context.BoundedReservationsHotels.Find(id);
        }

        public void InsertOrUpdate(BoundedReservationsHotel boundedreservationshotel)
        {
            context.PreInsertOrUpdateHotel(boundedreservationshotel);
            if (boundedreservationshotel.Id == default(int))
            {
                // New entity
                context.BoundedReservationsHotels.Add(boundedreservationshotel);
            } else {
                // Existing entity
                context.Entry(boundedreservationshotel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var boundedreservationshotel = context.BoundedReservationsHotels.Find(id);
            context.BoundedReservationsHotels.Remove(boundedreservationshotel);
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

    public interface IBoundedReservationsHotelRepository : IDisposable
    {
        IQueryable<BoundedReservationsHotel> All { get; }
        IQueryable<BoundedReservationsHotel> AllIncluding(params Expression<Func<BoundedReservationsHotel, object>>[] includeProperties);
        BoundedReservationsHotel Find(int id);
        void InsertOrUpdate(BoundedReservationsHotel boundedreservationshotel);
        void Delete(int id);
        void Save();
    }
}