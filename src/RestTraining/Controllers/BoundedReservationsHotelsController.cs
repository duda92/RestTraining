using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Api.Models;
using RestTraining.Domain;

namespace RestTraining.Api.Controllers
{
    public class BoundedReservationsHotelsController : ApiController
    {
        private readonly IBoundedReservationsHotelRepository _boundedReservationsHotelRepository;

        public BoundedReservationsHotelsController(IBoundedReservationsHotelRepository boundedReservationsHotelRepository)
        {
            _boundedReservationsHotelRepository = boundedReservationsHotelRepository;
        }

        public List<BoundedReservationsHotel> Get()
        {
            return _boundedReservationsHotelRepository.All.ToList();
        }

        public BoundedReservationsHotel Get(int id)
        {
            var hotel = _boundedReservationsHotelRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return hotel;
        }

        public HttpResponseMessage Post([FromBody]BoundedReservationsHotel hotel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            } 
            _boundedReservationsHotelRepository.InsertOrUpdate(hotel);
            _boundedReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotel);
            string uri = Url.Route(null, new { id = hotel.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]BoundedReservationsHotel hotel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            } 
            _boundedReservationsHotelRepository.InsertOrUpdate(hotel);
            _boundedReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel);
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
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel);
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

