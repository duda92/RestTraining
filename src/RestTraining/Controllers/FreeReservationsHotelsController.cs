using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestTraining.Common.DTO;
using RestTraining.Api.Domain.Repositories;

namespace RestTraining.Api.Controllers
{
    [Authorize]
    public class FreeReservationsHotelsController : ApiController
    {
        private readonly IFreeReservationsHotelRepository _freeReservationsHotelRepository;

        public FreeReservationsHotelsController(IFreeReservationsHotelRepository freeReservationsHotelRepository)
        {
            _freeReservationsHotelRepository = freeReservationsHotelRepository;
        }

        public List<FreeReservationsHotelDTO> Get()
        {
            var hotels = _freeReservationsHotelRepository.AllIncluding(x => x.HotelsAttractions).ToList();
            return hotels.Select(x => x.ToDTO()).ToList();
        }

        public FreeReservationsHotelDTO Get(int id)
        {
            var hotel = _freeReservationsHotelRepository.AllIncluding(x => x.HotelsAttractions).SingleOrDefault(x => x.Id == id);
            if (hotel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return hotel.ToDTO();
        }

        public HttpResponseMessage Post([FromBody]FreeReservationsHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotel = hotelDTO.ToEntity();
            _freeReservationsHotelRepository.InsertOrUpdate(hotel);
            _freeReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.Created, hotel.ToDTO());
            string uri = Url.Route(null, new { id = hotel.Id });
            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public HttpResponseMessage Put([FromBody]FreeReservationsHotelDTO hotelDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
            var hotel = hotelDTO.ToEntity();
            _freeReservationsHotelRepository.InsertOrUpdate(hotel);
            _freeReservationsHotelRepository.Save();
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel.ToDTO());
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
            var response = Request.CreateResponse(HttpStatusCode.OK, hotel.ToDTO());
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
