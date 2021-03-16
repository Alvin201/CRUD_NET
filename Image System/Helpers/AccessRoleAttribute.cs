using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Image_System.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AccessRoleAttribute : AuthorizeAttribute
    {
        /// <summary>  
        /// Check for Authorization  
        /// </summary>  
        /// <param name="filterContext"></param>  
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            HandleUnauthorizedRequest(filterContext);
        }

        //public string PermittedRoles;

        //public AccessRoleAttribute(string roles)
        //{
        //    PermittedRoles = roles;
        //}

        //protected override bool OnAuthorization(AuthorizationContext filterContext)
        //{
        //    var user = filterContext.User;

        //    var NIK = filterContext.User.Identity.IsAuthenticated;
        //    var roles = System.Web.Security.Roles.GetRolesForUser(NIK.ToString());

        //    if (user.IsInRole(PermittedRoles))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        protected override void HandleUnauthorizedRequest(
               AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Unauthorised", action = "Index" })
                );
            }
        }

        /// <summary>  
        /// Method to check if the user is Authorized or not  
        /// if yes continue to perform the action else redirect to error page  
        /// </summary>  
        /// <param name = "filterContext" ></ param>
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new RedirectToRouteResult(
        //       new RouteValueDictionary
        //       {
        //            { "controller", "Unauthorised" },
        //            { "action", "Index" }
        //       });
 
        //}

        /// <summary>  
        /// Method to check if the user is Authorized or not  
        /// if yes continue to perform the action else redirect to error page  
        /// </summary>  
        ///// <param name="filterContext"></param>  
        //private void IsUserAuthorized(AuthorizationContext filterContext)
        //{
        //    // If the Result returns null then the user is Authorized   
        //    if (filterContext.Result == null)
        //        return;

        //    //If the user is Un-Authorized then Navigate to Auth Failed View   
        //    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {

        //        // var result = new ViewResult { ViewName = View };  
        //        var vr = new ViewResult();
        //        vr.ViewName = View;

        //        ViewDataDictionary dict = new ViewDataDictionary();
        //        dict.Add("Message", "Sorry you are not Authorized to View this Page");

        //        vr.ViewData = dict;

        //        var result = vr;

        //        filterContext.Result = result;
        //    }
        //}
    }
}