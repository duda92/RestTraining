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
    public class FreeReservationsHotelsController : ApiController
    {
        private readonly IFreeReservationsHotelRepository _freeReservationsHotelRepository;

        public FreeReservationsHotelsController(IFreeReservationsHotelRepository freeReservationsHotelRepository)
        {
            _freeReservationsHotelRepository = freeReservationsHotelRepository;
        }

        public List<FreeReservationsHotel> Get()
        {
            return _freeReservationsHotelRepository.All.ToList();
        }

        public FreeReservationsHotel Get(int id)
        {
            var hotel = _freeReservationsHotelRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return hotel;
        }

        public HttpResponseMessage Post([FromBody]FreeReservationsHotel hotel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            _freeReservationsHotelRepository.InsertOrUpdate(hotel);
            _freeReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotel);
            string uri = Url.Route(null, new { id = hotel.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]FreeReservationsHotel hotel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            } 
            _freeReservationsHotelRepository.InsertOrUpdate(hotel);
            _freeReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel);
            string uri = Url.Route(null, new { id = hotel.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var hotel = _freeReservationsHotelRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _freeReservationsHotelRepository.Delete(id);
            _freeReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _freeReservationsHotelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
