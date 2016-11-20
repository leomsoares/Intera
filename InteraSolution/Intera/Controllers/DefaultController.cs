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

            ViewBag.Mensagem = "Usuário e/ou senha inválidos!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index");
        }

        public ActionResult forget()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult forget(FormCollection form)
        {
            string email = form["Email"];
            Pessoa p = new Pessoa();
            using (PessoaModel modelp = new PessoaModel())
            {
                p = modelp.ResgatarSenha(email);
            }

            using (HomeModel modelh = new HomeModel())
            {
                modelh.EnviarEmail(email, p);
            }
            return RedirectToAction("Index");
        }

        public ActionResult scientificresearch()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }
            List<Projeto> lista = new List<Projeto>();
            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadScientificResearch();
            }
            
            return View(lista);
        }


        public ActionResult group()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;

                List<Projeto> lista = new List<Projeto>();

                if (p.Status == 1)
                {
                    using (ProjetoModel model = new ProjetoModel())
                    {
                        lista = model.ReadProjetoAluno(p.IdPessoa);
                    }
                    return View(lista);
                }
                if (p.Status == 2)
                {
                    using (ProjetoModel model = new ProjetoModel())
                    {
                        lista = model.ReadProjetoProfessor(p.IdPessoa);
                    }
                    return View(lista);
                }
            }
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
    }
}