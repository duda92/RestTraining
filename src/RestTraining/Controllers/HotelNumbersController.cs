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
    public class HotelNumbersController : ApiController
    {
        private readonly IHotelNumbersRepository _hotelNumbersRepository;

        public HotelNumbersController(IHotelNumbersRepository hotelNumbersRepository)
        {
            _hotelNumbersRepository = hotelNumbersRepository;
        }

        public List<HotelNumber> Get(int hotelId)
        {
            return _hotelNumbersRepository.All.Where(x => x.HotelId == hotelId).ToList();
        }

        public HotelNumber Get(int hotelId, int id)
        {
            var hotelNumber = _hotelNumbersRepository.Find(id);
            if (hotelNumber == null || hotelNumber.HotelId != hotelId)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return hotelNumber;
        }

        public HttpResponseMessage Post([FromBody]HotelNumber hotelNumber, int hotelId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            _hotelNumbersRepository.InsertOrUpdate(hotelNumber, hotelId);
            _hotelNumbersRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotelNumber);
            string uri = Url.Route(null, new { id = hotelNumber.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]HotelNumber hotelNumber)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            _hotelNumbersRepository.InsertOrUpdate(hotelNumber);
            _hotelNumbersRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotelNumber);
            string uri = Url.Route(null, new { id = hotelNumber.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var hotelNumber = _hotelNumbersRepository.Find(id);
            if (hotelNumber == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _hotelNumbersRepository.Delete(id);
            _hotelNumbersRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotelNumber);
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hotelNumbersRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}