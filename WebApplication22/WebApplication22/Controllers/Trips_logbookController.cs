using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class Trips_logbookController : Controller
    {
        private carsEntities1 db = new carsEntities1();

        // GET: trips_logbook
        public ActionResult Index()
        {
            var trips_logbook = db.trips_logbook.Include(t => t.cars).Include(t => t.trips);
            return View(trips_logbook.ToList());
        }

        // GET: trips_logbook/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trips_logbook trips_logbook = db.trips_logbook.Find(id);
            if (trips_logbook == null)
            {
                return HttpNotFound();
            }
            return View(trips_logbook);
        }

        // GET: trips_logbook/Create
        public ActionResult Create()
        {
            ViewBag.cars_number = new SelectList(db.cars, "number", "number");
            IEnumerable<SelectListItem> selectList = from s in db.trips
                                                     select new SelectListItem
                                                     {
                                                         Value = s.idtrip.ToString(),
                                                         Text = s.start_point + " - " + s.end_point
                                                     };
            ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
            //ViewBag.trips_idtrip = new SelectList(db.trips, "idtrip", "idtrip");
            return View();
        }

        // POST: trips_logbook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idtrip_logbook,trips_idtrip,cars_number,start_time_trip,end_time_trip,weight")] trips_logbook trips_logbook)
        {
            IEnumerable<SelectListItem> selectList = from s in db.trips select new SelectListItem { Value = s.idtrip.ToString(), Text = s.start_point + " - " + s.end_point };
            if (ModelState.IsValid)
            {
                //если введенное время больше чем на 2 месяца текущего
                if (trips_logbook.start_time_trip > DateTime.Now.AddMonths(2))
                {
                    MessageBox.Show("Введенное вами время начала рейса некорректно");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number"); 
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                if (trips_logbook.end_time_trip < trips_logbook.start_time_trip)
                {
                    MessageBox.Show("Время начала рейса не может быть позднее времени окончания поездки");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                var f = db.trips_logbook.Where(p => p.start_time_trip == trips_logbook.start_time_trip && p.cars_number == trips_logbook.cars_number && p.trips_idtrip == trips_logbook.trips_idtrip && p.end_time_trip == trips_logbook.end_time_trip && p.weight == trips_logbook.weight).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой рейс уже добавлен в базу данных");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                var f1 = db.trips_logbook.Where(p => p.cars_number == trips_logbook.cars_number && 
                ((p.start_time_trip>=trips_logbook.start_time_trip && p.start_time_trip <= trips_logbook.end_time_trip && p.end_time_trip<=trips_logbook.end_time_trip) ||
                (p.start_time_trip<=trips_logbook.start_time_trip && p.end_time_trip>=trips_logbook.start_time_trip && p.end_time_trip <= trips_logbook.end_time_trip)||
                (p.start_time_trip >= trips_logbook.start_time_trip && p.start_time_trip <= trips_logbook.end_time_trip && p.end_time_trip >= trips_logbook.end_time_trip))).FirstOrDefault();
                if (f1 != null)
                {
                    MessageBox.Show("Данная машина уже находится в другой поездке в этом промежутке времени");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                db.trips_logbook.Add(trips_logbook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cars_number = new SelectList(db.cars, "number", "number", trips_logbook.cars_number);
            ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text", trips_logbook.trips_idtrip);
            return View(trips_logbook);
        }

        // GET: trips_logbook/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trips_logbook trips_logbook = db.trips_logbook.Find(id);
            if (trips_logbook == null)
            {
                return HttpNotFound();
            }
            ViewBag.cars_number = new SelectList(db.cars, "number", "number", trips_logbook.cars_number);
            IEnumerable<SelectListItem> selectList = from s in db.trips select new SelectListItem { Value = s.idtrip.ToString(), Text = s.start_point + " - " + s.end_point };
            ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text", trips_logbook.trips_idtrip);
            return View(trips_logbook);
        }

        // POST: trips_logbook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idtrip_logbook,trips_idtrip,cars_number,start_time_trip,end_time_trip,weight")] trips_logbook trips_logbook)
        {
            IEnumerable<SelectListItem> selectList = from s in db.trips select new SelectListItem { Value = s.idtrip.ToString(), Text = s.start_point + " - " + s.end_point };
            if (ModelState.IsValid)
            {
                //если введенное время больше чем на 2 месяца текущего
                if (trips_logbook.start_time_trip > DateTime.Now.AddMonths(2))
                {
                    MessageBox.Show("Введенное вами время начала рейса некорректно");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                if (trips_logbook.end_time_trip < trips_logbook.start_time_trip)
                {
                    MessageBox.Show("Время начала рейса не может быть позднее времени окончания поездки");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                var f = db.trips_logbook.Where(p => p.start_time_trip == trips_logbook.start_time_trip && p.cars_number == trips_logbook.cars_number && p.trips_idtrip == trips_logbook.trips_idtrip && p.end_time_trip == trips_logbook.end_time_trip && p.weight == trips_logbook.weight).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой рейс уже добавлен в базу данных");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                var f1 = db.trips_logbook.Where(p => p.cars_number == trips_logbook.cars_number && p.idtrip_logbook != trips_logbook.idtrip_logbook &&
                ((p.start_time_trip >= trips_logbook.start_time_trip && p.start_time_trip <= trips_logbook.end_time_trip && p.end_time_trip <= trips_logbook.end_time_trip) ||
                (p.start_time_trip <= trips_logbook.start_time_trip && p.end_time_trip >= trips_logbook.start_time_trip && p.end_time_trip <= trips_logbook.end_time_trip) ||
                (p.start_time_trip >= trips_logbook.start_time_trip && p.start_time_trip <= trips_logbook.end_time_trip && p.end_time_trip >= trips_logbook.end_time_trip))).FirstOrDefault();
                if (f1 != null)
                {
                    MessageBox.Show("Данная машина уже находится в другой поездке в этом промежутке времени");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text");
                    return View();
                }
                db.Entry(trips_logbook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cars_number = new SelectList(db.cars, "number", "number", trips_logbook.cars_number);
            ViewBag.trips_idtrip = new SelectList(selectList, "Value", "Text", trips_logbook.trips_idtrip);
            return View(trips_logbook);
        }

        // GET: trips_logbook/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trips_logbook trips_logbook = db.trips_logbook.Find(id);
            if (trips_logbook == null)
            {
                return HttpNotFound();
            }
            return View(trips_logbook);
        }

        // POST: trips_logbook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            trips_logbook trips_logbook = db.trips_logbook.Find(id);
            db.trips_logbook.Remove(trips_logbook);
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
