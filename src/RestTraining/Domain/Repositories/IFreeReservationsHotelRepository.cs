using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
    public interface IFreeReservationsHotelRepository : IDisposable
    {
        IQueryable<FreeReservationsHotel> All { get; }
        IQueryable<FreeReservationsHotel> AllIncluding(params Expression<Func<FreeReservationsHotel, object>>[] includeProperties);
        FreeReservationsHotel Find(int id);
        void InsertOrUpdate(FreeReservationsHotel freereservationshotel);
        void Delete(int id);
        void Save();
    }
}