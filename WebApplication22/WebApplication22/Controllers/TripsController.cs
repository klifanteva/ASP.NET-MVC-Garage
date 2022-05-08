using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class TripsController : Controller
    {
        private carsEntities1 db = new carsEntities1();

        // GET: Trips
        public ActionResult Index()
        {
            return View(db.trips.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trips trips = db.trips.Find(id);
            if (trips == null)
            {
                return HttpNotFound();
            }
            return View(trips);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idtrip,distance,end_point,start_point")] trips trips)
        {
            if (ModelState.IsValid)
            {
                var f = db.trips.Where(p => p.distance == trips.distance && p.end_point == trips.end_point && p.start_point == trips.start_point).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой рейс уже добавлен в базу данных.");
                    return View();
                }
                db.trips.Add(trips);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trips);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trips trips = db.trips.Find(id);
            if (trips == null)
            {
                return HttpNotFound();
            }
            return View(trips);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idtrip,distance,end_point,start_point")] trips trips)
        {
            if (ModelState.IsValid)
            {
                var f = db.trips.Where(p => p.distance == trips.distance && p.end_point == trips.end_point && p.start_point == trips.start_point).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой рейс уже добавлен в базу данных.");
                    return View();
                }
                db.Entry(trips).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trips);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trips trips = db.trips.Find(id);
            if (trips == null)
            {
                return HttpNotFound();
            }
            var trip = db.trips_logbook.Where(p => p.trips_idtrip == id).FirstOrDefault();
            if (trip != null)
            {
                MessageBox.Show("Этот рейс уже добавлен в Журнал Рейсов, его невозможно удалить");
                return RedirectToAction("Index");
            }
            return View(trips);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            trips trips = db.trips.Find(id);
            db.trips.Remove(trips);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
