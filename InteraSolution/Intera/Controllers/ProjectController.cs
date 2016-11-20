﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intera.Entity;
using Intera.Models;
using System.IO;

namespace Intera.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        [Autoriza]
        public ActionResult createstep1()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
                ViewBag.id = p.IdPessoa;
            }

            List<Professor> lista = new List<Professor>();
            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadProfessor();
            }
            return View(lista);
        }

        [Autoriza]
        [HttpPost]
        public ActionResult createstep1(FormCollection form)
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
                ViewBag.id = p.IdPessoa;
            }
            Projeto projeto = new Projeto();

            int arquivosSalvos = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];
                string caminhoArquivo = null;

                if (arquivo.ContentLength > 0)
                {
                    var uploadPath = Server.MapPath("~/Imagens");
                    caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }
                projeto.Link = caminhoArquivo;
            }

            projeto.IdProfessor = ViewBag.id;
            if (Convert.ToInt32(form["IdCoorientador"]) != 0)
            {
                projeto.IdCoorientador = Convert.ToInt32(form["IdCoorientador"]);
            }
            projeto.IdTipo = Convert.ToInt32(form["Tipo"]);
            projeto.NomeProjeto = form["NomeProjeto"];
            projeto.DataInicio = DateTime.Today;
            projeto.Descricao = form["Descricao"];

            using (ProjetoModel model = new ProjetoModel())
            {
                projeto.IdProjeto = model.CreateProject(projeto);
            }

            Session["idProjeto"] = projeto;
            return RedirectToAction("createstep2");
        }

        [Autoriza]
        public ActionResult createstep2()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];
            List<AlunoProjeto> lista = new List<AlunoProjeto>();

            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadAlunoProjeto(projeto.IdProjeto);
            }
            return View(lista);
        }

        //[Autoriza]
        //public ActionResult addstudent()
        //{
        //    List<Pessoa> lista = new List<Pessoa>();
        //    using (ProjetoModel model = new ProjetoModel())
        //    {
        //        //lista = model.ReadAluno();
        //    }

        //    return View(lista);
        //}

        [Autoriza]
        public ActionResult addalunodata(int id)
        {
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];
            int cont = 0;
            using (ProjetoModel modelcont = new ProjetoModel())
            {
                cont = modelcont.VerificarAluno(id, projeto.IdProjeto);
            }

            if (cont == 0)
            {
                using (ProjetoModel model = new ProjetoModel())
                {
                    model.AddAluno(id, projeto.IdProjeto, DateTime.Today);

                }
            }
            return RedirectToAction("Createstep2");
        }

        public ActionResult delalunodata(int id)
        {
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];

            using (ProjetoModel model = new ProjetoModel())
            {
                model.DelAluno(id, projeto.IdProjeto);
            }

            return RedirectToAction("Createstep2");
        }

        public ActionResult seekproject(FormCollection form)
        {
            if (Session["user"] != null)
            {
                Pessoa u = new Pessoa();
                u = (Pessoa)Session["user"];
                ViewBag.user = u.Nome;
                ViewBag.Status = u.Status;
            }
            string Professor = null;
            string NameProject = null;
            string Status = null;
            Professor = form["Professor"];
            NameProject = form["NameProject"];
            Status = form["Status"];
            ViewBag.NameProject = form["NameProject"];

            List<Projeto> lista = new List<Projeto>();
            if (Professor == null)
            {
                Professor = "";
            }
            if (NameProject == null)
            {
                NameProject = "";
            }
            if (Status == null)
            {
                Status = "";
            }

            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadProjeto(Professor, NameProject, Status);
            }
            using (PessoaModel nome = new PessoaModel())
            {
                ViewBag.Professor = nome.ReadProfessor();
            }
            return View(lista);
        }

        public ActionResult seeproject(int id)
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
                lista = model.ReadProjeto(id);
                ViewBag.Membros = model.ReadAlunoProjeto(id);
                ViewBag.ProfessorNome = model.ReadProfessorProjeto(id);
            }
            return View(lista);

        }

        public ActionResult liststudent(string nome)
        {
            List<Pessoa> lista = new List<Pessoa>();
            if (nome != "")
            {
                using (ProjetoModel model = new ProjetoModel())
                {
                    lista = model.SearchAluno(nome);
                }
            }
            return PartialView(lista);
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

        [Autoriza]
        public ActionResult posts(int id)
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
                ViewBag.IdPessoa = p.IdPessoa;
            }
            ViewBag.IdProjeto = id;
            List<Mensagem> lista = new List<Mensagem>();
            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadMensagem(id);
            }
            return View(lista);
        }

        [HttpPost]
        public ActionResult posts(FormCollection form, int id)
        {
            Pessoa p = new Pessoa();
            if (Session["user"] != null)
            {
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }

            Mensagem msg = new Mensagem();
            msg.DescricaoMsg = form["Mensagem"];
            msg.IdPessoa = p.IdPessoa;

            using (ProjetoModel model = new ProjetoModel())
            {
                model.CreateMensagem(msg, id);
            }
            return RedirectToAction("posts");
        }
    }
}