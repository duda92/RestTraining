using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using RestTraining.Common.DTO;
using System.Linq;
using RestTraining.Common.Proxy;

namespace RestTraining.Web.Controllers
{
    public partial class BoundedPeriodsController : ControllerBase
    {
        public const string Resource = "/api/BoundedReservations/{0}/Periods/";
        
        public virtual ActionResult Index(int hotelId)
        {
            var responseObj = JsonRequestExecutor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(Resource, hotelId));

            responseObj.OrderBy(x => x.BeginDate).ToList();

            return View(responseObj);
        }

        public virtual ActionResult Create(int hotelId)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            return View(MVC.BoundedPeriods.Views.EditOrCreate, new BoundedPeriodDTO());
        }

        [HttpPost]
        public virtual ActionResult Create(int hotelId, BoundedPeriodDTO boundedPeriod)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Create;
            if (!ModelState.IsValid)
            {
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            HttpStatusCode responseCode;
            JsonRequestExecutor.ExecutePost(boundedPeriod, BaseUrl, string.Format(Resource, hotelId), out responseCode);
            
            if (responseCode == HttpStatusCode.Conflict)
            {
                _viewDataProvider.InvalidBoundedPeriod();
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            if (responseCode == HttpStatusCode.OK || responseCode == HttpStatusCode.Created)
            {
                return RedirectToAction(MVC.BoundedPeriods.Index(hotelId));
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            throw new Exception("Unexpected api service response");
        }

        public virtual ActionResult Edit(int hotelId, int id)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            HttpStatusCode responseCode;
            JsonRequestExecutor.ExecuteGet<BoundedPeriodDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id), out responseCode);

            return View(MVC.BoundedPeriods.Views.EditOrCreate, new BoundedPeriodDTO());
        }

        [HttpPost]
        public virtual ActionResult Edit(int hotelId, BoundedPeriodDTO boundedPeriod)
        {
            _viewDataProvider.ControllerActionType = ControllerActionType.Edit;
            if (!ModelState.IsValid)
            {
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            HttpStatusCode responseCode;
            JsonRequestExecutor.ExecutePut(boundedPeriod, BaseUrl, string.Format(Resource, hotelId), out responseCode);

            if (responseCode == HttpStatusCode.Conflict)
            {
                _viewDataProvider.InvalidBoundedPeriod();
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            if (responseCode == HttpStatusCode.OK)
            {
                return RedirectToAction(MVC.BoundedPeriods.Index(hotelId));
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return View(MVC.BoundedPeriods.Views.EditOrCreate, boundedPeriod);
            }
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("Api service response BadRequest");
            }
            
            throw new Exception("Unexpected api service response");
        }

        public virtual ActionResult Delete(int hotelId, int id)
        {
            HttpStatusCode responseCode;
            JsonRequestExecutor.ExecuteDelete<BoundedPeriodDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id), out responseCode);

            if (responseCode == HttpStatusCode.OK)
            {
                return RedirectToAction(MVC.BoundedPeriods.Index(hotelId));
            }
            if (responseCode == HttpStatusCode.BadGateway)
            {
                _viewDataProvider.ApiServiceNotReponse();
                return MVC.BoundedPeriods.Index(hotelId);
            }
            if (responseCode == HttpStatusCode.NotFound)
            {
                _viewDataProvider.ResourceNotFound();
                return MVC.BoundedPeriods.Index(hotelId);
            }
            if (responseCode == HttpStatusCode.BadRequest)
            {
                throw new Exception("BadRequest api service response");
            }
            throw new Exception("Unexpected api service response");
        }
    }
}
