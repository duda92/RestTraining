using System.Collections.Generic;
using System.Web.Mvc;
using RestTraining.Api;
using RestTraining.Api.DTO;

namespace RestTraining.Web.Controllers
{
    public partial class HotelsController : Controller
    {
        private const string BaseUrl = "http://localhost.:9075";
        public const string Resource = "/api/Hotels/";

        //
        // GET: /Hotels/

        public virtual ActionResult Index()
        {
            var responseObj = JsonRequestExecutor.ExecuteGet<List<HotelDTO>>(BaseUrl, Resource);
            return View(responseObj);
        }

        //
        // GET: /Hotels/Details/5

        public virtual ActionResult Details(int id)
        {
            return View();
        }

    //    //
    //    // GET: /Hotels/Create

    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    //
    //    // POST: /Hotels/Create

    //    [HttpPost]
    //    public ActionResult Create(FormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add insert logic here

    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    //
    //    // GET: /Hotels/Edit/5

    //    public ActionResult Edit(int id)
    //    {
    //        return View();
    //    }

    //    //
    //    // POST: /Hotels/Edit/5

    //    [HttpPost]
    //    public ActionResult Edit(int id, FormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add update logic here

    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    //
    //    // GET: /Hotels/Delete/5

    //    public ActionResult Delete(int id)
    //    {
    //        return View();
    //    }

    //    //
    //    // POST: /Hotels/Delete/5

    //    [HttpPost]
    //    public ActionResult Delete(int id, FormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add delete logic here

    //            return RedirectToAction("Index");
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                JsonRequestExecutor.ExecutePost<HotelDTO>(hotel, BaseUrl, Resource);
                return RedirectToAction(MVC.Hotels.Index());
            }
            return View();
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var hotelDTO = JsonRequestExecutor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", id));
            return View(hotelDTO);
        }

        [HttpPost]
        public virtual ActionResult Edit(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                JsonRequestExecutor.ExecutePut(hotel, BaseUrl, Resource);
                return RedirectToAction(MVC.Hotels.Index());
            }
            return View();
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
