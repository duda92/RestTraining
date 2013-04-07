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
    public class BoundedBookingRepository : IBoundedBookingRepository
    {
        private readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IBookingDatesService _bookingDatesService;

        public BoundedBookingRepository(IBookingDatesService bookingDatesService)
        {
            _bookingDatesService = bookingDatesService;
        }

        public IQueryable<BoundedBooking> All
        {
            get { return _context.BoundedBookings; }
        }
        public IQueryable<BoundedBooking> AllIncluding(params Expression<Func<BoundedBooking, object>>[] includeProperties)
        {
            IQueryable<BoundedBooking> query = _context.BoundedBookings;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoundedBooking Find(int id)
        {
            return _context.BoundedBookings.Find(id);
        }

        public void InsertOrUpdate(BoundedBooking boundedBooking)
        {
            if (boundedBooking.Id == default(int))
            {
                PreInsertCheck(boundedBooking);
            }
            else
            {
                PreUpdateCheck(boundedBooking);
            } 
            if (!_bookingDatesService.IsBoundedBookingValid(_context, boundedBooking))
            {
                throw new InvalidDatesBookingException();
            }
            var bp = _context.BoundedPeriods.First(x => x.Id == boundedBooking.BoundedPeriod.Id);
            boundedBooking.BoundedPeriod = bp;

            UpdateClient(boundedBooking);

            if (boundedBooking.Id == default(int))
            {
                //_context.BoundedBookings.Add(boundedBooking);
                _context.InsertBoundedBooking(boundedBooking);
            }
            else
            {
                _context.UpdateBoundedBooking(boundedBooking);
            }
        }

        private void UpdateClient(BoundedBooking boundedBooking)
        {
            if (boundedBooking.Id == 0) return;
            var prevBooking = _context.BoundedBookings.Find(boundedBooking.Id);
            var clientId = prevBooking.ClientId;
            _context.Entry(prevBooking).State = EntityState.Detached;
            boundedBooking.ClientId = clientId;
        }

        public void Delete(int id)
        {
            var boundedBooking = _context.BoundedBookings.Include(x => x.Client).Include(x => x.BoundedPeriod).FirstOrDefault(x => x.Id == id);
            if (boundedBooking == null)
            {
                throw new ParameterNotFoundException("Id");
            } 
            _context.BoundedBookings.Remove(boundedBooking);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private void PreInsertCheck(BoundedBooking boundedBooking)
        {
            var boundedHotel = _context.BoundedReservationsHotels.Find(boundedBooking.HotelId);
            if (boundedHotel == null)
                throw new ParameterNotFoundException("HotelId");
        }

        private void PreUpdateCheck(BoundedBooking boundedBooking)
        {
            var boundedHotel = _context.BoundedReservationsHotels.Find(boundedBooking.HotelId);
            if (boundedHotel == null)
                throw new ParameterNotFoundException("HotelId"); 
            var boundedPeriod = _context.BoundedPeriods.Find(boundedBooking.Id);
            if (boundedPeriod == null)
                throw new ParameterNotFoundException("BoundedPeriodId");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}