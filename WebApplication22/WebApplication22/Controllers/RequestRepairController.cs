using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebApplication22.Models;

namespace WebApplication22.Controllers
{
    public class RequestRepairController : Controller
    {
        private carsEntities1 db = new carsEntities1();
        // GET: RequestRepair
        public ActionResult Index()
        {
            return View(db.Request2_Repairs());
        }
    }
}