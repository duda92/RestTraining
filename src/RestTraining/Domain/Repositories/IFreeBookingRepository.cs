using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
    public interface IFreeBookingRepository : IDisposable
    {
        IQueryable<FreeBooking> All { get; }
        IQueryable<FreeBooking> AllIncluding(params Expression<Func<FreeBooking, object>>[] includeProperties);
        FreeBooking Find(int id);
        void InsertOrUpdate(FreeBooking freeBooking);
        void Delete(int id);
        void Save();
    }
}