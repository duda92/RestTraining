using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
    public interface IHotelRepository : IDisposable
    {
        IQueryable<Hotel> All { get; }
        IQueryable<Hotel> AllIncluding(params Expression<Func<Hotel, object>>[] includeProperties);
        Hotel Find(int id);
        void InsertOrUpdate(Hotel hotel);
        void Delete(int id);
        void Save();
    }
}