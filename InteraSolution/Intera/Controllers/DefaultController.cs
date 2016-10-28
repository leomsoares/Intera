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

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult register(FormCollection form)
        {
            Pessoa p = new Pessoa();
            Aluno a = new Aluno();

            p.Nome = form["Nome"];
            p.Email = form["Email"];
            p.Senha = form["Senha"];
            a.Ra = form["Ra"];

            using (PessoaModel model = new PessoaModel())
            {
                model.CreateAluno(p, a);
            }

            return RedirectToAction("Register", "Default");
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
            return View();
        }
        public ActionResult editmembers()
        {
            return View();
        }
    }
}