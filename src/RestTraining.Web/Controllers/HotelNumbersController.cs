using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using RestTraining.Common.DTO;
using RestTraining.Web.Models;

namespace RestTraining.Web.Controllers
{
    public partial class HotelNumbersController : ControllerBase
    {
        public const string Resource = "/api/Hotels/{0}/HotelNumbers/";
        public const string SearchResource = "/api/HotelNumbers/Search";

        public virtual ActionResult Index(int hotelId)
        {
            var hotelNumbers = executor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(Resource, hotelId));
            return View(hotelNumbers);
        }

        [HttpGet]
        public virtual ActionResult Create(int hotelId)
        {
            return View(MVC.HotelNumbers.Views.EditOrCreate, new HotelNumberDTO());
        }
        
        [HttpPost]
        public virtual ActionResult Create(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
            executor.ExecutePost(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
        
        [HttpGet]
        public virtual ActionResult Edit(int hotelId, int id)
        {
            var hotelNumber = executor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
            return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
            executor.ExecutePut(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }

        public virtual ActionResult Delete(int hotelId, int hotelNumberId)
        {
            executor.ExecuteDelete<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, hotelNumberId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }

        [HttpGet]
        public virtual ActionResult Search()
        {
            var viewModel = new HotelNumbersSearchViewModel();
            return View(MVC.HotelNumbers.Views.Search, viewModel);
        }

        [HttpPost]
        public virtual ActionResult Search(HotelNumbersSearchViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(MVC.HotelNumbers.Views.Search, viewModel);
            HttpStatusCode responseCode;
            var searchResults = executor.ExecutePost<HotelNumbersSearchQuery, List<HotelNumberDTO>>(viewModel.Query, BaseUrl, string.Format(SearchResource), out responseCode);
            viewModel.Results = searchResults;
            return View(viewModel);
        }

    }
}