using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AttendanceManagementSystem.Models
{
    public class AllClasses
    {
      
        public class Attendance
        {
            BusinessLayer bl = new BusinessLayer();
            public int Attendance_Id { get; set; }
            public int StudentID { get; set; }
            public string Student_Rno { get; set; }
            public string Attendance_Status { get; set; }
            public string Attendance_Date { get; set; }
            public string Attendance_Description { get; set; }
            public string Attendance_DivisionId { get; set; }
            public string Attendance_StandardId { get; set; }

            void GetInitialData()
            {
                DataTable dt = bl.GetStudentData();
            }
        }

        public class StudentLeave
        {
            public int Leave_Id { get; set; }
            public int Leave_NoOfDays { get; set; }
            public int StudentID { get; set; }
            public string Student_Rno { get; set; }
            public string Leave_Reason { get; set; }
        }
    }
}