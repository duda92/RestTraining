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