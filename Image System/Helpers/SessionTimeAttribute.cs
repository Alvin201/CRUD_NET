using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Image_System.Helpers
{
    public class SessionTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            DateTime lastpassworddate = Convert.ToDateTime(HttpContext.Current.Session["LastPasswordChangedDate"]);
            if    ((lastpassworddate.Date - DateTime.Now.Date).TotalDays == 0)
            {
                filterContext.Result = new RedirectResult("~/Login/ChangePassword_Expired");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}