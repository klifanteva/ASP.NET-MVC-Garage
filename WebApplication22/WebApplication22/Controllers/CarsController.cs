using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class CarsController : Controller
    {
        private carsEntities1 db = new carsEntities1();

        // GET: Cars
        public ActionResult Index()
        {
            return View(db.cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars cars = db.cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "number,brand,colour,type_car")] cars cars)
        {
            if (ModelState.IsValid)
            {
                var f = db.cars.Where(p => p.number == cars.number).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такая машина уже добавлена в базу данных.");
                    return View();
                }
                db.cars.Add(cars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cars);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars cars = db.cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "number,brand,colour,type_car")] cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cars);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars cars = db.cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            var rep = db.repairs_logbook.Where(p => p.cars_number == id).FirstOrDefault();
            if (rep != null)
            {
                MessageBox.Show("Эта машина уже добавлена в Журнал Ремонтов, её невозможно удалить");
                return RedirectToAction("Index");
            }
            var trip = db.trips_logbook.Where(p => p.cars_number == id).FirstOrDefault();
            if (trip != null)
            {
                MessageBox.Show("Эта машина уже добавлена в Журнал Рейсов, её невозможно удалить");
                return RedirectToAction("Index");
            }
            return View(cars);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cars cars = db.cars.Find(id);
            db.cars.Remove(cars);
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
