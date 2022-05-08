using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class RequestWeightController : Controller
    {
        private carsEntities1 db = new carsEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexQ1(DateTime start_time, DateTime end_time)
        {
            if (start_time > end_time)
            {
                MessageBox.Show("Дата и время начала периода не могут быть позднее даты и времени окончания периода.");
                return View("Index");
            }
            var q1 = db.Request_Weight(start_time, end_time);
            return View(q1.ToList());
        }
    }
}