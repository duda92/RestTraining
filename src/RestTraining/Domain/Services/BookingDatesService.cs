using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity.Utility;
using RestTraining.Api.Domain.Entities;

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
                throw new ArgumentException("Hotel passed does not exist");
            var hotelNumber =
                context.HotelNumbers.SingleOrDefault(
                    x => x.HotelId == freeBooking.HotelId && 
                        x.Id == freeBooking.HotelNumberId);
            if (hotelNumber == null)
                throw new ArgumentException("HotelNumber passed does not exist");

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
                throw new ArgumentException("Bounded period not found");
            var hotel = context.BoundedReservationsHotels.Find(boundedBooking.HotelId);
            if (hotel == null)
                throw new ArgumentException("Hotel passed does not exist");
            var hotelNumber =
                context.HotelNumbers.SingleOrDefault(
                    x => x.HotelId == boundedBooking.HotelId && x.Id == boundedBooking.HotelNumberId);
            if (hotelNumber == null)
                throw new ArgumentException("HotelNumber passed does not exist");

            var debugValue1 = context.BoundedBookings.Where(x => x.HotelId == boundedBooking.HotelId &&
                                                                 x.HotelNumberId == boundedBooking.HotelNumberId && 
                                                                 x.Id != boundedBooking.Id).//if update
                                                                 ToList();
            var debugValue2 = debugValue1.Any(x => x.BoundedPeriod.Id == boundedBooking.BoundedPeriod.Id);
            return !debugValue2;

        }

        public bool DoesDatesIntersect(DateTime beginDate, DateTime endDate, List<Pair<DateTime, DateTime>> datesList)
        {
            if (datesList.Any(x => x.First >= x.Second))
                throw new ArgumentException("Invalid datesList passed, beginDate cannot be bigger or equal the endDate");

            if (beginDate > endDate)
                throw new ArgumentException("Invalid dates passed, beginDate cannot be bigger or equal the endDate",
                                            "beginDate and endDate");

            return datesList.Any(x => (x.First <= beginDate && x.Second >= beginDate) ||
                                      (x.First <= endDate && x.Second >= endDate) ||
                                      (x.First >= beginDate && x.Second <= endDate));
        }
    }
}