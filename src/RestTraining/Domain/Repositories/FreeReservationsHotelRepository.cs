using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;
using RestTraining.Domain;

namespace RestTraining.Api.Domain.Repositories
{ 
    public class FreeReservationsHotelRepository : IFreeReservationsHotelRepository
    {
        readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IHotelNumbersUpdateService _hotelNumbersUpdateService;

        public FreeReservationsHotelRepository(IHotelNumbersUpdateService hotelNumbersUpdateService)
        {
            _hotelNumbersUpdateService = hotelNumbersUpdateService;
        }

        public IQueryable<FreeReservationsHotel> All
        {
            get
            {
                return _context.FreeReservationsHotels.
                    Include(x => x.HotelNumbers.Select(y => y.WindowViews)).
                    Include(x => x.HotelNumbers.Select(y => y.IncludeItems));
            }
        }

        public IQueryable<FreeReservationsHotel> AllIncluding(params Expression<Func<FreeReservationsHotel, object>>[] includeProperties)
        {
            IQueryable<FreeReservationsHotel> query = _context.FreeReservationsHotels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public FreeReservationsHotel Find(int id)
        {
            return _context.FreeReservationsHotels.Include(x => x.HotelNumbers).SingleOrDefault(x => x.Id == id);
        }

        public void InsertOrUpdate(FreeReservationsHotel freereservationshotel)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotel(_context, freereservationshotel); 
            if (freereservationshotel.Id == default(int))
            {
                _context.FreeReservationsHotels.Add(freereservationshotel);
            } else 
            {
                _context.Entry(freereservationshotel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var freereservationshotel = _context.FreeReservationsHotels.Find(id);
            _context.FreeReservationsHotels.Remove(freereservationshotel);
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