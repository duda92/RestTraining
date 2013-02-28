using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Repositories;

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

        public List<BoundedPeriodDTO> Get(int hotelId)
        {
            var boundedPeriods = _boundedPeriodRepository.All.Where(x => x.BoundedReservationsHotelId == hotelId).ToList();
            return boundedPeriods.Select(x => x.ToDTO()).ToList();
        }

        public BoundedPeriodDTO Get(int hotelId, int id)
        {
            return
                _boundedPeriodRepository.All.SingleOrDefault(x => x.BoundedReservationsHotelId == hotelId && x.Id == id).ToDTO();
        }

        public HttpResponseMessage Post(int hotelId, [FromBody]BoundedPeriodDTO boundedPeriodDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            } 
            if (_boundedReservationsHotelRepository.Find(hotelId) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            boundedPeriodDTO.BoundedReservationsHotelId = hotelId;
            var boundedPeriod = boundedPeriodDTO.ToEntity();
            try
            {
                _boundedPeriodRepository.InsertOrUpdate(boundedPeriod);
                _boundedPeriodRepository.Save();
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            var response = Request.CreateResponse(HttpStatusCode.Created, boundedPeriod.ToDTO());
            string uri = Url.Route(null, new { id = boundedPeriod.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]BoundedPeriodDTO boundedPeriodDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            if (_boundedPeriodRepository.Find(boundedPeriodDTO.Id) == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            var boundedPeriod = boundedPeriodDTO.ToEntity();
            try
            {
                _boundedPeriodRepository.InsertOrUpdate(boundedPeriod);
                _boundedPeriodRepository.Save();
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, boundedPeriod.ToDTO());
            string uri = Url.Route(null, new { id = boundedPeriod.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var boundedPeriod = _boundedPeriodRepository.Find(id);
            if (boundedPeriod == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _boundedPeriodRepository.Delete(id);
            _boundedPeriodRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, boundedPeriod.ToDTO());
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
