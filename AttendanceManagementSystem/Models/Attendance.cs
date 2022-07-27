using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AttendanceManagementSystem.Models
{
    public class Attendance
    {
        BusinessLayer objBus = new BusinessLayer();
        public string Student_Id { get; set; }
        public string RollNo { get; set; }
        public string StandardId { get; set; }
        public string StandardName { get; set; }
        public string DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string StudentName { get; set; }
        public string AbsentPresent { get; set; }
        public string AttDate { get; set; }
        public string FilePath { get; set; }
        public List<Attendance> AttendanceList { get; set; }

        internal Attendance GetStudentAttendanceData(Attendance obj)
        {
            obj.AttendanceList = new List<Attendance>();
            DataTable dt = new DataTable();
            dt = objBus.GetStudentAttendanceData(obj);
            if (dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Attendance innobj = new Attendance();
                    innobj.RollNo = dt.Rows[i]["Std_RollNo"].ToString();
                    innobj.StudentName = dt.Rows[i]["StudentName"].ToString();
                    innobj.Student_Id = dt.Rows[i]["StudezntId"].ToString();
                    innobj.FilePath = dt.Rows[i]["StudezntId"].ToString();
                    obj.AttendanceList.Add(innobj);
                }
                


            }
            return obj;
        }
    }
}