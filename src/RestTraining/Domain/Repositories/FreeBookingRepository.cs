using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api.Domain.Repositories
{
    public class FreeBookingRepository : IFreeBookingRepository
    {
        private readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IBookingDatesService _bookingDatesService;

        public FreeBookingRepository(IBookingDatesService bookingDatesService)
        {
            _bookingDatesService = bookingDatesService;
        }

        public IQueryable<FreeBooking> All
        {
            get
            {
                return _context.FreeBookings;
            }
        }

        public IQueryable<FreeBooking> AllIncluding(params Expression<Func<FreeBooking, object>>[] includeProperties)
        {
            IQueryable<FreeBooking> query = _context.FreeBookings;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public FreeBooking Find(int id)
        {
            return _context.FreeBookings.Find(id);
        }

        public void InsertOrUpdate(FreeBooking freeBooking)
        {
            var hotel = _context.FreeReservationsHotels.SingleOrDefault(x => x.Id == freeBooking.HotelId);
            var hotelNumber = _context.HotelNumbers.SingleOrDefault(x => x.Id == freeBooking.HotelNumberId && x.HotelId == freeBooking.HotelId);
            if (hotel == null || hotelNumber == null)
            {
                throw new ParameterNotFoundException();
            }
            if (!_bookingDatesService.IsFreeBookingValid(_context, freeBooking))
            {
                throw new InvalidDatesBookingException();
            }
            if (freeBooking.Id == default(int))
            {
                _context.FreeBookings.Add(freeBooking);
            }
            else
            {
                var previousBooking = _context.FreeBookings.Include(x => x.Client).FirstOrDefault(x => x.Id == freeBooking.Id);
                previousBooking.Client.Name = freeBooking.Client.Name;
                previousBooking.Client.PhoneNumber = freeBooking.Client.PhoneNumber;
                previousBooking.BeginDate = freeBooking.BeginDate;
                previousBooking.EndDate = freeBooking.EndDate;
                _context.Entry(previousBooking).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var freeBooking = _context.FreeBookings.Find(id);
            _context.FreeBookings.Remove(freeBooking);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Exist(int id)
        {
            var freeBooking = _context.FreeBookings.Find(id);
            var exists = freeBooking != null;
            if (exists)
                _context.Entry(freeBooking).State = EntityState.Detached;
            return exists;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}