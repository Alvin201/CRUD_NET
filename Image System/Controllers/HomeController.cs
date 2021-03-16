using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using System.Data.OleDb;
using System.Data;
using Image_System.DTO;
using Image_System.Models;
using System.IO.Compression;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using System.Text;

namespace Image_System.Controllers
{

    [Helpers.SessionTime]
    [SessionExpire]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }


        private List<DTO.DocumentUploadDTO> SortTableData(string sortColumn, string sortColumnDir, List<DTO.DocumentUploadDTO> data)
        {
            List<DTO.DocumentUploadDTO> lst = new List<DTO.DocumentUploadDTO>();
            try
            {
                switch (sortColumn)
                {
                    case "0":
                        lst = sortColumnDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ID).ToList()
                                                                                                 : data.OrderBy(p => p.ID).ToList();
                        break;
                    case "1":
                        lst = sortColumnDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FileName).ToList()
                                                                                                 : data.OrderBy(p => p.FileName).ToList();
                        break;
                    default:
                        lst = sortColumnDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ID).ToList()
                                                                                                 : data.OrderBy(p => p.ID).ToList();
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
        public ActionResult LoadData(int draw, string start, string length, string sortColumn, string sortColumnDir)
        {
            try
            {
                // Search Value  
                string searchID = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
                string searchActive = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();

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

                    Models.DocumentUploadModels du = new Models.DocumentUploadModels();
                    // Get document Records. 
                    var datadocument = du.GetList.ToList();

                    // 1. Searching  
                    if (!string.IsNullOrEmpty(searchActive))
                    {
                        datadocument = datadocument.Where(u => u.CurrentStatus.ToUpper().Contains(searchActive)).ToList();
                    }
                    if (!string.IsNullOrEmpty(searchID))
                    {
                        datadocument = datadocument.Where(u => u.ID.ToString().Contains(searchID)).ToList();
                    }



                    // 2. Get the total record count  
                    recordsTotal = datadocument.Count();


                    // 3. Sorting 
                    #region Sorting
                    //var filteredData = datadocument as IEnumerable<DTO.List_Data_Document>;
                    //Func<List_Data_Document, string> orderingFunction = (c => sortColumn == "ID" ? c.ID.ToString() :
                    //                          sortColumn == "FileName" ? c.FileName.ToString() :
                    //                          //sortColumn == "Designation" ? c.Designation :
                    //                          c.FileName);
                    //if (sortColumnDir == "asc")
                    //    filteredData = filteredData.OrderBy(orderingFunction);
                    //else
                    //    filteredData = filteredData.OrderByDescending(orderingFunction);
                    #endregion
                    var filteredData = SortTableData(sortColumn, sortColumnDir, datadocument);

                    // 4. Filtering  
                    filteredData = filteredData.Skip(skip).Take(pageSize).ToList();
                    // 5. Get the filtered record count  
                    recordFilteredTotal = filteredData.Count();
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordFilteredTotal, data = filteredData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create(string id = "")
        {
           Models.DocumentUploadModels udb = new Models.DocumentUploadModels();
           DTO.DocumentUploadDTO a = udb.GetList.Single(doc => doc.ID == id);
           return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Insert([Bind] DTO.DocumentUploadDTO result)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.DocumentUploadModels udb = new Models.DocumentUploadModels();
                    udb.Insert(result);
                    TempData["Message"] = "Data Saved Successfully";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    TempData["Message"] = "Data Saved but " + ex.Message;
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            Models.DocumentUploadModels udb = new Models.DocumentUploadModels();
            DTO.DocumentUploadDTO a = udb.GetList.Single(usr => usr.ID.Equals(id));
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(DTO.DocumentUploadDTO a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.DocumentUploadModels udb = new Models.DocumentUploadModels();
                    udb.Update(a);
                    TempData["Message"] = "Data Updated Successfully";
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
        public ActionResult Delete(string ID = "")
        {
            try
            {
                Models.DocumentUploadModels udb = new Models.DocumentUploadModels();
                udb.Delete(ID);
                udb.InsertLogDelete(ID);
                TempData["Message"] = "Data Deleted Successfully";
               
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                TempData["Message"] = "Error" + ex.Message;
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download_TXT()
        {
            Models.DocumentUploadModels db = new Models.DocumentUploadModels();
            List<object> models = (from model in db.GetList.ToList()
                                      select new[] { model.ID.ToString(),
                                                            model.FileName.ToString(),
                                                            model.CurrentStatus.ToString()
                                }).ToList<object>();

            //Insert the Column Names.
            models.Insert(0, new string[3] { "ID", "FileName", "CurrentStatus" });

           
            StringBuilder sb = new StringBuilder();

            //output session
            string NIK = "ID User : " + Convert.ToString(System.Web.HttpContext.Current.Session["NIK"] as String);
            string line_session = String.Format("{0,1}", NIK);
            
            //output time
            var time = "Print Date : " + DateTime.Now;
            string line_time = String.Format("{0,100}", time);

            //concate session and time same line
            string join = String.Concat(NIK, line_time);
            sb.AppendLine(join);


            string symbol_session = String.Format("{0,1}", "==================");
            string symbol_date = String.Format("{0,100}", "================================");
            string join_symbol = String.Join(" ", symbol_session, symbol_date);
            sb.AppendLine(join_symbol);

            for (int i = 0; i < models.Count; i++)
            {
                string[] model = (string[])models[i];
                for (int j = 0; j < model.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(model[j] + '|');
                }

                //Append new line character.
                sb.Append("\n");         
            }


            var datadocument = db.GetList.ToList();
            int recordsTotal = datadocument.Count();
            sb.Append("\n");
            var total_data = "Total Data: " + recordsTotal;
            sb.Append(total_data).AppendLine();


            // format filename
            var fileName = "ListDoc_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/txt", fileName);
            }


        public ActionResult Download_XLS()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Models.DocumentUploadModels db = new Models.DocumentUploadModels();
            List<DTO.DocumentUploadDTO> models = db.GetList.ToList();

            #region Style
            //.Style.Font.Color.SetColor(Color.Red);
            //.Style.Fill.BackgroundColor.SetColor(Color.Aquamarine);
            //.Style.Font.Bold = true;
            //.Merge = true;
            //.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //.Style.Font.Size = 22;
            #endregion style

            using (ExcelPackage exl = new ExcelPackage())
            {
                ExcelWorksheet sheet = exl.Workbook.Worksheets.Add("Sheet1");

                //protect the whole workbook  
                exl.Encryption.Password = "alvin";

                sheet.Cells.Style.Font.Size = 10;

                sheet.Cells["A1"].Value = "Master Data Document";
                sheet.Cells["A1:C3"].Merge = true; //Merge columns start and end range
                sheet.Cells["A1:C3"].Style.Font.Bold = true; //Font should be bold
                sheet.Cells["A1:C3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Alignment is center
                sheet.Cells["A1:C3"].Style.Font.Size = 22;
                sheet.Cells["A1:C3"].AutoFitColumns();


                sheet.Cells["E2"].Value = "ID User : " + Convert.ToString(System.Web.HttpContext.Current.Session["NIK"] as String);
                sheet.Cells["E3"].Value = "User : " + Convert.ToString(System.Web.HttpContext.Current.Session["Username"] as String);
                sheet.Cells["F2"].Value = "Print Date : " + DateTime.Now.ToString("yyyy-MM-dd");

                
                sheet.Cells["E2:F4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                sheet.Cells["E2:F4"].Style.Font.Size = 12;
                sheet.Cells["E2:F4"].Style.Font.Italic = true; //Font should be italic
                sheet.Cells["E2:F4"].AutoFitColumns();


                //start row parse data
                int iRow = 8;
                
                    //value parse data
                    foreach (DTO.DocumentUploadDTO model in models)
                    {
                        sheet.Cells[iRow, 1].Value = model.ID;
                        sheet.Cells[iRow, 2].Value = model.FileName;
                        sheet.Cells[iRow, 3].Value = model.CurrentStatus;
                        iRow = iRow + 1;
                
                    }

                
                //Count Total data with FORMULA
                sheet.Cells[iRow + 1, 2, iRow + 1, 3].Style.Fill.BackgroundColor.SetColor(Color.Aquamarine);
                sheet.Cells[iRow + 1, 2].Value = "Total Data";
                sheet.Cells[iRow + 1, 3].Formula = string.Format("=ROWS(A8:C{0})", iRow - 1);

                //sheet.Cells["E4"].Formula = string.Format("=ROWS(A8:C{0})", iRow - 1);
                // sheet.Cells[iRow + 1, 3].Formula = "=ROWS(" + sheet.Cells[iRow, 1].Value + ":" + sheet.Cells[iRow, 3].Value + ")";

                //count data without formula
                //var datadocument = db.GetList.ToList();
                //int recordsTotal = datadocument.Count();
                //sheet.Cells["E4"].Value = "Total Data : " + recordsTotal;




                // format position columns
                sheet.Cells["A7"].Value = "Code";
                sheet.Cells["B7"].Value = "Filename";
                sheet.Cells["C7"].Value = "Current Status";
                sheet.Column(1).Width = 30;


                int totalRow = sheet.Dimension.End.Row;
                //int totalCol = sheet.Dimension.End.Column;

                ExcelRange modelTable = sheet.Cells[7, 1, totalRow, 3];
                modelTable.Style.Numberformat.Format = "@";
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;




                // format cells - add colors    7,A7,7,total kolom
                ExcelRange rgHead = sheet.Cells[7, 1, 7, 3];
                rgHead.Style.WrapText = true;
                rgHead.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                rgHead.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                rgHead.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rgHead.Style.Fill.BackgroundColor.SetColor(Color.Aquamarine);

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


                // format filename
                var fileName = "ListDoc_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx";
                Response.AddHeader("content-disposition", "attachment; filename= " + fileName);
                MemoryStream stream = new MemoryStream(exl.GetAsByteArray());
                Response.OutputStream.Write(stream.ToArray(), 0, stream.ToArray().Length);
                Response.Flush();
                Response.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Upload")]
        public ActionResult Submit_Upload(FormCollection formCollection)
        {
            List<DTO.DocumentUploadDTO> docDTO = new List<DTO.DocumentUploadDTO>();

            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["file_Uploader"];
                    string filesavepath = "";
                    var uploadedFile = Request.Files[0];
                    string fileName = Path.GetFileName(file.FileName);
                    filesavepath = Server.MapPath("~/App_Data/UploadedFiles/" + fileName);



                    if ((file != null) && (file.ContentLength > 0)) //&& !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileextension = System.IO.Path.GetExtension(uploadedFile.FileName);
                        {
                            #region XLS
                            if (fileextension == ".xls" || fileextension == ".xlsx")
                            {
                              
                                if (System.IO.File.Exists(filesavepath))
                                {
                                    System.IO.File.Delete(filesavepath);
                                }
                                uploadedFile.SaveAs(filesavepath);

                                string ExcelConnStr = string.Empty;

                                if (fileextension == ".xlsx")
                                {
                                    ExcelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filesavepath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=2\"";
                                }


                                if (fileextension == ".xls")
                                {
                                    ExcelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filesavepath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=2\"";
                                }

                                string fileContentType = file.ContentType;
                                byte[] fileBytes = new byte[file.ContentLength];
                                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                string password_excel = "alvin";
                                using (var package = new ExcelPackage(new FileInfo(filesavepath), password_excel)) //file.InputStream))
                                {

                                    var currentSheet = package.Workbook.Worksheets;
                                    var workSheet = currentSheet.First();
                                    var noOfCol = workSheet.Dimension.End.Column;
                                    var noOfRow = workSheet.Dimension.End.Row;
                                    
                                    for (int rowIterator = 8; rowIterator <= noOfRow - 2; rowIterator++)
                                    {
                                        DTO.DocumentUploadDTO doc = new DTO.DocumentUploadDTO();
                                        doc.ID = workSheet.Cells[rowIterator, 1].Value.ToString();
                                        doc.FileName = workSheet.Cells[rowIterator, 2].Value?.ToString();
                                        doc.CurrentStatus = workSheet.Cells[rowIterator, 3].Value.ToString();
                                        docDTO.Add(doc);
                                    }

                                }

                                Models.DocumentUploadModels db = new Models.DocumentUploadModels();
                                db.Upload(docDTO);

                            }
                            #endregion XLS

                            #region TXT
                            else if (fileextension == ".txt")
                            {
                                file.SaveAs(filesavepath);

                                #region read contents / all text
                                //Read the contents of TXT file.
                                //string csvData = System.IO.File.ReadAllText(filesavepath, Encoding.UTF8);
                                //string[] lines = csvData.Split('\n');

                                //Start Get Data
                                //foreach (string row in lines.Skip(4))
                                //{
                                //    if (!string.IsNullOrEmpty(row))
                                //    {
                                //        docDTO.Add(new DTO.DocumentUploadDTO
                                //        {
                                //            ID = row.Split('|')[0],
                                //            FileName = row.Split('|')[1],
                                //            CurrentStatus = row.Split('|')[2]
                                //        });
                                //    }
                                //}
                                #endregion read contents / all text

                                #region read all lines
                                string[] _lines = System.IO.File.ReadAllLines(filesavepath, Encoding.UTF8);
                                string[] _columns;

                                //Start at index 3 - and keep looping until index Length - 2
                                for (int i = 3; i < _lines.Length - 2; i++)
                                {
                                    _columns = _lines[i].Split('|');

                                    docDTO.Add(new DTO.DocumentUploadDTO
                                    {
                                        ID = _columns[0],
                                        FileName = _columns[1],
                                        CurrentStatus = _columns[2]
                                    });
                                
                                }
                                Models.DocumentUploadModels db = new Models.DocumentUploadModels();
                                db.Upload(docDTO);
                                #endregion read all lines

                            }

                            #endregion TXT
                        }

                    }
                }
                ViewBag.Message = "Upload successful";
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
                return View(docDTO);
            }
        }


        [HttpPost]
        [ActionName("ZipUpload")]
        public ActionResult ZipUpload(HttpPostedFileBase zip)
        {
            var path = Server.MapPath("~/App_Data/UploadedFiles/");

            using (ZipArchive archive = new ZipArchive(zip.InputStream))
            {
                //loop file
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    
                    var namefile = entry.FullName;
                    var extension = Path.GetExtension(entry.FullName);
                    string fullPath = Request.MapPath("~/App_Data/UploadedFiles/" + namefile);

                    //cek file yang sama atau tidak
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    //extract zip
                    if (!string.IsNullOrEmpty(extension))
                    {
                        entry.ExtractToFile(Path.Combine(path, namefile));

                    } else
                    {
                        Directory.CreateDirectory(Path.Combine(path, namefile));
                    }
                }
            }
            ViewBag.Files = Directory.EnumerateFiles(path);
            ViewBag.Message = "Upload successful";
            return View("Upload");
        }

        //public ActionResult Upload_Post()
        //{
        //    List<DTO.DocumentUploadDTO> customers = new List<DTO.DocumentUploadDTO>();
        //    HttpPostedFileBase postedFile = Request.Files["file_Uploader"];

        //    string filePath = string.Empty;

        //    if (postedFile != null)
        //    {
        //        string path = Server.MapPath("~/App_Data/UploadedFiles/");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        filePath = path + Path.GetFileName(postedFile.FileName);
        //        string extension = Path.GetExtension(postedFile.FileName);
        //        postedFile.SaveAs(filePath);

        //        //Read the contents of TXT file.
        //        // string csvData = System.IO.File.ReadAllLines(filePath, Encoding.UTF8).ElementAtOrDefault(1);
        //        string csvData = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

        //        string[] lines = csvData.Split('\n');
        //        foreach (string row in lines.Skip(1))
        //        {
        //            if (!string.IsNullOrEmpty(row))
        //            {
        //                customers.Add(new DTO.DocumentUploadDTO
        //                {
        //                    ID = row.Split('|')[0],
        //                    FileName = row.Split('|')[1],
        //                    CurrentStatus = row.Split('|')[2]
        //                });
        //            }
        //        }

        //        Models.DocumentUploadModels db = new Models.DocumentUploadModels();
        //        db.Upload(customers);
        //    }
        //    ViewBag.Message = "Upload successful";
        //    return View(customers);
        //}
    }
}