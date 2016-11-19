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

        [Autoriza]
        public ActionResult addstudent()
        {
            List<Pessoa> lista = new List<Pessoa>();
            using (ProjetoModel model = new ProjetoModel())
            {
                lista = model.ReadAluno();
            }

            return View(lista);
        }

        [Autoriza]
        public ActionResult addalunodata(int id)
        {
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];

            using (ProjetoModel model = new ProjetoModel())
            {
                model.AddAluno(id, projeto.IdProjeto, DateTime.Today);

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
            using (PessoaModel teste = new PessoaModel())
            {
                ViewBag.Professor = teste.ReadProfessor();
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
            List<Pessoa> alunos = new List<Pessoa>();
            using (PessoaModel model = new PessoaModel())
            {
                alunos = model.Read();
            }

            return PartialView(alunos);
        }
    }
}