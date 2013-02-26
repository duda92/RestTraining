using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RestTraining.Api;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Domain;

namespace RestTraining.Web.Controllers
{
    public partial class HotelNumbersController : Controller
    {
        public const string BaseUrl = "http://localhost.:9075";
        public const string Resource = "/api/Hotels/{0}/HotelNumbers/";

        public virtual ActionResult Index(int hotelId)
        {
            ViewBag.hotelId = hotelId;
            var hotelNumbers = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(Resource, hotelId));
            return View(hotelNumbers);
        }

        [HttpGet]
        public virtual ActionResult Create(int hotelId)
        {
            return View();
        }
        
        [HttpPost]
        public virtual ActionResult Create(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(hotelNumber);
            JsonRequestExecutor.ExecutePost(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
        
        [HttpGet]
        public virtual ActionResult Edit(int hotelId, int id)
        {
            var hotelNumber = JsonRequestExecutor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
            return View(hotelNumber);
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, HotelNumberDTO hotelNumber)
        {
            if (!ModelState.IsValid)
                return View(hotelNumber);
            JsonRequestExecutor.ExecutePut(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }

        public virtual ActionResult Delete(int hotelId, int hotelNumberId)
        {
            JsonRequestExecutor.ExecuteDelete<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, hotelNumberId));
            return RedirectToAction(MVC.HotelNumbers.Index(hotelId));
        }
    }

    public class WindowViewListBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var windowViews = new List<WindowView>();
            var viewTypesParam = bindingContext.ValueProvider.GetValue("WindowViews").AttemptedValue;

            try
            {
                var viewTypes = viewTypesParam.Split(',');
                foreach (var viewType in viewTypes)
                {
                    var vindowViewType = (WindowViewType)Int32.Parse(viewType);
                    windowViews.Add(new WindowView { Type = vindowViewType });
                }
            }
            catch (Exception)
            {
                return null;
            }
            return windowViews;
        }
    }
}
