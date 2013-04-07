using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestTraining.Api;
using RestTraining.Api.DTO;
using RestTraining.Web.Models;

namespace RestTraining.Web.Controllers
{
    public partial class FreeBookingController : ControllerBase
    {
        private const string HotelNumbersResource = "/api/Hotels/{0}/HotelNumbers/";
        private const string HotelsResource = "/api/Hotels/";
        private const string FreeBookingResource = "api/Booking/FreeReservations/{0}/";

        public virtual ActionResult Index(int hotelId)
        {
            var bookings = JsonRequestExecutor.ExecuteGet<List<FreeBookingDTO>>(BaseUrl, string.Format(FreeBookingResource, hotelId));
            var bookingList = new List<BookingFullViewModel>();

            foreach (var booking in bookings)
            {
                var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));
                var hotelNumber = JsonRequestExecutor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(HotelNumbersResource + "{1}", hotelId, booking.HotelNumberId));
                bookingList.Add(new BookingFullViewModel
                {
                    BeginDate = booking.BeginDate,
                    EndDate = booking.EndDate,
                    Client = booking.Client,
                    Hotel = hotel,
                    HotelNumber = hotelNumber,
                    BookingId = booking.Id
                });
            }

            return View(MVC.Shared.Views.BookingFullViewModelList, bookingList);
        }
        
        public virtual ActionResult Create(int hotelId)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create; 
            var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));

            if (hotel.Type != HotelDTO.TypeDescriminator.Free)
                throw new Exception("invalid hotel type for free reservations");

            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));

            var freeBooking = new FreeBookingDTO { HotelId = hotelId };
            var freeBookingViewModel = new FreeBookingViewModel  { FreeBooking = freeBooking,  HotelNumbers = hotelNumbers  };
            return View(MVC.FreeBooking.Views.EditOrCreate, freeBookingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(int hotelId, FreeBookingDTO freeBooking)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));
                
            if (!ModelState.IsValid)
            {
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }

            HttpStatusCode responseCode;
            var response = JsonRequestExecutor.ExecutePost(freeBooking, BaseUrl, string.Format(FreeBookingResource, hotelId), out responseCode);

            if (responseCode == HttpStatusCode.Conflict)
            {
                _viewDataProvider.BookingDatesConflict();
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }
            if (responseCode == HttpStatusCode.OK || responseCode == HttpStatusCode.Created)
            {
                var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));
                var hotelNumber = hotelNumbers.Single(x => x.Id == response.HotelNumberId);
                var viewModel = new BookingFullViewModel
                {
                    BeginDate = response.BeginDate,
                    EndDate = response.EndDate,
                    Client = response.Client,
                    Hotel = hotel,
                    HotelNumber = hotelNumber,
                    BookingId = response.Id
                };
                return View(MVC.FreeBooking.Views.BookingEdited, viewModel);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }

        public virtual ActionResult Edit(int hotelId, int id)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));

            if (hotel.Type != HotelDTO.TypeDescriminator.Free)
                throw new Exception("invalid hotel type for free reservations");

            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));

            var freeBooking = JsonRequestExecutor.ExecuteGet<FreeBookingDTO>(BaseUrl, string.Format(FreeBookingResource + "{1}", hotelId, id));
            var freeBookingViewModel = new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers };
            return View(MVC.FreeBooking.Views.EditOrCreate, freeBookingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, FreeBookingDTO freeBooking)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(HotelNumbersResource, hotelId));

            if (!ModelState.IsValid)
            {
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }

            HttpStatusCode responseCode;
            var response = JsonRequestExecutor.ExecutePut(freeBooking, BaseUrl, string.Format(FreeBookingResource, hotelId), out responseCode);

            if (responseCode == HttpStatusCode.Conflict)
            {
                _viewDataProvider.BookingDatesConflict();
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }
            if (responseCode == HttpStatusCode.OK || responseCode == HttpStatusCode.Created)
            {
                var hotel = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(HotelsResource + "{0}", hotelId));
                var hotelNumber = hotelNumbers.Single(x => x.Id == response.HotelNumberId);
                var viewModel = new BookingFullViewModel
                {
                    BeginDate = response.BeginDate,
                    EndDate = response.EndDate,
                    Client = response.Client,
                    Hotel = hotel,
                    HotelNumber = hotelNumber,
                    BookingId = response.Id
                };
                return View(MVC.FreeBooking.Views.BookingEdited, viewModel);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.FreeBooking.Views.EditOrCreate, new FreeBookingViewModel { FreeBooking = freeBooking, HotelNumbers = hotelNumbers });
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }


        public virtual ActionResult Delete(int hotelId, int id)
        {
            HttpStatusCode responseCode;
            JsonRequestExecutor.ExecuteDelete<FreeBookingDTO>(BaseUrl, string.Format(FreeBookingResource + "{1}", hotelId, id), out responseCode);

            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.FreeBooking.Views.BookingDeleted);
            }
            if (responseCode == HttpStatusCode.OK)
            {
                return View(MVC.FreeBooking.Views.BookingDeleted);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.FreeBooking.Views.BookingDeleted);
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }
    }
}
