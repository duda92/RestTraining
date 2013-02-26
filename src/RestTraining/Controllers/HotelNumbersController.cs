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
    public class HotelNumbersController : ApiController
    {
        private readonly IHotelNumbersRepository _hotelNumbersRepository;

        public HotelNumbersController(IHotelNumbersRepository hotelNumbersRepository)
        {
            _hotelNumbersRepository = hotelNumbersRepository;
        }

        public List<HotelNumberDTO> Get(int hotelId)
        {
            var hotelNumbers = _hotelNumbersRepository.All.Where(x => x.HotelId == hotelId).ToList();
            return hotelNumbers.Select(x => x.ToDTO()).ToList();
        }

        public HotelNumberDTO Get(int hotelId, int id)
        {
            var hotelNumber = _hotelNumbersRepository.Find(id);
            if (hotelNumber == null || hotelNumber.HotelId != hotelId)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return hotelNumber.ToDTO();
        }

        public HttpResponseMessage Post([FromBody]HotelNumberDTO hotelNumberDTO, int hotelId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotelNumber = hotelNumberDTO.ToEntity();
            _hotelNumbersRepository.InsertOrUpdate(hotelNumber, hotelId);
            _hotelNumbersRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotelNumber.ToDTO());
            string uri = Url.Route(null, new { id = hotelNumberDTO.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]HotelNumberDTO hotelNumberDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotelNumber = hotelNumberDTO.ToEntity();
            _hotelNumbersRepository.InsertOrUpdate(hotelNumber);
            _hotelNumbersRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotelNumber.ToDTO());
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
            var response = Request.CreateResponse(HttpStatusCode.OK, hotelNumber.ToDTO());
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