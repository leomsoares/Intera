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
            projeto.IdProfessor = ViewBag.id;
            if (Convert.ToInt32(form["IdCoorientador"]) != 0)
            {
                projeto.IdCoorientador = Convert.ToInt32(form["IdCoorientador"]);
            }
            projeto.IdTipo = Convert.ToInt32(form["Tipo"]);
            projeto.NomeProjeto = form["NomeProjeto"];
            projeto.DataInicio = DateTime.Today;
            projeto.Descricao = form["Descricao"];

            if (ViewBag.Id == projeto.IdCoorientador)
            {
                ViewBag.MensagemCoorientador = "Coorientador inválido";
                List<Professor> lista = new List<Professor>();
                using (ProjetoModel model = new ProjetoModel())
                {
                    lista = model.ReadProfessor();
                }
                return View(lista);
            }

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
            ViewBag.MensagemErroAluno = Session["MensagemErroAluno"];
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];
            List<AlunoProjeto> lista = new List<AlunoProjeto>();

            using (ProjetoModel model = new ProjetoModel())
            {
                ViewBag.AlunoProjeto = model.ReadAlunoProjeto(projeto.IdProjeto);
                ViewBag.Referencia = model.ReadReferencia(projeto.IdProjeto);
            }
            return View();
        }

        [HttpPost]
        public ActionResult salvararquivo()
        {
            HttpPostedFileBase arquivo = Request.Files[0];
            string caminhoArquivo = null;

            if (arquivo.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/Arquivos");
                caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                arquivo.SaveAs(caminhoArquivo);

                var linkPath = "~\\Arquivos\\";

                string link = Path.Combine(linkPath, Path.GetFileName(arquivo.FileName));

                Projeto projeto = new Projeto();
                projeto = (Projeto)Session["idProjeto"];
                using (ProjetoModel model = new ProjetoModel())
                {
                    model.UpProjectLink(link, projeto.IdProjeto);
                }

            }
            return RedirectToAction("group");
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
            Session["MensagemErroAluno"] = null;
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
                    return RedirectToAction("Createstep2");
                }
            }
            Session["MensagemErroAluno"] = "Student is already in the project";
            return RedirectToAction("Createstep2");
        }

        public ActionResult addalunodataedit(int id)
        {
            Projeto projeto = new Projeto();
            int idproj = (int)Session["idProj"];
            int cont = 0;
            using (ProjetoModel modelcont = new ProjetoModel())
            {
                cont = modelcont.VerificarAluno(id, idproj);
            }

            if (cont == 0)
            {
                using (ProjetoModel model = new ProjetoModel())
                {
                    model.AddAluno(id, idproj, DateTime.Today);

                }
            }
            return RedirectToAction("edit/" + idproj);
        }

        public ActionResult delalunodata(int id)
        {
            Session["MensagemErroAluno"] = null;
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];

            using (ProjetoModel model = new ProjetoModel())
            {
                model.DelAluno(id, projeto.IdProjeto);
            }

            return RedirectToAction("Createstep2");
        }

        public ActionResult delalunodataedit(int id)
        {
            int idproj = (int)Session["idProj"];

            using (ProjetoModel model = new ProjetoModel())
            {
                model.AlterarAlunoProjeto(id, idproj);
            }

            return RedirectToAction("edit/" + idproj);
        }

        //public ActionResult alterarAlunoData(int id)
        //{
        //    int idproj = (int)Session["idProj"];
        //    using (ProjetoModel model = new ProjetoModel())
        //    {
        //        model.AlterarAlunoProjeto(id, 3);
        //    }
        //    return RedirectToAction("edit");
        //}

        [HttpPost]
        public ActionResult addreferenciaprojeto(FormCollection form)
        {
            Referencia referencia = new Referencia();
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];

            referencia.DescricaoReferencia = form["referenceName"];
            referencia.IdProjeto = projeto.IdProjeto;

            using (ProjetoModel model = new ProjetoModel())
            {
                model.AddReferencia(referencia);
            }
            return RedirectToAction("createstep2");
        }

        [HttpPost]
        public ActionResult addreferenciaprojetoedit(FormCollection form)
        {
            Referencia referencia = new Referencia();
            int idproj = (int)Session["idProj"];

            referencia.DescricaoReferencia = form["referenceName"];
            referencia.IdProjeto = idproj;

            using (ProjetoModel model = new ProjetoModel())
            {
                model.AddReferencia(referencia);
            }
            return RedirectToAction("edit/" + idproj);
        }

        public ActionResult delreferenciaprojeto(int id)
        {
            using (ProjetoModel model = new ProjetoModel())
            {
                model.DelReferencia(id);
            }
            return RedirectToAction("createstep2");
        }

        public ActionResult delreferenciaprojetoedit(int id)
        {
            int idproj = (int)Session["idProj"];
            using (ProjetoModel model = new ProjetoModel())
            {
                model.DelReferencia(id);
            }
            return RedirectToAction("edit/" + idproj);
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
                ViewBag.Refencia = model.ReadReferencia(id);
            }
            return View(lista);

        }
        [HttpPost]
        public FileResult seeproject()
        {
            string nome = (string)Session["caminho"];
            string contentType = "application/pdf";
            //return File(nome, contentType, nome.Substring(9, nome.Length));
            return File(nome, contentType, "Projeto.pdf");
            //return File(caminho, contentType, "reporte.pdf");
        }
        public ActionResult liststudent(string nome)
        {
            List<Pessoa> lista = new List<Pessoa>();
            if (nome != "")
            {
                using (ProjetoModel model = new ProjetoModel())
                {
                    ViewBag.AlunoProjetoModal = model.SearchAluno(nome);
                }
            }
            return PartialView();
        }

        public ActionResult liststudentedit(string nome)
        {
            List<Pessoa> lista = new List<Pessoa>();
            if (nome != "")
            {
                using (ProjetoModel model = new ProjetoModel())
                {
                    ViewBag.AlunoProjetoModal = model.SearchAluno(nome);
                }
            }
            return PartialView();
        }

        public ActionResult group()
        {
            Pessoa p = new Pessoa();
            if (Session["user"] != null)
            {
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;

                List<Projeto> lista = new List<Projeto>();

                if (p.Status == 1)
                {
                    using (ProjetoModel model = new ProjetoModel())
                    {
                        lista = model.ReadProjetoAluno(p.IdPessoa);
                        ViewBag.Professores = model.ReadProfessor();
                    }
                    return View(lista);
                }
                if (p.Status == 2)
                {
                    using (ProjetoModel model = new ProjetoModel())
                    {
                        lista = model.ReadProjetoProfessor(p.IdPessoa);
                        ViewBag.Professores = model.ReadProfessor();
                    }
                    return View(lista);
                }
            }
            if (p.Status == 3)
            {
                return new RedirectResult("/default/index");
            }
            return View();
        }

        [Autoriza]
        public ActionResult posts(int id)
        {
            Pessoa p = new Pessoa();
            if (Session["user"] != null)
            {
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
                ViewBag.IdPessoa = p.IdPessoa;
            }
            ViewBag.IdProjeto = id;
            List<Mensagem> lista = new List<Mensagem>();
            bool i = false;

            using (ProjetoModel modelv = new ProjetoModel())
            {
                if (p.Status == 1)
                {
                    i = modelv.UserProjeto(id, p.IdPessoa);
                }
                else
                {
                    i = modelv.ProfProjeto(id, p.IdPessoa);
                }
            }

            if (i == true)
            {

                using (ProjetoModel model = new ProjetoModel())
                {
                    lista = model.ReadMensagem(id);
                    return View(lista);
                }
            }
            if (p.Status == 3)
            {
                return new RedirectResult("/default/index");
            }
            return RedirectToAction("group");
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

        public ActionResult edit(int id)
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Status = p.Status;
            }
            Projeto projeto = new Projeto();

            using (ProjetoModel model = new ProjetoModel())
            {
                ViewBag.Projeto = model.ReadEditProjeto(id);
                ViewBag.ListaAlunoProjeto = model.ReadAlunoProjeto(id);
                ViewBag.ListaReferencia = model.ReadReferencia(id);
            }

            using (PessoaModel model2 = new PessoaModel())
            {
                ViewBag.NomeOrientador = model2.UpdateReadProfessor(ViewBag.Projeto.IdProfessor);
                ViewBag.NomeCoorientador = "";
                if (ViewBag.Projeto.IdCoorientador != 0)
                {
                    Pessoa p = model2.UpdateReadProfessor(ViewBag.Projeto.IdCoorientador);
                    if (!String.IsNullOrEmpty(p.Nome))
                    {
                        ViewBag.NomeCoorientador = p.Nome;
                    }
                }
            }
            Session["idProj"] = id;

            return View();
        }

        public ActionResult fimprojeto()
        {
            HttpPostedFileBase arquivo = Request.Files[0];
            string caminhoArquivo = null;
            Projeto projeto = new Projeto();
            projeto = (Projeto)Session["idProjeto"];

            if (arquivo.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/Arquivos");
                caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                arquivo.SaveAs(caminhoArquivo);

                var linkPath = "~\\Arquivos\\";

                string link = Path.Combine(linkPath, Path.GetFileName(arquivo.FileName));


                using (ProjetoModel model = new ProjetoModel())
                {
                    model.UpProjectLink(link, projeto.IdProjeto);
                }

            }

            using (ProjetoModel modelf = new ProjetoModel())
            {
                modelf.EndProject(projeto.IdProjeto);
            }
            return RedirectToAction("seeproject" + projeto.IdProjeto);
        }

        public ActionResult endproject(int id)
        {
            using (ProjetoModel model = new ProjetoModel())
            {
                model.EndProject(id);
            }

            return RedirectToAction("group");
        }

        [HttpPost]
        public ActionResult editarprojeto(FormCollection form)
        {
            HttpPostedFileBase arquivo = Request.Files[0];
            string caminhoArquivo = null;
            string descricao = form["Desc"];
            int id = (int)Session["idProj"];

            if (arquivo.ContentLength > 0)
            {
                var uploadPath = Server.MapPath("~/Arquivos");
                caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                arquivo.SaveAs(caminhoArquivo);

                var linkPath = "~\\Arquivos\\";

                string link = Path.Combine(linkPath, Path.GetFileName(arquivo.FileName));


                using (ProjetoModel model = new ProjetoModel())
                {
                    model.UpProjectLink(link, id);
                }

            }
            using (ProjetoModel modeld = new ProjetoModel())
            {
                modeld.UpProjectDesc(descricao, id);
            }

            return RedirectToAction("group");
        }
    }
}