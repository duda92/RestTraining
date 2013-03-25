using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Services;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api.Domain.Repositories
{
    public class BoundedPeriodRepository : IBoundedPeriodRepository
    {
        private readonly RestTrainingApiContext _context = new RestTrainingApiContext();
        private readonly IBookingDatesService _bookingDatesService;
        
        public BoundedPeriodRepository(IBookingDatesService bookingDatesService)
        {
            _bookingDatesService = bookingDatesService;
        }

        public IQueryable<BoundedPeriod> All
        {
            get
            {
                return _context.BoundedPeriods;
            }
        }

        public IQueryable<BoundedPeriod> AllIncluding(params Expression<Func<BoundedPeriod, object>>[] includeProperties)
        {
            IQueryable<BoundedPeriod> query = _context.BoundedPeriods;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BoundedPeriod Find(int id)
        {
            return _context.BoundedPeriods.Find(id);
        }

        public void InsertOrUpdate(BoundedPeriod boundedPeriod)
        {
            if (boundedPeriod.Id == default(int))
            {
                PreInsertCheck(boundedPeriod);
            }
            else
            {
                PreUpdateCheck(boundedPeriod);
            } 
            
            DatesValidCheck(boundedPeriod);
            
            if (boundedPeriod.Id == default(int))
            {
                _context.BoundedPeriods.Add(boundedPeriod);
            }
            else
            {
                _context.Entry(boundedPeriod).State = EntityState.Modified;
            }
        }

        private void DatesValidCheck(BoundedPeriod boundedPeriod)
        {
            if (!_bookingDatesService.IsBoundedPeriodValid(_context, boundedPeriod))
            {
                throw new BoundedPeriodDatesException();
            }
        }

        private void PreUpdateCheck(BoundedPeriod boundedPeriod)
        {
            PreInsertCheck(boundedPeriod);
            if (_context.BoundedPeriods.Find(boundedPeriod.Id) == null)
            {
                throw new ParameterNotFoundException();
            }
        }

        private void PreInsertCheck(BoundedPeriod boundedPeriod)
        {
            if (_context.BoundedReservationsHotels.Find(boundedPeriod.BoundedReservationsHotelId) == null)
            {
                throw new ParameterNotFoundException();
            }
        }

        public void Delete(int id)
        {
            var boundedPeriod = _context.BoundedPeriods.Find(id);
            if (boundedPeriod == null)
            {
                throw new ParameterNotFoundException();
            }
            _context.BoundedPeriods.Remove(boundedPeriod);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<BoundedPeriod> GetAllForHotel(int hotelId)
        {
            CheckHotelIsValid(hotelId);
            return All.Where(x => x.BoundedReservationsHotelId == hotelId).ToList();
        }

        public BoundedPeriod GetByHotelIdAndId(int hotelId, int id)
        {
            CheckHotelIsValid(hotelId);
            return All.SingleOrDefault(x => x.BoundedReservationsHotelId == hotelId && x.Id == id);
        }

        private void CheckHotelIsValid(int hotelId)
        {
            var hotel = _context.BoundedReservationsHotels.Find(hotelId);
            if (hotel == null)
                throw new ParameterNotFoundException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}