using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using RestTraining.Domain;

namespace RestTraining.Api.Models
{ 
    public class HotelRepository : IHotelRepository
    {
        RestTrainingApiContext context = new RestTrainingApiContext();

        public IQueryable<Hotel> All
        {
            get { return context.Hotels; }
        }

        public IQueryable<Hotel> AllIncluding(params Expression<Func<Hotel, object>>[] includeProperties)
        {
            IQueryable<Hotel> query = context.Hotels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Hotel Find(int id)
        {
            return context.Hotels.Find(id);
        }

        public void InsertOrUpdate(Hotel hotel)
        {
            context.PreInsertOrUpdateHotel(hotel);
            if (hotel.Id == default(int)) {
                // New entity
                context.Hotels.Add(hotel);
            } else {
                // Existing entity
                context.Entry(hotel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var hotel = context.Hotels.Find(id);
            context.Hotels.Remove(hotel);
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

    public interface IHotelRepository : IDisposable
    {
        IQueryable<Hotel> All { get; }
        IQueryable<Hotel> AllIncluding(params Expression<Func<Hotel, object>>[] includeProperties);
        Hotel Find(int id);
        void InsertOrUpdate(Hotel hotel);
        void Delete(int id);
        void Save();
    }
}