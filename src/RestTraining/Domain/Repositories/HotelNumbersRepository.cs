using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;
using RestTraining.Common.DTO;

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

        public List<HotelNumber> GetByQuery(HotelNumbersSearchQuery query)
        {
            var all = All;

            var filteredByIncludeItems = new List<HotelNumber>();
            foreach (var hotelNumber in all)
            {
                var isSuitable = true;
                foreach(var queryIncludeItem in query.IncludeItems)
                {
                    if (!hotelNumber.IncludeItems.Any(x => x.Count >= queryIncludeItem.Count && x.IncludeItemType == queryIncludeItem.IncludeItemType.ToEntity()))
                    {
                        isSuitable = false;
                        break;
                    }
                }
                if (isSuitable && !filteredByIncludeItems.Any(x => x.Id == hotelNumber.Id))
                {
                    filteredByIncludeItems.Add(hotelNumber);
                }
            }

            var filteredByWindowViews = new List<HotelNumber>();
            foreach (var hotelNumber in all)
            {
                var isSuitable = true;
                foreach (var queryWindowView in query.WindowViews)
                {
                    if (!hotelNumber.WindowViews.Any(x => x.Type == queryWindowView.ToEntity()))
                    {
                        isSuitable = false;
                        break;
                    }
                }
                if (isSuitable && !filteredByWindowViews.Any(x => x.Id == hotelNumber.Id))
                {
                    filteredByWindowViews.Add(hotelNumber);
                }
            }

            var filteredByHotelsAttractions = new List<HotelNumber>();
            foreach (var hotelNumber in all)
            {
                var hotel = _context.Hotels.Include(y => y.HotelsAttractions).Single(x => x.Id == hotelNumber.HotelId);
                var isSuitable = true;
                foreach (var queryHotelsAttraction in query.HotelsAttractions)
                {
                    if (!hotel.HotelsAttractions.Any(x => x.Count >= queryHotelsAttraction.Count && x.HotelsAttractionType == queryHotelsAttraction.HotelsAttractionType.ToEntity()))
                    {
                        isSuitable = false;
                        break;
                    }
                }
                if (isSuitable && !filteredByHotelsAttractions.Any(x => x.Id == hotelNumber.Id))
                {
                    filteredByHotelsAttractions.Add(hotelNumber);
                }
            }

            var result = from q1 in filteredByIncludeItems
                        join q2 in filteredByWindowViews on q1.Id equals q2.Id
                        join q3 in filteredByHotelsAttractions on q2.Id equals q3.Id
                        select new HotelNumber { Id = q1.Id, HotelId = q1.HotelId, HotelNumberType = q1.HotelNumberType, IncludeItems = q1.IncludeItems, WindowViews = q1.WindowViews };
            return result.ToList();
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