using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using HMT.Areas.Admin.Models;
using System.Data.OleDb;
using System.Data.SqlClient;
using Excel;
using System.Data;
using System.Threading;
namespace HMT.Areas.Sales.Controllers
{
    public class SalesRepController : Controller
    {
        dbHMTEntities db = new dbHMTEntities();
        //
        // GET: /Sales/SaleAdmin/Index

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Sales/SaleAdmin/Details/5
        public ActionResult ManageSalesReps(string sortOrder, int? page, string txtValue)
        {
            if (TempData["message"] != null)
            {
                ViewBag.Success = TempData["message"];
            }

            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            //IPagedList<HMT.tblSalesRep> Sales = null;
            var model = new HMT.Areas.Sales.Models.Representative();

            var Sales = model.SalesRepresentatives().OrderByDescending(m => m.SalesRepId).ToPagedList(pageIndex, pageSize);
            ViewData["model"] = Sales;
            //Sales = db.tblSalesReps.OrderBy(m => m.SalesRepId).ToPagedList(pageIndex, pageSize);
            if (Sales.Count == 0)
            {
                ViewBag.Message = "No Record Found";
            }
            if (Request.IsAjaxRequest())
            {
                if (!String.IsNullOrEmpty(txtValue))
                {
                    Sales = model.SalesRepresentatives().OrderBy(m => m.SalesRepId).Where(s => s.FirstName.ToLower().Contains(txtValue.ToLower())).ToPagedList(pageIndex, pageSize);
                    if (Sales.Count == 0)
                    {
                        ViewBag.Message = "Result not found";

                    }
                }
                return PartialView("_ManageSalesReps", Sales);
            }
            return View(Sales);
        }
        //
        // GET: /Sales/SaleAdmin/IsSalesActive/5
        public ActionResult IsSalesActive(int id, int? page)
        {
            int pageSize = 4;
            int pageIndex = 1;
            if (Request.IsAjaxRequest())
            {
                var Sale = db.tblSalesReps.Where(i => i.SalesRepId == id).FirstOrDefault();
                if (Sale.IsActive == true)
                    Sale.IsActive = false;
                else if (Sale.IsActive == false)
                    Sale.IsActive = true;
                db.SaveChanges();
            }
            var Adminlist = new HMT.tblSalesRep();
            var Sales = db.tblSalesReps.OrderBy(m => m.SalesRepId).ToPagedList(pageIndex, pageSize);
            return RedirectToAction("ManageSalesReps");
        }
        // GET: /Sales/SaleAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var items = db.tblSalesReps.Where(k => k.SalesRepId == id).FirstOrDefault();

            db.tblSalesReps.Remove(items);
            db.SaveChanges();
            return RedirectToAction("ManageSalesReps");
        }
        //
        // Post: /Sales/SaleAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(ICollection<int> SelectedSources)
        {
            int pageSize = 4;
            int pageIndex = 1;
            if (SelectedSources != null)
            {
                var delete = db.tblSalesReps.ToList();
                delete = db.tblSalesReps.Where(k => SelectedSources.Contains(k.SalesRepId)).ToList();
                foreach (var i in delete)
                {
                    TempData["Alert"] = "Record has deleted successfully!";
                    db.tblSalesReps.Remove(i);
                }
                db.SaveChanges();

            }
            return PartialView("_ManageSalesReps", db.tblSalesReps.OrderBy(m => m.SalesRepId).ToPagedList(pageIndex, pageSize));
        }
        // GET: /Sales/SaleAdmin/BulkEmails/5
        public ActionResult BulkEmails()
        {

            ViewBag.Sales = db.tblSalesReps;
            ViewBag.EmailTemp = db.tblTemplates;
            return View();
        }
        // Post: /Sales/SaleAdmin/BulkEmails/5
        [HttpPost]
        public ActionResult BulkEmails(HttpPostedFileBase File, SalesRepresentative sale)
        {
            var filename = "";
            var model = new HMT.Areas.Admin.Models.SalesRepresentative();
            int id = sale.SalesRepId;
            int tempid = Convert.ToInt32(sale.ETId);

            var Sales = model.SalesRepresentatives().OrderByDescending(m => m.SalesRepId).Where(k => k.SalesRepId == id).FirstOrDefault();
            var Emails = model.EmailTemp().OrderByDescending(m => m.ETId).Where(k => k.ETId == tempid).FirstOrDefault();

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it.  
                        filename = Path.GetFileName(File.FileName);
                        string path = (Server.MapPath("/ExcelSheet/" + Path.GetFileName(File.FileName)));
                        File.SaveAs(path);
                        Stream stream = file.InputStream;
                        IExcelDataReader reader = null;


                        if (file.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (file.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            return View();
                        }

                        reader.IsFirstRowAsColumnNames = true;

                        DataSet result = reader.AsDataSet();
                        reader.Close();

                        var EmailBulk = new tblBulkEmailLog();
                        EmailBulk.Contact_Id = Convert.ToInt16(Session["SalesId"]);
                        EmailBulk.ContactType = "SalesRep";
                        EmailBulk.ExcelSheetFileName = filename;
                        EmailBulk.DateLogged = System.DateTime.Now;
                        db.tblBulkEmailLogs.Add(EmailBulk);
                        db.SaveChanges();

                        int lastLogId = db.tblBulkEmailLogs.Max(item => item.LogId);
                        int count = 0;
                        for (int j = 0; j < result.Tables[0].Rows.Count; j++)
                        {
                            var body = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title>Email template</title></head>" + HttpUtility.HtmlDecode(Emails.HtmlPageName) + "</html>";
                            string email = result.Tables[0].Rows[j]["Email"].ToString();
                            string ContactName = result.Tables[0].Rows[j]["ContactName"].ToString();
                            string Phonenumber = result.Tables[0].Rows[j]["PhoneNumber"].ToString();
                            string Domainname = result.Tables[0].Rows[j]["DomainName"].ToString();
                            string strContents = body.Replace("&lt;", "<").Replace("&gt;", ">").Replace("<br />", "");

                            strContents = strContents.Replace("{FirstName}", ContactName);
                            strContents = strContents.Replace("{SalesRep}", Session["Name"].ToString());
                            strContents = strContents.Replace("{Domain}", Domainname);
                            SendEmail.Sendmail(email, "softwareshash@gmail.com", "Lets make " + Domainname + "  live together !", strContents);

                            var sales = new tblContact();
                            sales.ContactName = ContactName;
                            sales.Email = email;
                            sales.Phonenumber = Phonenumber;
                            sales.Contact_Id = Convert.ToInt16(Session["SalesId"]);
                            sales.Log_Id = lastLogId;
                            sales.DomainName = Domainname;
                            db.tblContacts.Add(sales);
                            db.SaveChanges();
                            count++;
                            if (count == 10)
                            {
                                Thread.Sleep(1000);
                                count = 0;
                            }
                        }
                      
                    }
                    // Returns message that successfully uploaded  
                }
                catch (Exception ex)
                {
                    //return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return View();

        }
    }
}
