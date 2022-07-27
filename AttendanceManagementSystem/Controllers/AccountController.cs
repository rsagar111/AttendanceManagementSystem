using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using AttendanceManagementSystem.Models;
using static System.Net.WebRequestMethods;
using System.Web.Script.Serialization;
using System.IO;

namespace AttendanceManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        BusinessLayer objBus = new BusinessLayer();
        // GET: Account
     
        public ActionResult Login()
        {
            return View();
        }
       
        public string CheckUsernameAndPassword(string UserLog)
        {
            string username = "", password = "", RoleName = "", RoleType="", result="";
            string filename = "", fpath = "";
            DataTable dt = new DataTable();
           JavaScriptSerializer serializer = new JavaScriptSerializer();
           User obj= serializer.Deserialize<User>(UserLog);
            dt = objBus.GetUsernamePassword(obj);
            if (dt!=null && dt.Rows.Count>0)
            {
                username = Convert.ToString(dt.Rows[0]["UserName"]);
                username = username.ToUpper();
                password = dt.Rows[0]["UserPassword"].ToString();
                password = password.ToUpper();
                if (obj.Username.ToUpper().Equals(username) && obj.Password.ToUpper().Equals(password))
                {
                    
                    HttpContext.Session["objuser"] = "Validuser";
                    HttpContext.Session["PersonId"] = dt.Rows[0]["Person_Id"].ToString();
                    HttpContext.Session["UserId"] = dt.Rows[0]["UserLogin_Id"].ToString();
                    HttpContext.Session["UserName"] = Convert.ToString(dt.Rows[0]["UserName"]);
                    HttpContext.Session["UserPassword"] = dt.Rows[0]["UserPassword"].ToString();
                    HttpContext.Session["RoleName"] = dt.Rows[0]["RoleName"].ToString();
                    HttpContext.Session["RoleType"] = dt.Rows[0]["RoleType"].ToString();

                    obj.Username = Convert.ToString(dt.Rows[0]["UserName"]);
                    obj.Password = Convert.ToString(dt.Rows[0]["UserPassword"]);

                    string filepath = dt.Rows[0]["FilePath"].ToString();
                    string FileName = dt.Rows[0]["FileName"].ToString();
                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadedFiles/Staff"));

                    for (int j = 0; j < filePaths.Length; j++)
                    {
                        filename = Path.GetFileName(filePaths[j]);
                        fpath = Path.GetFullPath(filePaths[j]);
                        if (filename == FileName && fpath == filepath)
                        {
                            HttpContext.Session["FileName"] = filename;
                        }
                    }

                    RoleName =  dt.Rows[0]["RoleName"].ToString();
                    RoleType = dt.Rows[0]["RoleType"].ToString();
                    if (RoleType=="2")
                    {
                        Session["AddedBy"] = "577";
                    }
                    if (RoleType == "4")
                    {
                        Session["AddedBy"] = "666";
                    }

                    result = "True" + "|" + username + "|" + password+"|"+ RoleName+"|"+RoleType;
                }
            }
            //if (RoleName == "Admin" && RoleType == "2")
            //{
            //    return RedirectToRoute("AdminDashboard", "AdminDashboardCard");

            //}
            //else
            //{
            //    ViewBag.message = "False";
            //    return View(obj);
            //}
            return result;
        }

        public string CheckUserExit(string oldpass,string fusername)
        {
            string result = "",userId="";
            DataTable dt = new DataTable();
            dt = objBus.CheckUserExit(oldpass, fusername);
            if (dt != null && dt.Rows.Count > 0)
            {
                userId = dt.Rows[0]["UserLogin_Id"].ToString();
                result = "True"+"|"+userId;
            }
            else
            {
                result = "False";
            }
            return result;
        }
        public string ChangeUserPassword(string changePass)
        {
            string result = "";
            DataTable dt = new DataTable();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User obj = serializer.Deserialize<User>(changePass);
            result = objBus.ChangeUserPassword(obj);
            return result;
        }

        public ActionResult Logout()
        {
            Session["objuser"] = null;
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}