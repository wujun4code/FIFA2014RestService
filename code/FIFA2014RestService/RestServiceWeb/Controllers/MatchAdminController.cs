using RestService.Core;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestServiceWeb.Controllers
{
    public class MatchAdminController : Controller
    {

        IRSCRestClient client = new RSCClient();
        string BASE_URL = "http://localhost:30213/api";


        // GET: /MatchAdmin/
        public ActionResult Index()
        {
            var allMatches = client.GetFromUrl<Match>(BASE_URL, "Match");
            return View(allMatches);
        }

        //
        // GET: /MatchAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MatchAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MatchAdmin/Create
        [HttpPost]
        public ActionResult Create(Match collection)
        {
            try
            {
                
                client.Create<Match>(BASE_URL, "Match", collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MatchAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MatchAdmin/Edit/5
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
        // GET: /MatchAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MatchAdmin/Delete/5
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
