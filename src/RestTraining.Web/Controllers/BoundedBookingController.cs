using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestTraining.Common.DTO;
using RestTraining.Common.Proxy;
using RestTraining.Web.Models;

namespace RestTraining.Web.Controllers
{
    public partial class BoundedBookingController : ControllerBase
    {
        private const string HotelNumbersResource = "/api/Hotels/{0}/HotelNumbers/";
        private const string HotelsResource = "/api/Hotels/";
        private const string BoundedBookingResource = "api/Booking/BoundedReservations/{0}/";
        private const string BoundedPeriodsResource = "api/BoundedReservations/{0}/Periods/";


        public virtual ActionResult Index(int hotelId)
        {
            var bookings = executor.ExecuteGet<List<BoundedBookingDTO>>(BaseUrl, string.Format(BoundedBookingResource, hotelId));
            var bookingList = new List<BookingFullViewModel>();

            foreach (var booking in bookings)
            {
                var boundedPeriod = executor.ExecuteGet<BoundedPeriodDTO>(BaseUrl, string.Format(BoundedPeriodsResource + "{1}", hotelId, booking.BoundedPeriodId));
                var hotel = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));
                var hotelNumber = executor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(HotelNumbersResource + "{1}", hotelId, booking.HotelNumberId));
                bookingList.Add(new BookingFullViewModel
                {
                    BeginDate = boundedPeriod.BeginDate,
                    EndDate = boundedPeriod.EndDate,
                    Client = booking.Client,
                    Hotel = hotel,
                    HotelNumber = hotelNumber,
                    BookingId = booking.Id
                });
            }

            return View(MVC.Shared.Views.BookingFullViewModelList, bookingList);
        }

        public virtual ActionResult Create(int hotelId, int hotelNumberId = 0)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            var hotel = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));

            if (hotel.Type != HotelDTO.TypeDescriminator.Bounded)
                throw new Exception("invalid hotel type for bounded reservations");

            var hotelNumbers = executor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
            var boundedPeriods = executor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));
            var boundedBooking = new BoundedBookingDTO { HotelId = hotelId, HotelNumberId = hotelNumberId };
            var boundedBookingViewModel = new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods };
            return View(MVC.BoundedBooking.Views.EditOrCreate, boundedBookingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(int hotelId, BoundedBookingDTO boundedBooking)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            var hotelNumbers = executor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
            var boundedPeriods = executor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));
            
            if (!ModelState.IsValid)
            {
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }

            HttpStatusCode responseCode;
            var response = executor.ExecutePost(boundedBooking, BaseUrl, string.Format(BoundedBookingResource, hotelId), out responseCode);
            
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
                var boundedPeriod = boundedPeriods.Single(x => x.Id == response.BoundedPeriodId);
                var hotel = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));
                var hotelNumber = hotelNumbers.Single(x => x.Id == response.HotelNumberId);
            
                var viewModel = new BookingFullViewModel
                {
                    BeginDate = boundedPeriod.BeginDate,
                    EndDate = boundedPeriod.EndDate,
                    Client = response.Client,
                    Hotel = hotel,
                    HotelNumber = hotelNumber,
                    BookingId = response.Id
                };
                return View(MVC.BoundedBooking.Views.BookingEdited, viewModel);
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
            var hotel = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));

            if (hotel.Type != HotelDTO.TypeDescriminator.Bounded)
                throw new Exception("invalid hotel type for bounded reservations");

            var hotelNumbers = executor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
            var boundedPeriods = executor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));

            var boundedBooking = executor.ExecuteGet<BoundedBookingDTO>(BaseUrl, string.Format(BoundedBookingResource + "{1}", hotelId, id));
            var boundedBookingViewModel = new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods };
            return View(MVC.BoundedBooking.Views.EditOrCreate, boundedBookingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, BoundedBookingDTO boundedBooking)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            var boundedPeriods = executor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(BoundedPeriodsResource, hotelId));
            var hotelNumbers = executor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
               
            if (!ModelState.IsValid)
            {
                return View(MVC.BoundedBooking.Views.EditOrCreate, new BoundedBookingViewModel { BoundedBooking = boundedBooking, HotelNumbers = hotelNumbers, BoundedPeriods = boundedPeriods });
            }

            HttpStatusCode responseCode;
            var response = executor.ExecutePut(boundedBooking, BaseUrl, string.Format(BoundedBookingResource, hotelId), out responseCode);

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
                var boundedPeriod = boundedPeriods.Single(x => x.Id == response.BoundedPeriodId);
                var hotel = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));
                var hotelNumber = hotelNumbers.Single(x => x.Id == response.HotelNumberId);

                var viewModel = new BookingFullViewModel
                    {
                        BeginDate = boundedPeriod.BeginDate,
                        EndDate = boundedPeriod.EndDate,
                        Client = response.Client,
                        Hotel = hotel,
                        HotelNumber = hotelNumber,
                        BookingId = response.Id
                    };
                return View(MVC.BoundedBooking.Views.BookingEdited, viewModel);
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
            executor.ExecuteDelete<BoundedBookingDTO>(BaseUrl, string.Format(BoundedBookingResource + "{1}", hotelId, id), out responseCode);

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
