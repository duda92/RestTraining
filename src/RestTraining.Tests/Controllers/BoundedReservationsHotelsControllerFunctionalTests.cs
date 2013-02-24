﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestTraining.Api.Tests.Utils;
using RestTraining.Domain;

namespace RestTraining.Api.Tests.Controllers
{
    [TestClass]
    public class BoundedReservationsHotelsControllerFunctionalTests
    {
        private const string Resource = TestHelpers.BoundedReservationsHotelApiHelper.Resource;
        private const string BaseUrl = TestHelpers.BaseUrl;

        [TestMethod]
        public void Get()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotel();
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);
            var all = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Title == hotelObj.Title && x.Address == hotelObj.Address));
        }

        [TestMethod]
        public void Get_Id()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotel();
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);

            List<BoundedReservationsHotel> all = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();
            foreach (var client1 in all)
            {
                var result = TestHelpers.BoundedReservationsHotelApiHelper.TestGet(client1.Id);
                Assert.IsNotNull(result.Address);
                Assert.AreEqual(client1.Address, result.Address);
                Assert.AreEqual(client1.Id, result.Id);
                Assert.AreEqual(client1.Title, result.Title);
            }
        }

        [TestMethod]
        public void Post()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotel();
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);
            var all = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Title == hotelObj.Title && x.Address == hotelObj.Address));
        }

        [TestMethod]
        public void Put()
        {
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotel());
            List<BoundedReservationsHotel> all = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();

            var hotelObj = all.FirstOrDefault();
            hotelObj.Title = RandomUtils.RandomString(10);
            var id = hotelObj.Id;

            TestHelpers.BoundedReservationsHotelApiHelper.TestPut(hotelObj);
            var test = TestHelpers.BoundedReservationsHotelApiHelper.TestGet(hotelObj.Id);
            Assert.AreEqual(hotelObj.Title, test.Title);
            Assert.AreEqual(hotelObj.Address, test.Address);
            Assert.AreEqual(hotelObj.Id, id);
        }

       
    }
}
