using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intera.Entity;

namespace Intera.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult addproject()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
            }
            return View();
        }

        [Autoriza]
        public ActionResult createproject()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
            }
            return View();
        }

        public ActionResult seekproject()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
            }
            return View();
        }

        public ActionResult seeproject()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
            }
            return View();
        }
    }
}