﻿using System;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Repositories
{
    public interface IHotelNumbersRepository : IDisposable
    {
        IQueryable<HotelNumber> All { get; }
        IQueryable<HotelNumber> AllIncluding(params Expression<Func<HotelNumber, object>>[] includeProperties);
        HotelNumber Find(int id);
        void InsertOrUpdate(HotelNumber hotelNumber);
        void InsertOrUpdate(HotelNumber hotelNumber, int hotelId);
        void Delete(int id);
        void Save();
    }
}