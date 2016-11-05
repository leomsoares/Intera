using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intera.Entity;
using Intera.Models;

namespace Intera.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string email = form["email"];
            string senha = form["senha"];

            using (PessoaModel model = new PessoaModel())
            {
                Pessoa p = model.Login(email, senha);
                if(p != null)
                {
                    Session["user"] = p;
                    return RedirectToAction("Index", "Default");
                }
            }

            ViewBag.Mensagem = "Usuário não cadastrado";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index");
        }

        



        public ActionResult professors()
        {
            return View();
        }

        public ActionResult scientificresearch()
        {
            return View();
        }

        public ActionResult profile()
        {
            return View();
        }

        public ActionResult group()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult about()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }
            return View();
        }
        public ActionResult editmembers()
        {
            return View();
        }
    }
}