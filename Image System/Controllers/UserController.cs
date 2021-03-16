using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Image_System.DTO;
using Image_System.Helpers;
using Image_System.Models;

namespace Image_System.Controllers
{
    [SessionTime]
    [SessionExpire]
    public class UserController : Controller
    {
        //[AccessRole(Roles = "ADMIN")]
        //[Authorize(Roles = "1")]
        [AccessRole(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View();
        }

        private List<DTO.UserDTO> SortTableData(string sortColumn, string sortColumnDir, List<DTO.UserDTO> data)
        {
            List<DTO.UserDTO> lst = new List<DTO.UserDTO>();
            try
            {
                switch (sortColumn)
                {
                    case "0":
                        lst = sortColumnDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.NIK).ToList()
                                                                                                 : data.OrderBy(p => p.NIK).ToList();
                        break;
                    case "1":
                        lst = sortColumnDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Username).ToList()
                                                                                                 : data.OrderBy(p => p.Username).ToList();
                        break;
                    default:
                        lst = sortColumnDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CurrentStatus).ToList()
                                                                                                 : data.OrderBy(p => p.CurrentStatus).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }

        [HttpPost]
        public ActionResult LoadData(int draw, string start, string length, string sortColumn, string sortColumnDir, string searchActive)
        {
            // Search Value  
            //string search = Request.Form.GetValues("search[value]").FirstOrDefault();
            searchActive = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();

            //Find paging info
            start = Request.Form.GetValues("start").FirstOrDefault();
            length = Request.Form.GetValues("length").FirstOrDefault();

            // Sort columns value  
            sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            if (Request.IsAjaxRequest())
            {
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordFilteredTotal = 0;

                Models.UserModels du = new Models.UserModels();
                // Get document Records. 
                var users = du.GetList.ToList();

                // 1. Searching  
                //if (!string.IsNullOrEmpty(searchActive))
                //{
                //    users = users.Where(u => u.CurrentStatus.ToUpper().Contains(searchActive)).ToList();
                //}




                // 2. Get the total record count  
                recordsTotal = users.Count();


                // 3. Sorting  
                var filteredData = SortTableData(sortColumn, sortColumnDir, users);

       
                // 4. Filtering  
                filteredData = filteredData.Skip(skip).Take(pageSize).ToList();
                // 5. Get the filtered record count  
                recordFilteredTotal = filteredData.Count();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordFilteredTotal, data = filteredData }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            Models.UserModels udb = new Models.UserModels();
            DTO.UserDTO a = udb.GetList.Single(usr => usr.NIK == id);
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert([Bind] DTO.UserDTO a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.UserModels udb = new Models.UserModels();
                    udb.Insert(a);
                    TempData["Message"] = "Data Saved Successfully";
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    TempData["Message"] = "Error " + ex.Message;
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Models.UserModels udb = new Models.UserModels();
            DTO.UserDTO a = udb.GetList.Single(usr => usr.NIK == id);
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(DTO.UserDTO a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.UserModels udb = new Models.UserModels();
                    udb.Update(a);
                    TempData["Message"] = "Data Updated Successfully";
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    TempData["Message"] = "Error" + ex.Message;
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Delete(int NIK =0)
        {
            try
            {
                Models.UserModels udb = new Models.UserModels();
                udb.Delete(NIK);
                TempData["Message"] = "Data Deleted Successfully";
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                TempData["Message"] = "Error" + ex.Message;
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);

        }


    }
}