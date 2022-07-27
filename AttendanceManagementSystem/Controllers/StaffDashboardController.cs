using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;

using AttendanceManagementSystem.Models;
namespace AttendanceManagementSystem.Controllers
{
    [CheckSession]
    public class StaffDashboardController : Controller
    {
        BusinessLayer objBus = new BusinessLayer();
        // GET: StaffDashboard
        [HttpGet]
        public ActionResult StaffDashboardCard()
        {
            PersonDetails Obj = new PersonDetails();
            if (Session["RoleType"].ToString()!="2" && Session["RoleType"].ToString() != "4")
            {
                return RedirectToAction("Login", "Account");
            }
            Obj = Obj.GetStaffProfileDetails();
            return View(Obj);
        }
        [HttpPost]
        public async Task <ActionResult> StaffDashboardCard(PersonDetails Obj,HttpPostedFileBase file)
        {
            string res = objBus.SaveStaffUpdatedDetails(Obj,file);
            ViewBag.Updated = res;
            return View(Obj);
        }

        [HttpGet]
        public async Task<ActionResult> AddStudent()
        {
            PersonDetails Obj = new PersonDetails();

            DataTable dtR = objBus.GetStandardNameAndStandardId();
            if (dtR!=null && dtR.Rows.Count>0)
            {
                Obj.Standard = dtR.Rows[0]["StandardName"].ToString();
                Obj.StandardId = dtR.Rows[0]["Standard_Id"].ToString();

            }
            DataTable dt = objBus.GetDivisionDrop(Obj.StandardId);
            ViewBag.DivisionDrop = BusinessLayer.CreateDropdownList(dt);
            return await Task.FromResult(View(Obj));
        }
        [HttpPost]
        public async Task<ActionResult> AddStudent(PersonDetails Obj,HttpPostedFileBase file)
        {
           
            DataTable dt = objBus.GetDivisionDrop(Obj.StandardId);
            ViewBag.DivisionDrop = BusinessLayer.CreateDropdownList(dt);
            Obj.IsReturning = objBus.SaveAddStudent(Obj, file);
            DataTable dtR = objBus.GetStandardNameAndStandardId();
            if (dtR != null && dtR.Rows.Count > 0)
            {
                Obj.Standard = dtR.Rows[0]["StandardName"].ToString();
                Obj.StandardId = dtR.Rows[0]["Standard_Id"].ToString();

            }
            return await Task.FromResult(View(Obj));
        }
        public string GetSeatDetailsAccToDivsion(string divid) {
            string res = "";
            res = objBus.GetSeatDetailsAccToDivsion(divid);
            return res;
        }
        public async Task<ActionResult> StudentsReport()
        {
            PersonDetails obj = new PersonDetails();
            obj = obj.GetInititalStudentReport();
            return await Task.FromResult(View(obj));
        }

        [HttpGet]
        public async Task<ActionResult> Attendance()
        {
            Attendance Obj = new Attendance();
            Obj.DivisionId = "0";
            Obj = Obj.GetStudentAttendanceData(Obj);
            DataTable dtR = objBus.GetStandardNameAndStandardId();
            if (dtR != null && dtR.Rows.Count > 0)
            {
                Obj.StandardName = dtR.Rows[0]["StandardName"].ToString();
                Obj.StandardId = dtR.Rows[0]["Standard_Id"].ToString();
            }
            DataTable dt = objBus.GetDivisionDrop(Obj.StandardId);
            ViewBag.DivisionDrop = BusinessLayer.CreateDropdownList(dt);

            return await Task.FromResult(View(Obj));
        }
        [HttpPost]
        public async Task<ActionResult> Attendance(Attendance Obj)
        {
            Obj = Obj.GetStudentAttendanceData(Obj);

            DataTable dtR = objBus.GetStandardNameAndStandardId();
            if (dtR != null && dtR.Rows.Count > 0)
            {
                Obj.StandardName = dtR.Rows[0]["StandardName"].ToString();
                Obj.StandardId = dtR.Rows[0]["Standard_Id"].ToString();
            }
            DataTable dt = objBus.GetDivisionDrop(Obj.StandardId);
            ViewBag.DivisionDrop = BusinessLayer.CreateDropdownList(dt);

            return await Task.FromResult(View(Obj));
        }

        public string GetAttendanceStudentAccToDivision()
        {
            string res = "";
            return res;
        }

        public async Task<ActionResult> AttendanceReport()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> AdvanceReport()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> AdvanceAttendanceReport()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Complain()
        {
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> Leave()
        {
            return await Task.FromResult(View());
        }

    }
}