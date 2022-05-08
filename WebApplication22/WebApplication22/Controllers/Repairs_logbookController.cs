using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class Repairs_logbookController : Controller
    {
        private carsEntities1 db = new carsEntities1();

        // GET: Repairs_logbook
        public ActionResult Index()
        {
            var repairs_logbook = db.repairs_logbook.Include(r => r.cars).Include(r => r.repairs);
            return View(repairs_logbook.ToList());
        }

        // GET: Repairs_logbook/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repairs_logbook repairs_logbook = db.repairs_logbook.Find(id);
            if (repairs_logbook == null)
            {
                return HttpNotFound();
            }
            return View(repairs_logbook);
        }

        // GET: Repairs_logbook/Create
        public ActionResult Create()
        {
            ViewBag.cars_number = new SelectList(db.cars, "number", "number");
            ViewBag.repairs_idrepair = new SelectList(db.repairs, "idrepair", "type_rep");
            return View();
        }

        // POST: Repairs_logbook/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idrepair_logbook,cars_number,repairs_idrepair,start_time_rep")] repairs_logbook repairs_logbook)
        {
            if (ModelState.IsValid)
            {
                //если введенное время больше чем на 2 месяца текущего
                if (repairs_logbook.start_time_rep>DateTime.Now.AddMonths(2))
                {
                    MessageBox.Show("Введенное вами время некорректно");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.repairs_idrepair = new SelectList(db.repairs, "idrepair", "type_rep");
                    return View();
                }
                var f = db.repairs_logbook.Where(p => p.start_time_rep == repairs_logbook.start_time_rep && p.cars_number == repairs_logbook.cars_number && p.repairs_idrepair == repairs_logbook.repairs_idrepair).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой ремонт уже добавлен в базу данных");
                    ViewBag.cars_number = new SelectList(db.cars, "number", "number");
                    ViewBag.repairs_idrepair = new SelectList(db.repairs, "idrepair", "type_rep");
                    return View();
                }
                db.repairs_logbook.Add(repairs_logbook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cars_number = new SelectList(db.cars, "number", "number", repairs_logbook.cars_number);
            ViewBag.repairs_idrepair = new SelectList(db.repairs, "idrepair", "type_rep", repairs_logbook.repairs_idrepair);
            return View(repairs_logbook);
        }

        // GET: Repairs_logbook/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repairs_logbook repairs_logbook = db.repairs_logbook.Find(id);
            if (repairs_logbook == null)
            {
                return HttpNotFound();
            }
            ViewBag.cars_number = new SelectList(db.cars, "number", "number", repairs_logbook.cars_number);
            ViewBag.repairs_idrepair = new SelectList(db.repairs, "idrepair", "type_rep", repairs_logbook.repairs_idrepair);
            return View(repairs_logbook);
        }

        // POST: Repairs_logbook/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idrepair_logbook,cars_number,repairs_idrepair,start_time_rep")] repairs_logbook repairs_logbook)
        {
            if (ModelState.IsValid)
            {
                //если введенное время больше чем на 2 месяца текущего
                if (repairs_logbook.start_time_rep > DateTime.Now.AddMonths(2))
                {
                    MessageBox.Show("Введенное вами время некорректно");
                    return View();
                }
                var f = db.repairs_logbook.Where(p => p.start_time_rep == repairs_logbook.start_time_rep && p.cars_number == repairs_logbook.cars_number && p.repairs_idrepair == repairs_logbook.repairs_idrepair).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой рейс уже добавлен в базу данных");
                    return View();
                }
                db.Entry(repairs_logbook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cars_number = new SelectList(db.cars, "number", "number", repairs_logbook.cars_number);
            ViewBag.repairs_idrepair = new SelectList(db.repairs, "idrepair", "type_rep", repairs_logbook.repairs_idrepair);
            return View(repairs_logbook);
        }

        // GET: Repairs_logbook/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repairs_logbook repairs_logbook = db.repairs_logbook.Find(id);
            if (repairs_logbook == null)
            {
                return HttpNotFound();
            }
            return View(repairs_logbook);
        }

        // POST: Repairs_logbook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repairs_logbook repairs_logbook = db.repairs_logbook.Find(id);
            db.repairs_logbook.Remove(repairs_logbook);
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
