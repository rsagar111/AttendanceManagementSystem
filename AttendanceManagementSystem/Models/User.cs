using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceManagementSystem.Models
{

    public class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }

        public string Uname { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string IsReturn { get; set; }

    }
}