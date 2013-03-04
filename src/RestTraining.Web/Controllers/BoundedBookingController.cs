﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using RestTraining.Api;
using RestTraining.Api.DTO;
using RestTraining.Web.Models;

namespace RestTraining.Web.Controllers
{
    public partial class BoundedBookingController : ControllerBase
    {
        private const string HotelNumbersResource = "/api/Hotels/{0}/HotelNumbers/";
        private const string HotelsResource = "/api/Hotels/";
        private const string BoundedBookingResource = "api/Booking/BoundedReservations/{0}/";
        private const string BoundedPeriodsResource = "api/BoundedReservations/{0}/Periods/";

        public virtual ActionResult Create(int hotelId)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));

            if (hotel.Type != HotelDTO.TypeDescriminator.Bounded)
                throw new Exception("invalid hotel type for bounded reservations");

            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
            var boundedPeriods = JsonRequestExecutor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));
            var boundedBooking = new BoundedBookingDTO { HotelId = hotelId };
            var boundedBookingViewModel = new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods };
            return View(MVC.BoundedBooking.Views.EditOrCreate, boundedBookingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(int hotelId, BoundedBookingDTO boundedBooking)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
            var boundedPeriods = JsonRequestExecutor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));
            
            if (!ModelState.IsValid)
            {
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }

            HttpStatusCode responseCode;
            var response = JsonRequestExecutor.ExecutePost(boundedBooking, BaseUrl, string.Format(BoundedBookingResource, hotelId), out responseCode);
            if (responseCode == HttpStatusCode.Conflict)
            {
                _viewDataProvider.BookingDatesConflict();
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }
            if (responseCode == HttpStatusCode.OK || responseCode == HttpStatusCode.Created)
            {
                return View(MVC.BoundedBooking.Views.BookingEdited, response);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }

        public virtual ActionResult Edit(int hotelId, int id)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));

            if (hotel.Type != HotelDTO.TypeDescriminator.Bounded)
                throw new Exception("invalid hotel type for bounded reservations");

            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));

            var boundedBooking = JsonRequestExecutor.ExecuteGet<BoundedBookingDTO>(BaseUrl, string.Format(BoundedBookingResource + "{1}", hotelId, id));
            var boundedBookingViewModel = new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers };
            return View(MVC.BoundedBooking.Views.EditOrCreate, boundedBookingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, BoundedBookingDTO boundedBooking)
        {
            var boundedPeriods = JsonRequestExecutor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));

            if (!ModelState.IsValid)
            {
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }

            HttpStatusCode responseCode;
            var response = JsonRequestExecutor.ExecutePut(boundedBooking, BaseUrl, string.Format(BoundedBookingResource, hotelId), out responseCode);

            if (responseCode == HttpStatusCode.Conflict)
            {
                _viewDataProvider.BookingDatesConflict();
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }
            if (responseCode == HttpStatusCode.OK || responseCode == HttpStatusCode.Created)
            {
                return View(MVC.BoundedBooking.Views.BookingEdited, response);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }


        public virtual ActionResult Delete(int hotelId, int id)
        {
            HttpStatusCode responseCode;
            JsonRequestExecutor.ExecuteDelete<BoundedBookingDTO>(BaseUrl, string.Format(BoundedBookingResource + "{1}", hotelId, id), out responseCode);

            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.BoundedBooking.Views.BookingDeleted);
            }
            if (responseCode == HttpStatusCode.OK)
            {
                return View(MVC.BoundedBooking.Views.BookingDeleted);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.BoundedBooking.Views.BookingDeleted);
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }
    }
}