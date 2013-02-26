using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using RestTraining.Domain;

namespace RestTraining.Api.Controllers
{
    public class HotelsController : ApiController
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        
        // GET api/Hotels
        public List<HotelDTO> Get()
        {
            var allHotels = _hotelRepository.All.ToList();
            return allHotels.Select(x => x.ToDTO()).ToList();
        }

        // GET api/Hotels
        public HotelDTO Get(int id)
        {
            var hotel = _hotelRepository.Find(id);
            if (hotel == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            return hotel.ToDTO();

        }

        // POST api/Hotels
        public HttpResponseMessage Post(HotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotel = hotelDTO.ToEntity();
            _hotelRepository.InsertOrUpdate(hotel);
            _hotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotel.ToDTO());
            string uri = Url.Route(null, new { id = hotelDTO.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        // PUT api/Hotels
        public HttpResponseMessage Put(HotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotel = hotelDTO.ToEntity();
            _hotelRepository.InsertOrUpdate(hotel);
            _hotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotel.ToDTO());
            string uri = Url.Route(null, new { id = hotelDTO.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var hotel = _hotelRepository.Find(id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            _hotelRepository.Delete(id);
            _hotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel.ToDTO());
            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hotelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    
}
