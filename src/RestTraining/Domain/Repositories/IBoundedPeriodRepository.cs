using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
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