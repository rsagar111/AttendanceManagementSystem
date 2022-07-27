using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Attendance()
        {
            return View();
        }

        public ActionResult LeaveForm()
        {
            return View();
        }

        public ActionResult AttendenceReport()
        {
            return View();
        }
        public ActionResult ComplainForm()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}