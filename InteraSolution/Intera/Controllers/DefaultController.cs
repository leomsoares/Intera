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
            List<Projeto> lista = new List<Projeto>();
            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadProjetoLastAdd();
            }
            return View(lista);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string email = form["email"];
            string senha = form["senha"];

            using (PessoaModel model = new PessoaModel())
            {
                Pessoa p = model.Login(email, senha);
                if (p != null)
                {
                    Session["user"] = p;
                    return RedirectToAction("Index", "Default");
                }
            }

            ViewBag.Mensagem = "Usuário e/ou senha inválidos!";
            List<Projeto> lista = new List<Projeto>();
            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadProjetoLastAdd();
            }
            return View(lista);
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
                bool retorno = modelp.VerificarEmail(email);
                if (retorno)
                {
                    p = modelp.ResgatarSenha(email);
                }
                else
                {
                    ViewBag.MsgErroForgot = "Email invalid";
                    return View();
                }
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
        [HttpPost]
        public ActionResult scientificresearch(int id)
        {
            if (Session["user"] != null)
            {
                ViewBag.MsgErroForgot = 1;
                @ViewBag.MsgErroForgot = "Email enviado com sucesso";
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;

                Pessoa Professor = new Pessoa();
                Pessoa Coorientador = new Pessoa();
                Projeto Projeto = new Projeto();

                using (ProjetoModel modelP = new ProjetoModel())
                {
                    Projeto = modelP.ReadProjeto2(id);
                }
                using (PessoaModel model1 = new PessoaModel())
                {
                    Professor = model1.ReadProfessor(Projeto.IdProfessor);
                    if (Projeto.IdCoorientador != 0)
                    {
                        Coorientador = model1.ReadProfessor(Projeto.IdCoorientador);
                    }
                }
                using (HomeModel email = new HomeModel())
                {
                    email.EnviarEmail(Professor, p, Projeto.NomeProjeto);
                    if (Projeto.IdCoorientador != 0)
                    {
                        email.EnviarEmail(Coorientador, p, Projeto.NomeProjeto);
                    }
                }
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

        public ActionResult reports()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }
            using (PessoaModel pessoa = new PessoaModel())
            {
                ViewBag.Professor = pessoa.ReadProfessor();
                ViewBag.Aluno = pessoa.ReadAluno();
            }
            using (ProjetoModel projeto = new ProjetoModel())
            {
                ViewBag.Projetos = projeto.ReadProjeto();
                ViewBag.AlunosProjeto = projeto.ReadAlunoData();
                ViewBag.CountAluno = projeto.Count(1);
                ViewBag.CountProfessor = projeto.Count(2);
                ViewBag.CountProjeto = projeto.Count(4);
            }
            return View();
        }
    }
}