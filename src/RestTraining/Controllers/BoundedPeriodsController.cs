using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Api.Models;

namespace RestTraining.Api.Controllers
{
    public class BoundedPeriodsController : ApiController
    {
        private readonly IBoundedPeriodRepository _boundedPeriodRepository;
        private readonly IBoundedReservationsHotelRepository _boundedReservationsHotelRepository;

        public BoundedPeriodsController(IBoundedPeriodRepository boundedPeriodRepository, IBoundedReservationsHotelRepository boundedReservationsHotelRepository)
        {
            _boundedPeriodRepository = boundedPeriodRepository;
            _boundedReservationsHotelRepository = boundedReservationsHotelRepository;
        }

        public List<BoundedPeriod> Get(int hotelId)
        {
            return _boundedPeriodRepository.All.Where(x => x.BoundedReservationsHotelId == hotelId).ToList();
        }

        public BoundedPeriod Get(int hotelId, int id)
        {
            return
                _boundedPeriodRepository.All.SingleOrDefault(x => x.BoundedReservationsHotelId == hotelId && x.Id == id);
        }

        public HttpResponseMessage Post(int hotelId, [FromBody]BoundedPeriod boundedPeriod)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            } 
            if (_boundedReservationsHotelRepository.Find(hotelId) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            boundedPeriod.BoundedReservationsHotelId = hotelId;
            try
            {
                _boundedPeriodRepository.InsertOrUpdate(boundedPeriod);
                _boundedPeriodRepository.Save();
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            var response = Request.CreateResponse(HttpStatusCode.Created, boundedPeriod);
            string uri = Url.Route(null, new { id = boundedPeriod.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]BoundedPeriod boundedPeriod)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            if (_boundedPeriodRepository.Find(boundedPeriod.Id) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            try
            {
                _boundedPeriodRepository.InsertOrUpdate(boundedPeriod);
                _boundedPeriodRepository.Save();
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, boundedPeriod);
            string uri = Url.Route(null, new { id = boundedPeriod.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var hotel = _boundedPeriodRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _boundedPeriodRepository.Delete(id);
            _boundedPeriodRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _boundedReservationsHotelRepository.Dispose();
                _boundedPeriodRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
