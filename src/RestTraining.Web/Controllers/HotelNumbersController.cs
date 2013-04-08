using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestTraining.Common.DTO;
using RestTraining.Common.Proxy;

namespace RestTraining.Web.Controllers
{
    public partial class HotelNumbersController : ControllerBase
    {
        public const string Resource = "/api/Hotels/{0}/HotelNumbers/";

        public virtual ActionResult Index(int hotelId)
        {
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(Resource, hotelId));
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
            JsonRequestExecutor.ExecutePost(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
        
        [HttpGet]
        public virtual ActionResult Edit(int hotelId, int id)
        {
            var hotelNumber = JsonRequestExecutor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
            return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(MVC.HotelNumbers.Views.EditOrCreate, hotelNumber);
            JsonRequestExecutor.ExecutePut(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }

        public virtual ActionResult Delete(int hotelId, int hotelNumberId)
        {
            JsonRequestExecutor.ExecuteDelete<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, hotelNumberId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
    }
}