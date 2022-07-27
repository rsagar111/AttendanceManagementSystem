using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;



namespace AttendanceManagementSystem.Models
{
    public class DataLayer
    {
        public static string Constr = "";
        public DataLayer()
        {
            Constr = ConfigurationManager.AppSettings.Get("Connectionstring");
        }

        public DataTable ExecuteDatatableForProcedure(string procedure, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand command = new SqlCommand(procedure, con);
                    command.CommandType = CommandType.StoredProcedure;
                    if (param != null && param.Length > 0)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            command.Parameters.Add(param[i]);
                        }
                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
            }
            return dt;
        }
        public DataTable ExecuteDataTableWithProccedure(string procedure, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand command = new SqlCommand(procedure, con);
                    command.CommandType = CommandType.StoredProcedure;
                    if (param != null && param.Length > 0)
                    {
                        for (int i = 0; i < param.Length; i++)
                        {
                            command.Parameters.Add(param[i]);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }

        public DataTable ExecuteDataTableWithoutParameter(string procedure)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand command = new SqlCommand(procedure, con);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }

        public DataTable ExecuteQueryWithDataTable(string sql,SqlConnection conn)
        {
            DataTable dt = new DataTable();
                try
                {
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception e)
                {
                    dt = null;
                conn.Close();
                }
                return dt;
        }

        internal DataTable GetStudentReportData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand command = new SqlCommand("SP_StudentReport", con);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }

            }

            return dt;
        }

        internal DataTable GetUsernamePassword(User obj)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand command = new SqlCommand("SP_GetUsernameAndPassword", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", obj.Username);
                    command.Parameters.AddWithValue("@Password", obj.Password);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }

            }

            return dt;
        }

    

        internal DataTable CheckUserExit(string oldpass,string fusername)
        {
            DataTable dt = new DataTable();
            string sql = "",res="";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select ul.UserLogin_Id,ul.UserName,ul.UserPassword from tbl_Userlogin ul where ul.UserName = '" + fusername + "' and  ul.UserPassword = '" + oldpass+"' and ul.IsActive = 1";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                    
                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }

        internal string ChangeUserPassword(User obj)
        {
            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand command = new SqlCommand("SP_ChangeUserPassword", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", obj.Uname);
                    command.Parameters.AddWithValue("@Password", obj.NewPassword);
                    command.Parameters.AddWithValue("@UserId", obj.UserId);
                    if (HttpContext.Current.Session["AddedBy"] == null)
                    {
                        command.Parameters.AddWithValue("@AddedBy", "577");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@AddedBy", HttpContext.Current.Session["AddedBy"]); ;
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                  
                        res = "True";
                 
                }
                catch (Exception e)
                {
                    dt = null;
                    res = "False";
                    con.Close();
                }
                return res;

            }
        }

        #region Standard Saving
        internal string SaveStandardName(AdminCommonClass obj)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[2];
                sp[0] = new SqlParameter("@StandardName", obj.StandardName);
                sp[1] = new SqlParameter("@AddedBy", HttpContext.Current.Session["AddedBy"]);
                dt = ExecuteDataTableWithProccedure("SP_SaveStandardName", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }

        internal DataTable GetStandardNameData()
        {
            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select Standard_Id,StandardName from tbl_Standard where IsActive=1";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }

        internal string UpdateStandardName(AdminCommonClass obj)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[3];
                sp[0] = new SqlParameter("@StandardName", obj.StandardName);
                sp[1] = new SqlParameter("@StandardId", obj.StandardId);
                sp[2] = new SqlParameter("@AddedBy", HttpContext.Current.Session["AddedBy"]);
                dt = ExecuteDataTableWithProccedure("SP_UpdateStandardNameAccToSID", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }

        internal string DeleteStandardName(string stdid)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@StandardId", stdid);
                dt = ExecuteDataTableWithProccedure("SP_DeleteStandardName", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }
        #endregion

        internal DataTable GetDropdownStandardName()
        {
            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select Standard_Id,StandardName from tbl_standard where IsActive=1";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }
        internal DataTable GetDivisionDrop(string stdid)
        {
            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select Division_Id,DivisionName  from tbl_Division where StandardId ='"+stdid+ "' and IsActive=1 order by DivisionName";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }
        #region Division Saving
        internal string SaveDivisionNameDetails(AdminCommonClass obj)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[4];
                sp[0] = new SqlParameter("@DivisionName", obj.DivisionName);
                sp[1] = new SqlParameter("@Seat", obj.Seat);
                sp[2] = new SqlParameter("@StandardId", obj.StandardId);
                sp[3] = new SqlParameter("@AddedBy", HttpContext.Current.Session["AddedBy"]);
                dt = ExecuteDataTableWithProccedure("SP_InsertDivisionDetails", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }

        internal DataTable GetDivisionData()
        {
            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select Division_Id,DivisionName,Seat,div.StandardId,StandardName from tbl_Division div left join tbl_Standard std on std.Standard_Id=div.StandardId where div.IsActive=1 and std.IsActive=1  order by StandardName";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }

        internal string UpdateDivisionName(AdminCommonClass obj)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[5];
                sp[0] = new SqlParameter("@DivisionName", obj.DivisionName);
                sp[1] = new SqlParameter("@DivisionId", obj.DivisionId);
                sp[2] = new SqlParameter("@Seat", obj.Seat);
                sp[3] = new SqlParameter("@StandardId", obj.StandardId);
                sp[4] = new SqlParameter("@AddedBy", HttpContext.Current.Session["AddedBy"]);
                dt = ExecuteDataTableWithProccedure("SP_UpdateDivisionData", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }

        internal string DeleteDivisionName(string stdid)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@DivisionId", stdid);
                dt = ExecuteDataTableWithProccedure("SP_DeleteDivisionDetails", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }
        #endregion

        #region AddStaff
        internal string SaveStaffDetails(HttpPostedFileBase file, PersonDetails obj)
        {
            string sql = "", sql1 = "", sql2 = "", res="";
            int PersonId = 0;
            int loginId = 0;
            string FilePath = "";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"), Path.GetFileName(file.FileName));
                       file.SaveAs(path);
                        FilePath = path;
                        obj.FileName = file.FileName;
                        obj.FileLength = file.ContentLength;

                    }
                    sql = "set dateformat dmy; Insert into tbl_Person(Pname,PFname,StdName,Email,Mobile,FilePath,FileName,FileLength,Qualification,Address,City,Pincode,Gender,AddedOn,AddedBy,IsActive) values('" + obj.PersonName + "','" + obj.FatherName + "','" + obj.Standard + "','" + obj.Emailid + "','" + obj.MobileNo + "','" + FilePath + "','" + obj.FileName + "','" + obj.FileLength + "','" + obj.Qualification + "','" + obj.Address + "','" + obj.City + "','" + obj.Pincode + "','" + obj.Gender + "',GETUTCDATE(),'" + HttpContext.Current.Session["AddedBy"] + "','1')Select Scope_Identity();";
                    dt = ExecuteQueryWithDataTable(sql, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        PersonId = Convert.ToInt32(dt.Rows[0][0]);
                    }
                    if (PersonId > 0)
                    {
                        sql1 = "set dateformat dmy; Insert into tbl_UserLogin(PersonId,UserName,UserPassword,IsActive,AddedOn,AddedBy) values('" + PersonId + "','" + obj.Username + "','" + obj.Password + "','1',GETUTCDATE(),'" + HttpContext.Current.Session["AddedBy"] + "')Select Scope_Identity()";
                        dt = ExecuteQueryWithDataTable(sql1, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            loginId = Convert.ToInt32(dt.Rows[0][0]);
                        }
                        if (loginId>0)
                        {
                            sql2 = "set dateformat dmy; Insert into tbl_PersonRoles(PersonId,UserLoginId,RoleName,RoleType,IsActive,AddedOn,AddedBy) values('"+PersonId+"','"+loginId+"','Teacher (Staff)','4','1',GETUTCDATE(),'" + HttpContext.Current.Session["AddedBy"] + "')";
                            dt = ExecuteQueryWithDataTable(sql2, con);
                        }
                    }
                    res = "True";
                }
                catch (Exception e)
                {
                    res = "False";
                    dt = null;
                }

            }
            return res;
        }

        internal DataTable GetStaffProfileImage()
        {
            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select FilePath,FileName,FileLength from tbl_Person p left join tbl_PersonRoles pr on p.Person_Id=pr.PersonId left join tbl_UserLogin ul on p.Person_Id=ul.PersonId where p.IsActive=1 and pr.IsActive=1 and ul.IsActive=1";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;

            }
        }

        internal DataTable GetAllStaffDetailsAnalytics()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataTableWithoutParameter("SP_GetStaffDetailsAnalytics");
            }
            catch (Exception e)
            {
                dt = null;
            }
            return dt;
        }

        internal string DeletePersonRecord(string personid)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@personid", personid);
                dt = ExecuteDataTableWithProccedure("SP_DeletePersonRecord", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }

        internal string ChngePasswd(User obj)
        {
            string res = "";
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[4];
                sp[0] = new SqlParameter("@Username", obj.Username);
                sp[1] = new SqlParameter("@Password", obj.Password);
                sp[2] = new SqlParameter("@UserId", HttpContext.Current.Session["UserId"]);
                sp[3] = new SqlParameter("@AddedBy", HttpContext.Current.Session["AddedBy"]);

                dt = ExecuteDataTableWithProccedure("SP_ChangeUserPassword", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";
            }
            return res;
        }
        #endregion



        #region StaffDetails
        internal DataTable GetStaffProfileDetails(string personid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@PersonId",Convert.ToInt32(personid));
                dt = ExecuteDataTableWithProccedure("SP_GetStaffDetailsAccToPersonId", sp);
            }
            catch (Exception e)
            {
                dt = null;
            }
            return dt;
        }

        internal string SaveStaffUpdatedDetails(PersonDetails obj,HttpPostedFileBase file)
        {
            string res = "", FilePath="", filename="", fpath="";
            DataTable dt = new DataTable();
            try
            {
                if (file!=null && file.ContentLength>0)
                {
                    string ImageName = System.IO.Path.GetFileName(file.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"), ImageName);
                    string subpath = "~/UploadedFiles/Staff";
                    string[] filePaths = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"));

                    for (int j = 0; j < filePaths.Length; j++)
                    {
                        filename = Path.GetFileName(filePaths[j]);
                        fpath = Path.GetFullPath(filePaths[j]);
                        if (filename == ImageName && fpath == path)
                        {
                            subpath = "~/UploadedFiles/Staff" + "/" + ImageName;
                            System.IO.File.Delete(HttpContext.Current.Server.MapPath(subpath));
                        }
                    }
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"), ImageName);
                    file.SaveAs(path);
                    FilePath = path;
                    obj.FileName = file.FileName;
                    obj.FileLength = file.ContentLength;
                }


                SqlParameter[] sp = new SqlParameter[10];
                sp[0] = new SqlParameter("@PersonId",obj.PersonId);
                sp[1] = new SqlParameter("@Email", obj.Emailid);
                sp[2] = new SqlParameter("@Mobile", obj.MobileNo);
                sp[3] = new SqlParameter("@Address", obj.Address);
                sp[4] = new SqlParameter("@City", obj.City);
                sp[5] = new SqlParameter("@Pincode", obj.Pincode);
                sp[6] = new SqlParameter("@FilePath", FilePath);
                sp[7] = new SqlParameter("@FileName", obj.FileName);
                sp[8] = new SqlParameter("@FileLength", obj.FileLength);
                sp[9] = new SqlParameter("@UpdatedBy", HttpContext.Current.Session["AddedBy"]);
                dt = ExecuteDataTableWithProccedure("SP_UpdateStaffDetails", sp);
                res = "True";
            }
            catch (Exception e)
            {
                dt = null;
                res = "False";

            }
            return res;
        }
        #endregion

        #region Add Student
        internal DataTable GetStandardNameAndStandardId(string personid)
        {

            DataTable dt = new DataTable();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select std.StandardName,std.Standard_Id from tbl_Person p inner join tbl_UserLogin ul on p.Person_Id=ul.PersonId left join tbl_PersonRoles pr on p.Person_Id=pr.PersonId left join tbl_Standard std on p.StdName=std.Standard_Id where p.Person_Id='" + personid + "' and p.IsActive=1 and ul.IsActive=1 and pr.IsActive=1";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);

                }
                catch (Exception e)
                {
                    dt = null;
                    con.Close();
                }
                return dt;
            }
        }

        internal string GetSeatDetailsAccToDivsion(string divid) 
        {

            DataSet ds = new DataSet();
            string sql = "", res = "";
            using (SqlConnection con = new SqlConnection(Constr))
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    sql = "select [TotalSeat]=tbl_Division.Seat,[Total Admitted]=COUNT(tbl_Person.DivisionId), [Student Remaining]=(tbl_Division.Seat-COUNT(tbl_Person.DivisionId)) From tbl_Division LEFT JOIN tbl_Person ON tbl_Person.DivisionId=tbl_Division.Division_Id where Division_Id='"+divid+"' AND tbl_Division.IsActive=1  GROUP BY tbl_Person.DivisionId ,tbl_Division.Seat;";
                    sql += "select Top (1) StudentRollNo=SUBSTRING(Std_RollNo,1,3)+'0'+CONVERT(char(20), CONVERT(int, SUBSTRING(Std_RollNo,4,5))+1) From tbl_Person where IsActive=1  order by Std_RollNo desc";
                    SqlCommand command = new SqlCommand(sql, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
          
                    res = "True"+"|"+ds.Tables[0].Rows[0]["TotalSeat"].ToString()+"|"+ ds.Tables[0].Rows[0]["Total Admitted"].ToString()+"|"+ ds.Tables[0].Rows[0]["Student Remaining"].ToString() + "|" + ds.Tables[1].Rows[0]["StudentRollNo"].ToString();
                }
                catch (Exception e)
                {
                    res ="False";
                    con.Close();
                }
                return res;
            }
        }

        internal string SaveAddStudent(PersonDetails obj, HttpPostedFileBase file)
        {
            string sql = "", sql1 = "", sql2 = "", res = "";
            int PersonId = 0;
            int loginId = 0;
            string FilePath = "";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Constr))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/Staff"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        FilePath = path;
                        obj.FileName = file.FileName;
                        obj.FileLength = file.ContentLength;

                    }
                    sql = "set dateformat dmy; Insert into tbl_Person(Pname,PFname,StdName,Email,Mobile,FilePath,FileName,FileLength,Qualification,Address,City,Pincode,Gender,AddedOn,AddedBy,IsActive,PersonType,DivisionId,Std_RollNo) values('" + obj.PersonName + "','" + obj.FatherName + "','" + obj.Standard + "','" + obj.Emailid + "','" + obj.MobileNo + "','" + FilePath + "','" + obj.FileName + "','" + obj.FileLength + "','" + obj.Qualification + "','" + obj.Address + "','" + obj.City + "','" + obj.Pincode + "','" + obj.Gender + "',GETUTCDATE(),'" + HttpContext.Current.Session["AddedBy"] + "','1','Student','"+obj.DivisionId+ "','" + obj.RollNo + "')Select Scope_Identity();";
                    dt = ExecuteQueryWithDataTable(sql, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        PersonId = Convert.ToInt32(dt.Rows[0][0]);
                    }
                    if (PersonId > 0)
                    {
                        sql1 = "set dateformat dmy; Insert into tbl_UserLogin(PersonId,UserName,UserPassword,IsActive,AddedOn,AddedBy) values('" + PersonId + "','" + obj.UserStudent + "','" + obj.PasswordStudent + "','1',GETUTCDATE(),'" + HttpContext.Current.Session["AddedBy"] + "')Select Scope_Identity()";
                        dt = ExecuteQueryWithDataTable(sql1, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            loginId = Convert.ToInt32(dt.Rows[0][0]);
                        }
                        if (loginId > 0)
                        {
                            sql2 = "set dateformat dmy; Insert into tbl_PersonRoles(PersonId,UserLoginId,RoleName,RoleType,IsActive,AddedOn,AddedBy) values('" + PersonId + "','" + loginId + "','Student','6','1',GETUTCDATE(),'" + HttpContext.Current.Session["AddedBy"] + "')";
                            dt = ExecuteQueryWithDataTable(sql2, con);
                        }
                    }
                    res = "True";
                }
                catch (Exception e)
                {
                    res = "False";
                    dt = null;
                }

            }
            return res;
        }
        #endregion

        #region Attendance
        internal DataTable GetStudentAttendanceData(Attendance obj)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@PersonId",obj.DivisionId);
                dt = ExecuteDataTableWithProccedure("SP_GetStudentAttendanceAccToDiv", sp);
            }
            catch (Exception e)
            {
                dt = null;
            }
            return dt;
        }

        internal DataTable GetStudentData()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] sp = new SqlParameter[1];
                sp[0] = new SqlParameter("@PersonId", HttpContext.Current.Session["PersonId"]);
                dt = ExecuteDataTableWithProccedure("SP_GetStudentAttendanceAccToDiv", sp);
            }
            catch (Exception e)
            {
                dt = null;
            }
            return dt;
        }
        #endregion
    }
}
