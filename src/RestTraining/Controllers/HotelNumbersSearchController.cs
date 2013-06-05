using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Common.DTO;

namespace RestTraining.Api.Controllers
{
    public class HotelNumbersSearchController : ApiController
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelNumbersRepository _hotelNumbersRepository;

        public HotelNumbersSearchController(IHotelRepository hotelRepository, IHotelNumbersRepository hotelNumbersRepository)
        {
            _hotelRepository = hotelRepository;
            _hotelNumbersRepository = hotelNumbersRepository;
        }
        
        public List<HotelNumberDTO> Post (HotelNumbersSearchQuery query)
        {
            var results = _hotelNumbersRepository.All.ToList();
            return results.Select(x => x.ToDTO()).ToList();
        }
    }
}
