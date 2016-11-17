using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intera.Entity;
using Intera.Models;

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
            return View();
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
            projeto.IdProfessor = ViewBag.id;
            projeto.IdCoorientador = Convert.ToInt32(form["IdCoorientador"]);
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
            }
            return View(lista);

        }
    }
}