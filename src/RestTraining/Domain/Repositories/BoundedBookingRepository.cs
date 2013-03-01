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
            if (!_bookingDatesService.IsBoundedBookingValid(_context, boundedBooking))
            {
                throw new InvalidDatesBookingException();
            }
            if (boundedBooking.Id == default(int))
            {
                _context.BoundedBookings.Add(boundedBooking);
            }
            else
            {
                _context.Entry(boundedBooking).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var boundedBooking = _context.BoundedBookings.Find(id);
            _context.BoundedBookings.Remove(boundedBooking);
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