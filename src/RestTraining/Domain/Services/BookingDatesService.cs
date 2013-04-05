using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity.Utility;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api.Domain.Services
{
    public class BookingDatesService : IBookingDatesService
    {
        public bool IsBoundedPeriodValid(RestTrainingApiContext context, BoundedPeriod boundedPeriod)
        {
            var existingPeriods = context.BoundedPeriods.Where(
                x => x.BoundedReservationsHotelId == boundedPeriod.BoundedReservationsHotelId);

            var datesList = new List<Pair<DateTime, DateTime>>();
            foreach (var period in existingPeriods)
            {
                datesList.Add(new Pair<DateTime, DateTime>(period.BeginDate, period.EndDate));
            }

            return !DoesDatesIntersect(boundedPeriod.BeginDate, boundedPeriod.EndDate, datesList);

        }

        public bool IsFreeBookingValid(RestTrainingApiContext context, FreeBooking freeBooking)
        {
            var hotel = context.FreeReservationsHotels.Find(freeBooking.HotelId);
            if (hotel == null)
                throw new ParameterNotFoundException("HotelId");
            var hotelNumber =
                context.HotelNumbers.SingleOrDefault(
                    x => x.HotelId == freeBooking.HotelId && 
                        x.Id == freeBooking.HotelNumberId);
            if (hotelNumber == null)
                throw new ParameterNotFoundException("HotelNumberId");

            var allBookingsForNumber =
                context.FreeBookings.Where(x => x.HotelId == freeBooking.HotelId && x.HotelNumberId == freeBooking.HotelNumberId && x.Id != freeBooking.Id ).ToList();
            return !DoesDatesIntersect(freeBooking.BeginDate, freeBooking.EndDate,
                                      allBookingsForNumber.Select(
                                          x => new Pair<DateTime, DateTime>(x.BeginDate, x.EndDate)).ToList());
        }

        public bool IsBoundedBookingValid(RestTrainingApiContext context, BoundedBooking boundedBooking)
        {
            var boundedPeriod = context.BoundedPeriods.Find(boundedBooking.BoundedPeriod.Id);
            if (boundedPeriod == null)
                throw new ParameterNotFoundException("BoundedPeriodId");
            var hotel = context.BoundedReservationsHotels.Find(boundedBooking.HotelId);
            if (hotel == null)
                throw new ParameterNotFoundException("HotelId");
            var hotelNumber =
                context.HotelNumbers.SingleOrDefault(
                    x => x.HotelId == boundedBooking.HotelId && x.Id == boundedBooking.HotelNumberId);
            if (hotelNumber == null)
                throw new ParameterNotFoundException("HotelNumberId");

            var debugValue1 = context.BoundedBookings.Where(x => x.HotelId == boundedBooking.HotelId &&
                                                                 x.HotelNumberId == boundedBooking.HotelNumberId && 
                                                                 x.Id != boundedBooking.Id).//if update
                                                                 ToList();
            var debugValue2 = debugValue1.Any(x => x.BoundedPeriod.Id == boundedBooking.BoundedPeriod.Id);
            return !debugValue2;

        }

        public bool DoesDatesIntersect(DateTime beginDate, DateTime endDate, List<Pair<DateTime, DateTime>> datesList)
        {
            return datesList.Any(x => (x.First <= beginDate && x.Second >= beginDate) ||
                                      (x.First <= endDate && x.Second >= endDate) ||
                                      (x.First >= beginDate && x.Second <= endDate));
        }
    }
}