using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
    public interface IBoundedReservationsHotelRepository : IDisposable
    {
        IQueryable<BoundedReservationsHotel> All { get; }
        IQueryable<BoundedReservationsHotel> AllIncluding(params Expression<Func<BoundedReservationsHotel, object>>[] includeProperties);
        BoundedReservationsHotel Find(int id);
        void InsertOrUpdate(BoundedReservationsHotel boundedreservationshotel);
        void Delete(int id);
        void Save();
    }
}