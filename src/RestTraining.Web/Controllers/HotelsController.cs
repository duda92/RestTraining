using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using RestTraining.Common.DTO;

namespace RestTraining.Web.Controllers
{
    public partial class HotelsController : ControllerBase
    {
        public const string Resource = "/api/Hotels/";

        public virtual ActionResult Index()
        {
            HttpStatusCode responseCode;
            var responseObj = executor.ExecuteGet<List<HotelDTO>>(BaseUrl, Resource, out responseCode);
            if (responseCode == HttpStatusCode.OK)
            {
                return View(responseObj);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.Hotels.Views.Index);
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            return View(MVC.Hotels.Views.EditOrCreate, new HotelDTO());
        }

        [HttpPost]
        public virtual ActionResult Create(HotelDTO hotel)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            if (!ModelState.IsValid)
            {
                return View(MVC.Hotels.Views.EditOrCreate, hotel); 
            }
            HttpStatusCode responseCode;
            executor.ExecutePost(hotel, BaseUrl, Resource, out responseCode);
            if (responseCode == HttpStatusCode.OK || responseCode == HttpStatusCode.Created )
            {
                return RedirectToAction(MVC.Hotels.Index());
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.Hotels.Views.EditOrCreate, hotel);
            } 
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Api service response BadRequest");
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            
            HttpStatusCode responseCode;
            var hotelDTO = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", id), out responseCode);

            if (responseCode == HttpStatusCode.OK)
            {
                return View(MVC.Hotels.Views.EditOrCreate, hotelDTO);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.Hotels.Views.EditOrCreate);
            }
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Api service response BadRequest");
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }

        [HttpPost]
        public virtual ActionResult Edit(HotelDTO hotel)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            
            if (!ModelState.IsValid)
            {
                _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
                return View(MVC.Hotels.Views.EditOrCreate, hotel); 
            }

            HttpStatusCode responseCode;
            executor.ExecutePut(hotel, BaseUrl, Resource, out responseCode);
            
            if (responseCode == HttpStatusCode.OK)
            {
                return RedirectToAction(MVC.Hotels.Index());
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.Hotels.Views.EditOrCreate);
            }
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Api service response BadRequest");
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
            
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            HttpStatusCode responseCode;
            var hotelDTO = executor.ExecuteDelete<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", id), out responseCode);

            if (responseCode == HttpStatusCode.OK)
            {
                return View(MVC.Hotels.Views.Delete, hotelDTO);
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.Hotels.Views.EditOrCreate);
            }
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Api service response BadRequest");
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));           
        }

        [HttpPost]
        public virtual ActionResult Delete(HotelDTO hotel)
        {
            HttpStatusCode responseCode;
            executor.ExecuteDelete<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", hotel.Id), out responseCode);

            if (responseCode == HttpStatusCode.OK)
            {
                return RedirectToAction(MVC.Hotels.Index());
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return RedirectToAction(MVC.Hotels.Index());
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.Hotels.Views.Delete);
            }
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Api service response BadRequest");
            }
            throw new Exception(string.Format("Unexpected api service response {0}", responseCode));
        }
    }
}
