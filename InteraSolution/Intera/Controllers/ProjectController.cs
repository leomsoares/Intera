using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intera.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult addproject()
        {
            return View();
        }

        public ActionResult createproject()
        {
            return View();
        }

        public ActionResult seekproject()
        {
            return View();
        }
    }
}