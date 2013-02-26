using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;

namespace RestTraining.Api.Controllers
{
    public class FreeReservationsBookingController : ApiController
    {
        private readonly IFreeBookingRepository _freeBookingRepository;
        private readonly IFreeReservationsHotelRepository _freeReservationsHotelRepository;

        public FreeReservationsBookingController(IFreeBookingRepository freeBookingRepository, IFreeReservationsHotelRepository freeReservationsHotelRepository)
        {
            _freeBookingRepository = freeBookingRepository;
            _freeReservationsHotelRepository = freeReservationsHotelRepository;
        }

        public List<FreeBookingDTO> Get(int hotelId)
        {
            var bookings = _freeBookingRepository.All.Where(x => x.HotelId == hotelId).ToList();
            return bookings.Select(x => x.ToDTO()).ToList();
        }

        public FreeBookingDTO Get(int hotelId, int id)
        {
            var freeBooking = _freeBookingRepository.All.SingleOrDefault(x => x.HotelId == hotelId && x.Id == id);
            if (freeBooking == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return freeBooking.ToDTO();
        }

        public HttpResponseMessage Post(int hotelId, [FromBody]FreeBookingDTO freeBookingDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            if (_freeReservationsHotelRepository.Find(hotelId) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            freeBookingDTO.HotelId = hotelId;
            var freeBooking = freeBookingDTO.ToEntity();
            _freeBookingRepository.InsertOrUpdate(freeBooking);
            _freeBookingRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, freeBooking.ToDTO());
            string uri = Url.Route(null, new { id = freeBookingDTO.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]FreeBookingDTO freeBookingDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            if (_freeBookingRepository.Find(freeBookingDTO.Id) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            if (_freeReservationsHotelRepository.Find(freeBookingDTO.HotelId) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            var freeBooking = freeBookingDTO.ToEntity();
            try
            {
                _freeBookingRepository.InsertOrUpdate(freeBooking);
                _freeBookingRepository.Save();
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, freeBooking.ToDTO());
            string uri = Url.Route(null, new { id = freeBooking.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var hotel = _freeBookingRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _freeBookingRepository.Delete(id);
            _freeBookingRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel.ToDTO());
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _freeBookingRepository.Dispose();
                _freeReservationsHotelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
