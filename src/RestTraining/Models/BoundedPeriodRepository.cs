using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestTraining.Api.Models
{
    public class BoundedPeriodRepository : IBoundedPeriodRepository
    {
        RestTrainingApiContext context = new RestTrainingApiContext();

        public IQueryable<BoundedPeriod> All
        {
            get
            {
                return context.BoundedPeriods;
            }
        }

        public IQueryable<BoundedPeriod> AllIncluding(params Expression<Func<BoundedPeriod, object>>[] includeProperties)
        {
            IQueryable<BoundedPeriod> query = context.BoundedPeriods;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoundedPeriod Find(int id)
        {
            return context.BoundedPeriods.Find(id);
        }

        public void InsertOrUpdate(BoundedPeriod boundedPeriod)
        {
            if (!IsBoundedPeriodValid(boundedPeriod))
            {
                throw new ArgumentException("This period intersectы with another one already existed", "BeginDate, EndDate");
            }
            if (boundedPeriod.Id == default(int))
            {
                context.BoundedPeriods.Add(boundedPeriod);
            }
            else
            {
                context.Entry(boundedPeriod).State = EntityState.Modified;
            }
        }

        private bool IsBoundedPeriodValid(BoundedPeriod boundedPeriod)
        {
            var existingPeriods = context.BoundedPeriods.Where(
                x => x.BoundedReservationsHotelId == boundedPeriod.BoundedReservationsHotelId);
            var isBoundedPeriodValid =
                !existingPeriods.Any(
                    x => (x.BeginDate <= boundedPeriod.BeginDate && x.EndDate >= boundedPeriod.BeginDate) ||
                         (x.BeginDate <= boundedPeriod.EndDate && x.EndDate >= boundedPeriod.EndDate));
            return isBoundedPeriodValid;
        }

        public void Delete(int id)
        {
            var boundedPeriod = context.BoundedPeriods.Find(id);
            context.BoundedPeriods.Remove(boundedPeriod);
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

    public interface IBoundedPeriodRepository : IDisposable
    {
        IQueryable<BoundedPeriod> All { get; }
        IQueryable<BoundedPeriod> AllIncluding(params Expression<Func<BoundedPeriod, object>>[] includeProperties);
        BoundedPeriod Find(int id);
        void InsertOrUpdate(BoundedPeriod boundedreservationshotel);
        void Delete(int id);
        void Save();
    }
}