using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
    public interface IBoundedBookingRepository : IDisposable
    {
        IQueryable<BoundedBooking> All { get; }
        IQueryable<BoundedBooking> AllIncluding(params Expression<Func<BoundedBooking, object>>[] includeProperties);
        BoundedBooking Find(int id);
        void InsertOrUpdate(BoundedBooking boundedreservationshotel);
        void Delete(int id);
        void Save();
    }
}