using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Image_System.Controllers
{
    [Helpers.SessionTime]
    [SessionExpire]
    public class ChangePasswordController : Controller
    {
        public ActionResult Index(int NIK = 0)
        {
            NIK = Convert.ToInt32(Session["NIK"]);

            Models.ChangePasswordModels db = new Models.ChangePasswordModels();
            DTO.ChangePasswordDTO user = db.Users.Single(usr => usr.NIK == NIK);
            return View(user);
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult Save_ChangePassword(int nik, DTO.ChangePasswordDTO user)
        {
            Models.ChangePasswordModels db = new Models.ChangePasswordModels();

            if (user.Password != user.ConfirmPassword)
            {
                TempData["Message"] = "New password and confirm password does not match";
                //return Json(new { success = false, issue = user, errors = ModelState.Values.Where(i => i.Errors.Count > 0) });
                return RedirectToAction("Index", "ChangePassword");
            }
            if (ModelState.IsValid)
            {
                db.Update(user);
                TempData["Message"] = "Password Updated Successfully";
                //return Json(new { success = true, issue = user }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index", "ChangePassword");

            }
            TempData["Message"] = "Please Fill All the Fields";
            //return Json(new { success = false, issue = user, errors = ModelState.Values.Where(i => i.Errors.Count > 0) });
            return RedirectToAction("Index", "ChangePassword");


        }
    }
}