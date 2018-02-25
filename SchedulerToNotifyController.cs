using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NLG.Controllers
{
    public class SchedulerToNotifyController : Controller
    {
        HttpClient client;
        string urlresult = "http://api.vitagenum.pl/v1/analysis/result/";
        //
        // GET: /SchedulerToNotify/
        dbNlgEntities db = new dbNlgEntities();
        public SchedulerToNotifyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(urlresult);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "bmxnOkVtQ3lKaHNnNGQ==");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ActionResult> Index()
        {

            var isbundlexists = db.tblClientNotifications.Where(k => k.IsNotified == false).ToList();

            foreach (var c in isbundlexists)
            {
                if (c.IsNotified != true)
                {
                    HttpResponseMessage responseMessageresult = await client.GetAsync(urlresult + c.BundleNo);

                    if (responseMessageresult.IsSuccessStatusCode)
                    {
                        var responseDataresult = responseMessageresult.Content.ReadAsStringAsync().Result;


                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        // BundleResult data = ser.Deserialize<BundleResult>(responseDataresult);

                        var data = ser.Deserialize<dynamic>(responseDataresult);

                        var x = data["data"]["result"];
                        string k = "";
                        object obj = null;
                        object[] name = obj as object[];

                        foreach (var items in x)
                        {
                            k = items.Key;
                            if (items.Value != null)
                            {
                                var clientdeatil = db.tblBundles.Where(l => l.BundleNo == c.BundleNo).FirstOrDefault();
                                StreamReader sr = System.IO.File.OpenText(Server.MapPath("~/EmailTemplate/ResultsAvailable.html"));
                                string strContents = sr.ReadToEnd();
                                strContents = strContents.Replace("[Alias]", clientdeatil.Alias);
                                SendEmail.Sendmail(clientdeatil.Email, "alert@newlifegenetics.com", "Message from New Life Genetics!", strContents);
                                sr.Dispose();
                                Thread.Sleep(1000);

                                var clients = db.tblClientNotifications.Where(m => m.BundleNo == c.BundleNo).FirstOrDefault();
                                c.IsNotified = true;
                                db.SaveChanges();
                                clientdeatil.DispatchedDate = System.DateTime.Now;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            return View();
        }

        //
        // GET: /SchedulerToNotify/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SchedulerToNotify/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SchedulerToNotify/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SchedulerToNotify/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SchedulerToNotify/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SchedulerToNotify/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SchedulerToNotify/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
