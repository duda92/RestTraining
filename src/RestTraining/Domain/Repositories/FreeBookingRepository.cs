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
            if (!_bookingDatesService.IsFreeBookingValid(_context, freeBooking))
            {
                throw new InvalidBookingException();
            }
            if (freeBooking.Id == default(int))
            {
                _context.FreeBookings.Add(freeBooking);
            }
            else
            {
                _context.Entry(freeBooking).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var boundedPeriod = _context.BoundedPeriods.Find(id);
            _context.BoundedPeriods.Remove(boundedPeriod);
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