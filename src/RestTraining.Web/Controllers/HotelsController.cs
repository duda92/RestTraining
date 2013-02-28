using System.Collections.Generic;
using System.Web.Mvc;
using RestTraining.Api;
using RestTraining.Api.DTO;

namespace RestTraining.Web.Controllers
{
    public partial class HotelsController : ControllerBase
    {
        public const string Resource = "/api/Hotels/";

        public virtual ActionResult Index()
        {
            var responseObj = JsonRequestExecutor.ExecuteGet<List<HotelDTO>>(BaseUrl, Resource);
            return View(responseObj);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            ViewData[ViewUtils.ControllerActionTypeKey] = ControllerActionType.Create;
            return View(MVC.Hotels.Views.EditOrCreate, new HotelDTO());
        }

        [HttpPost]
        public virtual ActionResult Create(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                JsonRequestExecutor.ExecutePost(hotel, BaseUrl, Resource);
                return RedirectToAction(MVC.Hotels.Index());
            }
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            return View(MVC.Hotels.Views.EditOrCreate, hotel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var hotelDTO = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", id));
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            return View(MVC.Hotels.Views.EditOrCreate, hotelDTO);
        }

        [HttpPost]
        public virtual ActionResult Edit(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                JsonRequestExecutor.ExecutePut(hotel, BaseUrl, Resource);
                return RedirectToAction(MVC.Hotels.Index());
            }
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            return View(MVC.Hotels.Views.EditOrCreate);
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            var hotelDTO = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", id));
            return View(hotelDTO);
        }

        [HttpPost]
        public virtual ActionResult Delete(HotelDTO hotel)
        {
            try
            {
                JsonRequestExecutor.ExecuteDelete<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", hotel.Id));
                return RedirectToAction(MVC.Hotels.Index());
            }
            catch
            {
                return View(hotel);
            }
        }
    }
}
