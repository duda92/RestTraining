using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api.Controllers
{
    public class BoundedReservationsBookingController : ApiController
    {
        private readonly IBoundedBookingRepository _boundedBookingRepository;
        private readonly IBoundedReservationsHotelRepository _boundedReservationsHotelRepository;

        public BoundedReservationsBookingController(IBoundedBookingRepository boundedBookingRepository, IBoundedReservationsHotelRepository boundedReservationsHotelRepository)
        {
            _boundedBookingRepository = boundedBookingRepository;
            _boundedReservationsHotelRepository = boundedReservationsHotelRepository;
        }

        public List<BoundedBookingDTO> Get(int hotelId)
        {
            var bookings = _boundedBookingRepository.AllIncluding(x => x.Client).Where(x => x.HotelId == hotelId).ToList();
            return bookings.Select(x => x.ToDTO()).ToList();
        }

        public BoundedBookingDTO Get(int hotelId, int id)
        {
            var boundedBooking = _boundedBookingRepository.AllIncluding(x => x.Client).SingleOrDefault(x => x.HotelId == hotelId && x.Id == id);
            if (boundedBooking == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return boundedBooking.ToDTO();
        }

        public HttpResponseMessage Post(int hotelId, [FromBody]BoundedBookingDTO boundedBookingDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            boundedBookingDTO.HotelId = hotelId;
            var boundedBooking = boundedBookingDTO.ToEntity();
            
            try
            {
                _boundedBookingRepository.InsertOrUpdate(boundedBooking);
                _boundedBookingRepository.Save();
            }
            catch (InvalidDatesBookingException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            catch (ParameterNotFoundException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            var response = Request.CreateResponse(HttpStatusCode.Created, boundedBooking.ToDTO());
            string uri = Url.Route(null, new { id = boundedBookingDTO.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]BoundedBookingDTO boundedBookingDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var boundedBooking = boundedBookingDTO.ToEntity();
            try
            {
                _boundedBookingRepository.InsertOrUpdate(boundedBooking);
                _boundedBookingRepository.Save();
            }
            catch (InvalidDatesBookingException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            catch (ParameterNotFoundException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, boundedBooking.ToDTO());
            string uri = Url.Route(null, new { id = boundedBooking.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _boundedBookingRepository.Delete(id);
                _boundedBookingRepository.Save();
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;    
            }
            catch (InvalidDatesBookingException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            catch (ParameterNotFoundException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _boundedBookingRepository.Dispose();
                _boundedReservationsHotelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
