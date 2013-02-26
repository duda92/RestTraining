using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Domain.Services;

namespace RestTraining.Api.Tests.Services
{
    [TestClass]
    public class BookingDatesServiceTests
    {
        private BookingDatesService _bookingDatesServise;

        [TestInitialize()]
        public void SetUp()
        {
            _bookingDatesServise = new BookingDatesService();
        }

        [TestMethod]
        public void GivenIntersectedDates_DoesDatesIntersect_Returns_True()
        {
            //Case1
            var beginDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);

            var datesList = new List<Pair<DateTime, DateTime>>()
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(-2), DateTime.Today.AddDays(2)),
                };
            var result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsTrue(result);
            
            //Case2
            datesList = new List<Pair<DateTime, DateTime>>()
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(5), DateTime.Today.AddDays(8)),
                };
            result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsTrue(result);

            //Case3
            datesList = new List<Pair<DateTime, DateTime>>()
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(10)),
                };
            result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsTrue(result);

            //Case4
            datesList = new List<Pair<DateTime, DateTime>>()
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4)),
                };
            result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsTrue(result);

            //Case5
            datesList = new List<Pair<DateTime, DateTime>>()
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(-5), DateTime.Today),
                };
            result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsTrue(result);

            //Case6
            datesList = new List<Pair<DateTime, DateTime>>()
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(7), DateTime.Today.AddDays(8)),
                };
            result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNotIntersectedDates_DoesDatesIntersect_Returns_False()
        {
            var beginDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(7);

            var datesList = new List<Pair<DateTime, DateTime>>
                {
                    new Pair<DateTime, DateTime>(DateTime.Today.AddYears(1).AddDays(-2), DateTime.Today.AddYears(1).AddDays(2)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(-3), DateTime.Today.AddDays(-1)),
                    new Pair<DateTime, DateTime>(DateTime.Today.AddDays(8), DateTime.Today.AddDays(10)),
                };
            var result = _bookingDatesServise.DoesDatesIntersect(beginDate, endDate, datesList);
            Assert.IsFalse(result);
        }
    }
}
