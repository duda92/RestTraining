using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;

namespace RestTraining.Api.Domain.Repositories
{
    public class BoundedPeriodRepository : IBoundedPeriodRepository
    {
        private readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IBookingDatesService _bookingDatesService;
        
        public BoundedPeriodRepository(IBookingDatesService bookingDatesService)
        {
            _bookingDatesService = bookingDatesService;
        }

        public IQueryable<BoundedPeriod> All
        {
            get
            {
                return _context.BoundedPeriods;
            }
        }

        public IQueryable<BoundedPeriod> AllIncluding(params Expression<Func<BoundedPeriod, object>>[] includeProperties)
        {
            IQueryable<BoundedPeriod> query = _context.BoundedPeriods;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoundedPeriod Find(int id)
        {
            return _context.BoundedPeriods.Find(id);
        }

        public void InsertOrUpdate(BoundedPeriod boundedPeriod)
        {
            if (!_bookingDatesService.IsBoundedPeriodValid(_context, boundedPeriod))
            {
                throw new ArgumentException("This period intersectы with another one already existed", "BeginDate, EndDate");
            }
            if (boundedPeriod.Id == default(int))
            {
                _context.BoundedPeriods.Add(boundedPeriod);
            }
            else
            {
                _context.Entry(boundedPeriod).State = EntityState.Modified;
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