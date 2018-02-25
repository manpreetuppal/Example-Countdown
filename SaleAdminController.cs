using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace HMT.Areas.Sales.Controllers
{
    public class SaleAdminController : Controller
    {
        dbHMTEntities db = new dbHMTEntities();
        //
        // GET: /Sales/SaleAdmin/
        public ActionResult Index()
        {
            return View();
        }
        // GET: /Sales/SaleAdmin/Default
        public ActionResult Default()
        {
            return View();
        }
        // Post: /Sales/SaleAdmin/Default
        [HttpPost]
        public ActionResult Default(HMT.tblSalesRep sales)
        {
            if (ModelState.IsValid)
            {
                using (dbHMTEntities db = new dbHMTEntities())
                {

                    var user = db.tblSalesReps.Where(k => k.Username == sales.Username).FirstOrDefault();
                    if (user != null)
                    {
                        var Pass = db.tblSalesReps.Where(k => k.Password == sales.Password).FirstOrDefault();
                        if (Pass != null)
                        {
                            Session["SalesId"] = user.SalesRepId.ToString();
                            Session["Name"] = user.FirstName.ToString();
                            return RedirectToAction("Index", new { area = "Sales" });
                        }
                        else
                            ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                    else
                        TempData["ErrorMessage"] = "The user name or password provided is incorrect.";
                    // ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View();
        }
        //
        // GET: /Sales/SaleAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
