using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;

namespace RestTraining.Api.Domain.Repositories
{ 
    public class BoundedReservationsHotelRepository : IBoundedReservationsHotelRepository
    {
        readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IHotelNumbersUpdateService _hotelNumbersUpdateService;

        public BoundedReservationsHotelRepository(IHotelNumbersUpdateService hotelNumbersUpdateService)
        {
            _hotelNumbersUpdateService = hotelNumbersUpdateService;
        }

        public IQueryable<BoundedReservationsHotel> All
        {
            get 
            {
                return _context.BoundedReservationsHotels.
                    Include(x => x.HotelNumbers.Select(y => y.WindowViews)).
                    Include(x => x.HotelNumbers.Select(y => y.IncludeItems)).
                    Include(x => x.BoundedPeriods);
            }
        }

        public IQueryable<BoundedReservationsHotel> AllIncluding(params Expression<Func<BoundedReservationsHotel, object>>[] includeProperties)
        {
            IQueryable<BoundedReservationsHotel> query = _context.BoundedReservationsHotels;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoundedReservationsHotel Find(int id)
        {
            return _context.BoundedReservationsHotels.Find(id);
        }

        public void InsertOrUpdate(BoundedReservationsHotel boundedreservationshotel)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotel(_context, boundedreservationshotel);
            if (boundedreservationshotel.Id == default(int))
            {
                _context.BoundedReservationsHotels.Add(boundedreservationshotel);
            } 
            else
            {
                if (boundedreservationshotel.HotelNumbers.Count != 0)
                    boundedreservationshotel.HotelNumbers.Clear(); // put hotel does not affect hotel numbers, only on create
                _context.Entry(boundedreservationshotel).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var boundedreservationshotel = _context.BoundedReservationsHotels.Find(id);
            _context.BoundedReservationsHotels.Remove(boundedreservationshotel);
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