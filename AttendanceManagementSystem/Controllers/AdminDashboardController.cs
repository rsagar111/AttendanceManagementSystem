
using AttendanceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;


using System.Web.Script.Serialization;
using System.IO;
using System.Web.UI.WebControls;

namespace AttendanceManagementSystem.Controllers
{

    [CheckSession]
    public class AdminDashboardController : Controller
    {
        BusinessLayer objBus = new BusinessLayer();
        // GET: AdminDashboard
       
        public ActionResult AdminDashboardCard()
        {
            if (Session["RoleType"].ToString() != "2")
            {
                return RedirectToAction("Login", "Account");
            }
            //PersonDetails obj = new PersonDetails();
            //DataTable dt = new DataTable();
            //dt =objBus.GetStaffProfileImage();
            //if (dt!=null && dt.Rows.Count>0)
            //{
            //   // obj.file=dt.Rows[i][""]
            //}
            //string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadedFiles/Staff"));
            //foreach (var item in filePaths)
            //{
            //    string fileName = Path.GetFileName(item);
            //}
            //string fileName = Path.GetFileName(filePaths[0]);
            //ViewBag.Image = filePaths;
            //string[] filePaths = Server.MapPath("~/UploadedFiles/Staff");

            //List<ListItem> files = new List<ListItem>();
            //foreach (string filePath in filePaths)
            //{
            //    string fileName = Path.GetFileName(filePath);
            //    files.Add(new ListItem(fileName, "~/UploadedFiles/Staff/" + fileName));
            //}


            return View();
        }
        public ActionResult AddStandard()
        {
            return View();
        }
        #region Standard Saving
        public string SaveStandardName(string standard)
        {
            string result = "";
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdminCommonClass obj = serializer.Deserialize<AdminCommonClass>(standard);
            result = objBus.SaveStandardName(obj);
            return result;
        }
        public JsonResult GetStandardNameData()
        {
            List<AdminCommonClass> lstStandardData = new List<AdminCommonClass>();
            DataTable dt = new DataTable();
            dt = objBus.GetStandardNameData();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdminCommonClass obj = new AdminCommonClass();
                    obj.StandardId = Convert.ToString(dt.Rows[i]["Standard_Id"]);
                    obj.StandardName = Convert.ToString(dt.Rows[i]["StandardName"]);
                    lstStandardData.Add(obj);
                }
            }
            return Json(lstStandardData, JsonRequestBehavior.AllowGet);
        }
        public string UpdateStandardName(string updateSID)
        {
            string result = "";
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdminCommonClass obj = serializer.Deserialize<AdminCommonClass>(updateSID);
            result = objBus.UpdateStandardName(obj);
            return result;
        }
        public string DeleteStandardName(string stdid)
        {
            string result = "";
            DataTable dt = new DataTable();

            result = objBus.DeleteStandardName(stdid);
            return result;
        }
        #endregion

        public ActionResult AddDivision()
        {
            AdminCommonClass adobj = new AdminCommonClass();
            DataTable dts = new DataTable();
            dts = objBus.GetDropdownStandardName();
            ViewBag.StandardDrop = BusinessLayer.CreateDropdownList(dts);

            return View(adobj);
        }
        #region Division Saving
        public string SaveDivisionNameDetails(string standard)
        {
            string result = "";
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdminCommonClass obj = serializer.Deserialize<AdminCommonClass>(standard);
            result = objBus.SaveDivisionNameDetails(obj);
            return result;
        }

        public string UpdateDivisionName(string updateSID)
        {
            string result = "";
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdminCommonClass obj = serializer.Deserialize<AdminCommonClass>(updateSID);
            result = objBus.UpdateDivisionName(obj);
            return result;
        }
        public string DeleteDivisionName(string stdid)
        {
            string result = "";
            DataTable dt = new DataTable();

            result = objBus.DeleteDivisionName(stdid);
            return result;
        }
        public JsonResult GetDivisionData()
        {
            List<AdminCommonClass> lstStandardData = new List<AdminCommonClass>();
            DataTable dt = new DataTable();
            dt = objBus.GetDivisionData();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdminCommonClass obj = new AdminCommonClass();
                    obj.DivisionId = Convert.ToString(dt.Rows[i]["Division_Id"]);
                    obj.DivisionName = Convert.ToString(dt.Rows[i]["DivisionName"]);
                    obj.Seat = Convert.ToString(dt.Rows[i]["Seat"]);
                    obj.StandardId = Convert.ToString(dt.Rows[i]["StandardId"]);
                    obj.StandardName = Convert.ToString(dt.Rows[i]["StandardName"]);
                    lstStandardData.Add(obj);
                }
            }
            return Json(lstStandardData, JsonRequestBehavior.AllowGet);
        }

        #endregion
        [HttpGet]
        public ActionResult AddStaff()
        {
            PersonDetails obj = new PersonDetails();
            DataTable dts = new DataTable();
            dts = objBus.GetDropdownStandardName();
            ViewBag.StandardDrop = BusinessLayer.CreateDropdownList(dts);

            return View(obj);
        }
        [HttpPost]
        public ActionResult AddStaff(HttpPostedFileBase file, PersonDetails obj)
        {
            string response = "";
            DataTable dts = new DataTable();
            dts = objBus.GetDropdownStandardName();
            ViewBag.StandardDrop = BusinessLayer.CreateDropdownList(dts);
            response = objBus.SaveStaffDetails(file, obj);
            ViewBag.message = response;

            return View(obj);
        }

        public string DeletePersonRecord(string personid)
        {
            string result = "";
            DataTable dt = new DataTable();

            result = objBus.DeletePersonRecord(personid);
            return result;
        }
        public ActionResult StaffReport()
        {
            PersonDetails obj = new PersonDetails();
            string fileName = "", filePath = "";
            obj.lstPersonDetails = new List<PersonDetails>();
            DataTable dt = new DataTable();
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadedFiles/Staff"));

            dt = objBus.GetAllStaffDetailsAnalytics();
            if (dt != null & dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PersonDetails pobj = new PersonDetails();

                    pobj.PersonId = Convert.ToString(dt.Rows[i]["Person_Id"]);
                    pobj.PersonName = Convert.ToString(dt.Rows[i]["Pname"]);
                    pobj.Standard = Convert.ToString(dt.Rows[i]["StandardName"]);

                    pobj.Emailid = Convert.ToString(dt.Rows[i]["Email"]);
                    pobj.MobileNo = Convert.ToString(dt.Rows[i]["Mobile"]);
                    pobj.file = Convert.ToString(dt.Rows[i]["FilePath"]);
                    pobj.FileName = Convert.ToString(dt.Rows[i]["FileName"]);
                    pobj.FileLength = Convert.ToInt32(dt.Rows[i]["FileLength"]);
                    pobj.Qualification = Convert.ToString(dt.Rows[i]["Qualification"]);
                    pobj.City = Convert.ToString(dt.Rows[i]["City"]);
                    pobj.Gender = dt.Rows[i]["Gender"].ToString() == "1" ? "Male" : "Female";
                    for (int j = 0; j < filePaths.Length; j++)
                    {
                        fileName = Path.GetFileName(filePaths[j]);
                        filePath = Path.GetFullPath(filePaths[j]);
                        if (fileName == pobj.FileName && filePath == pobj.file)
                        {
                            pobj.FileName = fileName;
                        }
                    }
                    obj.lstPersonDetails.Add(pobj);

                }
            }
            return View(obj);
        }

        public ActionResult ComplainDetails()
        {
            return View();
        }
        public ActionResult LeaveDetails()
        {
            return View();
        }
        public ActionResult StudentReport()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            User obj = new User();
            obj.UserId = Session["UserId"].ToString();
            obj.Username = Session["UserName"].ToString();
            obj.Password = Session["UserPassword"].ToString();
            return View(obj);
        }
        [HttpPost]
        public ActionResult ChangePassword(User obj)
        {
            string result = objBus.ChngePasswd(obj);

              obj.IsReturn = result;
          
                return View(obj);
           
        }
    }
}