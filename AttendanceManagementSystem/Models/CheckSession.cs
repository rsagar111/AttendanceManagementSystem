using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace AttendanceManagementSystem.Models
{
    public class CheckSessionAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["objuser"]==null)
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(){{ "Controller","Account"},{ "action","Login"}});
               
            base.OnActionExecuting(filterContext);
        }
    }
}