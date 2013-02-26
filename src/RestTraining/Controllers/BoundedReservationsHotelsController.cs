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
    public class BoundedReservationsHotelsController : ApiController
    {
        private readonly IBoundedReservationsHotelRepository _boundedReservationsHotelRepository;

        public BoundedReservationsHotelsController(IBoundedReservationsHotelRepository boundedReservationsHotelRepository)
        {
            _boundedReservationsHotelRepository = boundedReservationsHotelRepository;
        }

        public List<BoundedReservationsHotelDTO> Get()
        {
            var hotels = _boundedReservationsHotelRepository.All.ToList();
            return hotels.Select(x => x.ToDTO()).ToList();
        }

        public BoundedReservationsHotelDTO Get(int id)
        {
            var hotel = _boundedReservationsHotelRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return hotel.ToDTO();
        }

        public HttpResponseMessage Post([FromBody]BoundedReservationsHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotel = hotelDTO.ToEntity();
            _boundedReservationsHotelRepository.InsertOrUpdate(hotel);
            _boundedReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotel.ToDTO());
            string uri = Url.Route(null, new { id = hotel.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]BoundedReservationsHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotel = hotelDTO.ToEntity();
            _boundedReservationsHotelRepository.InsertOrUpdate(hotel);
            _boundedReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel.ToDTO());
            string uri = Url.Route(null, new { id = hotel.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var hotel = _boundedReservationsHotelRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _boundedReservationsHotelRepository.Delete(id);
            _boundedReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel.ToDTO());
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _boundedReservationsHotelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

