using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Models
{
    
    public class BusinessLayer
    {
        DataLayer dl = new DataLayer();
      
        internal DataTable GetUsernamePassword(User Obj)
        {
            return dl.GetUsernamePassword(Obj);
        }

        internal DataTable CheckUserExit(string oldpass,string fusername)
        {
            return dl.CheckUserExit(oldpass, fusername);
        }
        internal string ChangeUserPassword(User obj)
        {
            return dl.ChangeUserPassword(obj);
        }
        public static bool CheckDataTable(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      

        public static List<SelectListItem> CreateDropdownList(DataTable dt)
        {
            List<SelectListItem> lstDist = new List<SelectListItem>();
            lstDist.Add(new SelectListItem() { Text = "--Select--", Value = "0" });
            if (CheckDataTable(dt))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstDist.Add(new SelectListItem() { Text = dt.Rows[i][1].ToString(), Value = dt.Rows[i][0].ToString() });
                    }
                }
            }
            else
            {
                lstDist.Add(new SelectListItem() { Text = "None", Value = "-1" });
            }
            return lstDist;
        }

       

        #region Standard Saving
        internal string SaveStandardName(AdminCommonClass obj)
        {
            return dl.SaveStandardName(obj);
        }
        internal DataTable GetStandardNameData()
        {
            return dl.GetStandardNameData();
        }
        internal string UpdateStandardName(AdminCommonClass obj)
        {
            return dl.UpdateStandardName(obj);
        }
        internal string DeleteStandardName(string stdid)
        {
            return dl.DeleteStandardName(stdid);
        }
        #endregion
        internal DataTable GetDropdownStandardName()
        {
            return dl.GetDropdownStandardName();
        }
        internal DataTable GetDivisionDrop( string stdid)
        {
            return dl.GetDivisionDrop(stdid);
        }
        #region Division Saving
        internal string SaveDivisionNameDetails(AdminCommonClass obj)
        {
            return dl.SaveDivisionNameDetails(obj);
        }
        internal DataTable GetDivisionData()
        {
            return dl.GetDivisionData();
        }
        internal string UpdateDivisionName(AdminCommonClass obj)
        {
            return dl.UpdateDivisionName(obj);
        }
        internal string DeleteDivisionName(string stdid)
        {
            return dl.DeleteDivisionName(stdid);
        }
        #endregion

        #region AddStaff
        public string SaveStaffDetails(HttpPostedFileBase file, PersonDetails obj)
        {
            return dl.SaveStaffDetails(file,obj);
        }
        #endregion

        internal DataTable GetStaffProfileImage()
        {
            return dl.GetStaffProfileImage();
        }

        internal DataTable GetAllStaffDetailsAnalytics()
        {
            return dl.GetAllStaffDetailsAnalytics();
        }

        internal DataTable GetStudentReportData()
        {
            return dl.GetStudentReportData();
        }

        internal string DeletePersonRecord(string personid)
        {
            return dl.DeletePersonRecord(personid);
        }

        internal string ChngePasswd(User obj)
        {
            return dl.ChngePasswd(obj);
        }
        #region Staff Details
        internal DataTable GetStaffProfileDetails()
        {
            string personid="";
            if (HttpContext.Current.Session["PersonId"]!=null)
            {
                personid = HttpContext.Current.Session["PersonId"].ToString();
            }
            return dl.GetStaffProfileDetails(personid);
        }
        internal string SaveStaffUpdatedDetails(PersonDetails Obj,HttpPostedFileBase file)
        {
            return dl.SaveStaffUpdatedDetails(Obj,file);
        }
        #endregion

        #region  AddStudent
        internal DataTable GetStandardNameAndStandardId()
        {
            string personid = "";
            if (HttpContext.Current.Session["PersonId"] != null)
            {
                personid = HttpContext.Current.Session["PersonId"].ToString();
            }
            return dl.GetStandardNameAndStandardId(personid);
        }

        internal string GetSeatDetailsAccToDivsion(string divid)
        {
            return dl.GetSeatDetailsAccToDivsion(divid);
        }
        internal string SaveAddStudent(PersonDetails Obj,HttpPostedFileBase file)
        {
            return dl.SaveAddStudent(Obj, file);
        }
        #endregion

        #region Attendance
        internal DataTable GetStudentAttendanceData(Attendance obj)
        {
            return dl.GetStudentAttendanceData(obj);
        }

        internal DataTable GetStudentData()
        {
            return dl.GetStudentData();
        }
        #endregion
    }
}