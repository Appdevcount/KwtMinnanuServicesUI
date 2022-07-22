using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class InspectionAppointmentsController : MyBaseController// Controller
    {
        // GET: InspectionAppointments
        public ActionResult Index()
        {
            return View();
        }
    }
}