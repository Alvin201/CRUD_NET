using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Image_System.DTO;
using Image_System.Models;

namespace Image_System.Controllers
{

    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            DateTime lastpassworddate = Convert.ToDateTime(Session["LastPasswordChangedDate"]);
            if (Session["NIK"] != null && DateTime.Now.Date == lastpassworddate)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {      
                return View();
            }
            
        }


        //[HttpPost]
        //public ActionResult Index(DTO.LoginDTO login)
        //{

        //    Models.AccessLogin UserLogin = new Models.AccessLogin();


        //    if (UserLogin.CheckUserLogin(login) > 0)
        //    {
        //        Session["Username"] = login.Username.ToString();
        //        string Username = login.Username;

        //        Models.AccessLogin udb = new Models.AccessLogin();
        //        DTO.LoginDTO clogin = udb.GetData(login.Username);

        //        if (clogin != null)
        //        {
        //            Session["Username"] = clogin.Username.ToString();
        //            return RedirectToAction("Index", "Home");
        //        }

        //    }
        //    TempData["Fail"] = "Username or Password is Incorrect!";
        //    return View(login);
        //}

        [HttpPost]
        public ActionResult Index(DTO.LoginDTO login)
        {
            if (login.NIK == null)
            {
                TempData["Fail"] = "Please Input NIK";
                return View(login);
            }
            else if (login.Password == null)
            {
                TempData["Fail"] = "Please Input Password";
                return View(login);
            }
         
            else
            {
                Models.AccessLogin udb = new Models.AccessLogin();
                var result = udb.CheckUserLogin(login);

              
                switch (result.Status)
                    {

                        case LoginStatus.Authorized:
                      
                            DTO.LoginDTO clogin = udb.GetData(login.NIK);

                            //DateTime expiryDate = DateTime.Today.AddDays(30);
                            DateTime lastpassworddate = Convert.ToDateTime(clogin.LastPasswordChangedDate);

                        //if ((lastpassworddate.Date - DateTime.Now.Date).TotalDays > 30 || (lastpassworddate.Date - DateTime.Now.Date).TotalDays < 0)
                        if ((lastpassworddate.Date - DateTime.Now.Date).TotalDays == 0)
                        {
                            FormsAuthentication.SetAuthCookie(clogin.NIK, clogin.RememberMe);
                            Session["NIK"] = clogin.NIK.ToString();
                            Session["Username"] = clogin.Username.ToString();
                            Session["Password"] = clogin.Password.ToString();
                            Session["LastPasswordChangedDate"] = Convert.ToDateTime(clogin.LastPasswordChangedDate);
                            Session["RoleID"] = Convert.ToInt32(clogin.RoleID);
                            Session["MenuID"] = Convert.ToInt32(clogin.MenuID);
                            TempData["Message"] = "Password Expired, Please Change Password";
                            return RedirectToAction("ChangePassword_Expired", "Login");
                        }

                        if (login.RememberMe == true)
                        {
                            HttpCookie cookie = new HttpCookie("Login");
                            cookie.Values.Add("NIK", clogin.NIK);
                            cookie.Values.Add("Password", clogin.Password);
                            cookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(cookie);
                        }

                            FormsAuthentication.SetAuthCookie(clogin.NIK, clogin.RememberMe);
                            Session["NIK"] = clogin.NIK.ToString();
                            Session["Username"] = clogin.Username.ToString();
                            Session["Password"] = clogin.Password.ToString();
                            Session["RoleID"] = Convert.ToInt32(clogin.RoleID);
                            Session["MenuID"] = Convert.ToInt32(clogin.MenuID);
                        Session["LastPasswordChangedDate"] = Convert.ToDateTime(clogin.LastPasswordChangedDate);

                       
                            return RedirectToAction("Index", "Home");

                       
                    case LoginStatus.InvalidCredentials:
                        if (result.AttemptsRemaining < 5)
                            TempData["Fail"] = "ID or Password is Incorrect, Attempts remaining:   " + result.AttemptsRemaining;
                        else if (result.RoleID == 0 && result.MenuID == 0)
                            TempData["Fail"] = " ID not have role menu or not exist"; //insert in table [RoleMenuSetup]
                        break;

                        case LoginStatus.Suspended:
                                TempData["Fail"] = "Account Suspended!";
                                break;
                        }


                    return View(login);
                }
        }

        public ActionResult ChangePassword_Expired(string NIK = "")
        {
            NIK = Session["NIK"].ToString();

            Models.AccessLogin db = new Models.AccessLogin();
            DTO.ChangePasswordExpiredDTO login = db.FindUsers.FirstOrDefault(usr => usr.NIK == NIK);
            return View("ChangePassword_Expired", login);
        }

        [HttpPost]
        [ActionName("POST_ChangePasswordExpired")]
        public ActionResult Save_ChangePasswordExpired(string NIK, DTO.ChangePasswordExpiredDTO login)
        {
            Models.AccessLogin db = new Models.AccessLogin();

            if (login.Password != login.ConfirmPassword)
            {
                TempData["Message"] = "New password and confirm password does not match";
                return View("ChangePassword_Expired", login);
            }

            if (ModelState.IsValid && login.Password == login.ConfirmPassword)
            {
                db.Update_day(login);
                TempData["Fail"] = "Password has been updated, Please Login";
                return RedirectToAction("Index", "Login");  
            }
            TempData["Message"] = "Please Fill All the Fields";
            return View("ChangePassword_Expired", login);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");

        }

    }
}