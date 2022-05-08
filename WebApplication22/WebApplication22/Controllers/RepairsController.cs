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
    public class RepairsController : Controller
    {
        private carsEntities1 db = new carsEntities1();

        // GET: repairs
        public ActionResult Index()
        {
            return View(db.repairs.ToList());
        }

        // GET: repairs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repairs repairs = db.repairs.Find(id);
            if (repairs == null)
            {
                return HttpNotFound();
            }
            return View(repairs);
        }

        // GET: repairs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: repairs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idrepair,time_rep,type_rep")] repairs repairs)
        {
            if (ModelState.IsValid)
            {
                var f = db.repairs.Where(p => p.type_rep == repairs.type_rep).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой ремонт уже добавлен в базу данных.");
                    return View();
                }
                db.repairs.Add(repairs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairs);
        }

        // GET: repairs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repairs repairs = db.repairs.Find(id);
            if (repairs == null)
            {
                return HttpNotFound();
            }
            return View(repairs);
        }

        // POST: repairs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idrepair,time_rep,type_rep")] repairs repairs)
        {
            if (ModelState.IsValid)
            {
                var f = db.repairs.Where(p => p.type_rep == repairs.type_rep && p.idrepair!=repairs.idrepair).FirstOrDefault();
                if (f != null)
                {
                    MessageBox.Show("Такой ремонт уже добавлен в базу данных.");
                    return View();
                }
                db.Entry(repairs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repairs);
        }

        // GET: repairs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repairs repairs = db.repairs.Find(id);
            if (repairs == null)
            {
                return HttpNotFound();
            }
            var rep = db.repairs_logbook.Where(p => p.repairs_idrepair == id).FirstOrDefault();
            if (rep != null)
            {
                MessageBox.Show("Этот ремонт уже добавлен в Журнал Ремонтов, его невозможно удалить");
                return RedirectToAction("Index");
            }
            return View(repairs);
        }

        // POST: repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repairs repairs = db.repairs.Find(id);
            db.repairs.Remove(repairs);
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
