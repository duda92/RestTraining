using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;

namespace RestTraining.Api.Domain.Repositories
{ 
    public class HotelRepository : IHotelRepository
    {
        readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private IHotelNumbersUpdateService _hotelNumbersUpdateService;

        public HotelRepository(IHotelNumbersUpdateService hotelNumbersUpdateService)
        {
            _hotelNumbersUpdateService = hotelNumbersUpdateService;
        }

        public IQueryable<Hotel> All
        {
            get { return _context.Hotels; }
        }

        public IQueryable<Hotel> AllIncluding(params Expression<Func<Hotel, object>>[] includeProperties)
        {
            IQueryable<Hotel> query = _context.Hotels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Hotel Find(int id)
        {
            return _context.Hotels.Find(id);
        }

        public void InsertOrUpdate(Hotel hotel)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotel(_context, hotel);
            if (hotel.Id == default(int)) 
            {
                _context.Hotels.Add(hotel);
            }
            else
            {
                _context.Entry(hotel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var hotel = _context.Hotels.Find(id);
            _context.Hotels.Remove(hotel);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }
}