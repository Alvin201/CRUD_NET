using Image_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Image_System.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult SideMenu()
        {
            string NIK = "";
            if (Session["NIK"] != null)
            {
                NIK = Session["NIK"].ToString();
            }
            else
            {
                return RedirectToAction("Home", "Login");
            }

            MenuModels db = new MenuModels();
            List<DTO.MenuDTO> menus = db.GetList(NIK).ToList();
            return PartialView("_Menu", menus);
        }

    }
}