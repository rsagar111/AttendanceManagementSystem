using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Models
{
    
    public class AdminCommonClass
    {
        BusinessLayer objBus = new BusinessLayer();
        public string StandardId { get; set; }
        public string StandardName { get; set; }

        public string DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string Seat { get; set; }
        
      public List<AdminCommonClass> StandardDropList { get; set; }

    }
    
}