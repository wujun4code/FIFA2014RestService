using RestService.Core;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestServiceWeb.Controllers
{
    public class AdminController : Controller
    {

        IRSCRestClient client = new RSCClient();
        string BASE_URL = "http://localhost:30213/api";
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
    }
}
