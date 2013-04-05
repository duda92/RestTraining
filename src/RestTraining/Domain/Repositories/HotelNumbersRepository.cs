﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;

namespace RestTraining.Api.Domain.Repositories
{
    public class HotelNumbersRepository : IHotelNumbersRepository
    {
        readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IHotelNumbersUpdateService _hotelNumbersUpdateService;

        public HotelNumbersRepository(IHotelNumbersUpdateService hotelNumbersUpdateService)
        {
            _hotelNumbersUpdateService = hotelNumbersUpdateService;
        }

        public IQueryable<HotelNumber> All
        {
            get
            {
                return _context.HotelNumbers.Include(x => x.IncludeItems).
                    Include(x => x.WindowViews);
            }
        }

        public IQueryable<HotelNumber> AllIncluding(params Expression<Func<HotelNumber, object>>[] includeProperties)
        {
            IQueryable<HotelNumber> query = _context.HotelNumbers;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public HotelNumber Find(int id)
        {
            var hotelNumber = _context.HotelNumbers.Include(x => x.IncludeItems).
                Include(x => x.WindowViews).SingleOrDefault(x => x.Id == id);
            if (hotelNumber != null)
            {
                
            }
            return hotelNumber;
        }

        public void InsertOrUpdate(HotelNumber hotelNumber)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotelNumber(_context, hotelNumber);
            if (hotelNumber.Id == default(int))
            {
                _context.HotelNumbers.Add(hotelNumber);
            }
            else
            {
                UpdateWithNestedCollections(hotelNumber);
            }
        }

        public void InsertOrUpdate(HotelNumber hotelNumber, int hotelId)
        {
            _hotelNumbersUpdateService.PreInsertOrUpdateHotelNumber(_context, hotelNumber);
            if (hotelNumber.Id == default(int))
            {
                hotelNumber.HotelId = hotelId;
                _context.HotelNumbers.Add(hotelNumber);
            }
            else
            {
                UpdateWithNestedCollections(hotelNumber);
            }
        }

        public void Delete(int id)
        {
            var hotelNumber = _context.HotelNumbers.Find(id);
            _context.HotelNumbers.Remove(hotelNumber);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public virtual void UpdateWithNestedCollections(HotelNumber hotelNumber)
        {
            _context.Entry(hotelNumber).State = EntityState.Detached;
            UpdateIncludedItems(hotelNumber);
            UpdateWindowViews(hotelNumber);
            UpdateHotelNumber(hotelNumber);
        }

        public virtual void UpdateIncludedItems(HotelNumber hotelNumber)
        {
            var itemsToRemove = _context.IncludeItems.Where(x => x.HotelNumberId == hotelNumber.Id).ToList();
            var prev = _context.HotelNumbers.First(x => x.Id == hotelNumber.Id);
            foreach (var includedItem in itemsToRemove)
            {
                prev.IncludeItems.Remove(includedItem);
            }
            foreach (var includedItem in itemsToRemove)
            {
                _context.IncludeItems.Remove(includedItem);
            }
            foreach (var includedItem in hotelNumber.IncludeItems)
            {
                includedItem.Id = 0;
                prev.IncludeItems.Add(includedItem);
            }
        }
        
        public virtual void UpdateHotelNumber(HotelNumber hotelNumber)
        {
            var prev = _context.HotelNumbers.First(x => x.Id == hotelNumber.Id);
            prev.HotelNumberType = hotelNumber.HotelNumberType;
        }

        public virtual void UpdateWindowViews(HotelNumber hotelNumber)
        {
            var newViews = _context.
                WindowViews.ToList().
                Where(wv => hotelNumber.WindowViews.Any(x => x.Type == wv.Type)).ToList();
                  
            var prev = _context.HotelNumbers.Include(x => x.WindowViews).First(x => x.Id == hotelNumber.Id);
            prev.WindowViews.Clear();
            newViews.ForEach(prev.WindowViews.Add);
        }
    }
}



//_context.Entry(hotelNumber).State = EntityState.Modified;

//_context.Entry(hotelNumber).CurrentValues.SetValues(hotelNumber);

//var hotelNumber_ = _context.HotelNumbers.Include(x => x.WindowViews).FirstOrDefault(u => u.Id == hotelNumber.Id);
//hotelNumber_.WindowViews.RemoveAll(x => hotelNumber.WindowViews.Single(y => y.Type == x.Type) != null);
//hotelNumber_.WindowViews.AddRange(x => hotelNumber.WindowViews.Single(y => y.Type == x.Type) != null);

//var previousHotelNumber =
//    _context.HotelNumbers.Include(x => x.IncludeItems)
//           .Include(x => x.WindowViews)
//           .Single(x => x.Id == hotelNumber.Id);
//while (previousHotelNumber.IncludeItems.Any(p => !hotelNumber.IncludeItems.Any(up => up.Id == p.Id && p.Id != 0)))
//    for (int i = 0; i < previousHotelNumber.IncludeItems.Count; i++)
//    {
//        var includedItem = previousHotelNumber.IncludeItems[i];
//        if (hotelNumber.IncludeItems.All(p => p.Id != includedItem.Id))
//            previousHotelNumber.IncludeItems.Remove(includedItem);
//    }

//foreach (var includedItem in previousHotelNumber.IncludeItems)
//{
//    if (includedItem.Id != default(int))
//    {
//        var includedItem1 = includedItem;
//        _context.Entry(includedItem)
//               .CurrentValues.SetValues(hotelNumber.IncludeItems.SingleOrDefault(p => p.Id == includedItem1.Id));
//    }
//}
//for (int i = 0; i < hotelNumber.IncludeItems.Count; i++)
//{
//    var includedItem = hotelNumber.IncludeItems[i];
//    if (includedItem.Id == default(int))
//        previousHotelNumber.IncludeItems.Add(includedItem);
//}

//previousHotelNumber.WindowViews.Clear();
//previousHotelNumber.WindowViews = hotelNumber.WindowViews;