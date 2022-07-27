using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendanceManagementSystem.Models;
namespace AttendanceManagementSystem.Controllers
{
    [CheckSession]
    public class StudentDashboardController : Controller
    {
        // GET: StudentDashboard
        public ActionResult StudentDashboardCard()
        {
            return View();
        }
    }
}