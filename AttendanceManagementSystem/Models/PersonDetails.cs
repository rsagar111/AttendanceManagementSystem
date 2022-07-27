using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

namespace AttendanceManagementSystem.Models
{
   
    public class PersonDetails
    {
        BusinessLayer objBus = new BusinessLayer();
        public string PersonName { get; set; }
        public string PersonId { get; set; }

        public string FatherName { get; set; }
        public string Emailid { get; set; }
        public string MobileNo { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }

        private string filePath { get; set; }

        public string Gender { get; set; }
        public string file { get; set; }
        public string FileName { get; set; }
        public int FileLength { get; set; }
        public string Standard { get; set; }
        public string StandardId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CnfrmPassword { get; set; }
        public string DivisionId { get; set; }
        public string DOB { get; set; }
        public string RollNo { get; set; }
        public string IsReturning { get; set; }
        public string  UserStudent { get; set; }
        public string  PasswordStudent { get; set; }

        public List<PersonDetails> lstPersonDetails { get; set; }

        internal PersonDetails GetStaffProfileDetails()
        {
            string filename = "", fpath = "";
            BusinessLayer bl = new BusinessLayer();
            PersonDetails Objmain = new PersonDetails();
            DataTable dt = new DataTable();
            dt = bl.GetStaffProfileDetails();
            if (dt!=null && dt.Rows.Count>0)
            {
                Objmain.PersonId = dt.Rows[0]["Person_Id"].ToString();
                Objmain.PersonName = dt.Rows[0]["Pname"].ToString();
                Objmain.Emailid = dt.Rows[0]["Email"].ToString();
                Objmain.MobileNo = dt.Rows[0]["Mobile"].ToString();
                Objmain.Address = dt.Rows[0]["Address"].ToString();
                Objmain.City = dt.Rows[0]["City"].ToString();
                Objmain.Pincode = dt.Rows[0]["Pincode"].ToString();
                Objmain.filePath = dt.Rows[0]["FilePath"].ToString();
                Objmain.FileName = dt.Rows[0]["FileName"].ToString();
                //Objmain.FileLength =Convert.ToInt32(dt.Rows[0]["FileLength"]);
                Objmain.Standard = Convert.ToString(dt.Rows[0]["StandardName"]);
                Objmain.StandardId = Convert.ToString(dt.Rows[0]["Standard_Id"]);

                string[] filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"));

                for (int j = 0; j < filePaths.Length; j++)
                {
                    filename = Path.GetFileName(filePaths[j]);
                    fpath = Path.GetFullPath(filePaths[j]);
                    if (filename == Objmain.FileName && fpath == Objmain.filePath)
                    {
                        HttpContext.Current.Session["FileName"] = filename;
                    }
                }
            }
            return Objmain; 
        }

        internal PersonDetails GetInititalStudentReport()
        {
            string filename = "", fpath = "";
            PersonDetails obj = new PersonDetails();
            obj.lstPersonDetails = new List<PersonDetails>();
            DataTable dt = objBus.GetStudentReportData();
            if (dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    PersonDetails mobj = new PersonDetails();
                    mobj.RollNo = dt.Rows[i]["Std_RollNo"].ToString();
                    mobj.PersonName = dt.Rows[i]["Pname"].ToString();
                    mobj.FatherName = dt.Rows[i]["PFname"].ToString();
                    mobj.DivisionId = dt.Rows[i]["DivisionName"].ToString();
                    mobj.Emailid = dt.Rows[i]["Email"].ToString();
                    mobj.MobileNo = dt.Rows[i]["Mobile"].ToString();
                    mobj.filePath = dt.Rows[i]["FilePath"].ToString();
                    mobj.FileName = dt.Rows[i]["FileName"].ToString();
                    mobj.Address = dt.Rows[i]["Address"].ToString();
                    mobj.City = dt.Rows[i]["City"].ToString();
                    mobj.Pincode = dt.Rows[i]["Pincode"].ToString();
                    mobj.Gender = dt.Rows[i]["Gender"].ToString()=="1"?"Male":"Female";

                    //string[] filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"));

                    //for (int j = 0; j < filePaths.Length; j++)
                    //{
                    //    filename = Path.GetFileName(filePaths[j]);
                    //    fpath = Path.GetFullPath(filePaths[j]);
                       
                    //}
                    obj.lstPersonDetails.Add(mobj);
                }
            }
            return obj;
        }
    }

    //public List<PersonDetails> GetAllStaffDetailsAnalytics()
    //{
    //    BusinessLayer bl = new BusinessLayer();
    //    string fileName = "", filePath = "";
    //    PersonDetails Mainpbj = new PersonDetails();
    //    Mainpbj.lstPersonDetails = new List<PersonDetails>();
    //    DataTable dt = new DataTable();
    //    string[] filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"));



    //    dt = bl.GetAllStaffDetailsAnalytics();
    //    if (dt != null & dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            PersonDetails pobj = new PersonDetails();

    //            pobj.PersonId = Convert.ToString(dt.Rows[i]["Person_Id"]);
    //            pobj.PersonName = Convert.ToString(dt.Rows[i]["Pname"]);
    //            pobj.Standard = Convert.ToString(dt.Rows[i]["StandardName"]);

    //            pobj.Emailid = Convert.ToString(dt.Rows[i]["Email"]);
    //            pobj.MobileNo = Convert.ToString(dt.Rows[i]["Mobile"]);
    //            pobj.file = Convert.ToString(dt.Rows[i]["FilePath"]);
    //            pobj.FileName = Convert.ToString(dt.Rows[i]["FileName"]);
    //            pobj.FileLength = Convert.ToInt32(dt.Rows[i]["FileLength"]);
    //            pobj.Qualification = Convert.ToString(dt.Rows[i]["Qualification"]);
    //            pobj.City = Convert.ToString(dt.Rows[i]["City"]);
    //            pobj.Gender = dt.Rows[i]["Gender"].ToString() == "1" ? "Male" : "Female";
    //            for (int j = 0; j < filePaths.Length; j++)
    //            {
    //                fileName = Path.GetFileName(filePaths[j]);
    //                filePath = Path.GetFullPath(filePaths[j]);
    //                if (fileName == pobj.FileName && filePath == pobj.file)
    //                {
    //                    pobj.FileName = fileName;
    //                }
    //            }

    //            lstPersonDetails.Add(pobj);
    //        }
    //        return lstPersonDetails;
    //    }
    //}

   
}