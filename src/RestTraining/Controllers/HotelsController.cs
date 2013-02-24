using RestTraining.Api.Models;
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
            var allHotelDTOs = new List<HotelDTO> {};
            foreach (var hotel in _hotelRepository.All)
            {
                allHotelDTOs.Add(HotelDTO.FromHotel(hotel));
            }
            return allHotelDTOs;
        }

        // GET api/Hotels
        public Hotel Get(int id)
        {
            return _hotelRepository.Find(id);
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

    public class HotelDTO : Hotel
    {
        public static HotelDTO FromHotel(Hotel hotel)
        {
            return new HotelDTO()
                {
                    Address = hotel.Address,
                    HotelNumbers = hotel.HotelNumbers,
                    Id = hotel.Id,
                    Title = hotel.Title
                };
        }
    }
}
