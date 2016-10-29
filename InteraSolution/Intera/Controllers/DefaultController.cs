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
            int verificar = 0;
            verificar = Convert.ToInt32(form["Type"]);

            Aluno a = new Aluno();
            Professor p = new Professor();
           
            a.Nome = form["Nome"];
            a.Email = form["Email"];
            a.Senha = form["Senha"];
            a.Ra = form["RaRs"];
            a.Curso = form["Curso"];

            p.Nome = form["Nome"];
            p.Email = form["Email"];
            p.Senha = form["Senha"];
            p.Rs = form["RaRs"];

            using (PessoaModel model = new PessoaModel())
            {
                if(verificar == 1)
                {
                    model.CreateAluno(a);
                }
                else if(verificar == 2)
                {
                    model.CreateProfessor(p);
                }
            }

            return RedirectToAction("Register");
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