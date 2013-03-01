using System.Web.Mvc;

namespace RestTraining.Web.Controllers
{
    public partial class BoundedBookingController : Controller
    {
        public virtual ActionResult Index(int hotelId)
        {
            return View();
        }

        //
        // GET: /BoundedBooking/Details/5

        public virtual ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BoundedBooking/Create

        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BoundedBooking/Create

        [HttpPost]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BoundedBooking/Edit/5

        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BoundedBooking/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BoundedBooking/Delete/5

        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BoundedBooking/Delete/5

        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
