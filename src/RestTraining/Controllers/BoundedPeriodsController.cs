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
            if (hotelId == 0)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            try
            {
                var boundedPeriods = _boundedPeriodRepository.GetAllForHotel(hotelId);
                return boundedPeriods.Select(x => x.ToDTO()).ToList();
            }
            catch (ParameterNotFoundException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

        public BoundedPeriodDTO Get(int hotelId, int id)
        {
            return _boundedPeriodRepository.GetByHotelIdAndId(hotelId, id).ToDTO();
        }

        public HttpResponseMessage Post(int hotelId, [FromBody]BoundedPeriodDTO boundedPeriodDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            } 
            boundedPeriodDTO.BoundedReservationsHotelId = hotelId;
            var boundedPeriod = boundedPeriodDTO.ToEntity();
            try
            {
                _boundedPeriodRepository.InsertOrUpdate(boundedPeriod);
                _boundedPeriodRepository.Save();
            }
            catch (ParameterNotFoundException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            catch (BoundedPeriodDatesException)
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
            var boundedPeriod = boundedPeriodDTO.ToEntity();
            try
            {
                _boundedPeriodRepository.InsertOrUpdate(boundedPeriod);
                _boundedPeriodRepository.Save();
            }
            catch (ParameterNotFoundException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            catch (BoundedPeriodDatesException)
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
            try
            {
                _boundedPeriodRepository.Delete(id);
                _boundedPeriodRepository.Save();
                var response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
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
                _boundedReservationsHotelRepository.Dispose();
                _boundedPeriodRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
