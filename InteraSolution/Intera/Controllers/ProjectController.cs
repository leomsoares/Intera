using System;
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
                ViewBag.AlunoProjeto = model.ReadAlunoProjeto(projeto.IdProjeto);
                ViewBag.Referencia = model.ReadReferencia(projeto.IdProjeto);
            }
            return View();
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

        public ActionResult delreferenciaprojeto(int id)
        {
            using (ProjetoModel model = new ProjetoModel())
            {
                model.DelReferencia(id);
            }
            return RedirectToAction("createstep2");
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
                i = modelv.UserProjeto(id, p.IdPessoa);
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
                if (ViewBag.Projeto.IdCoorientador != 0)
                {
                    ViewBag.NomeCoorientador = model2.UpdateReadProfessor(ViewBag.Projeto.IdCoorientador);
                }
            }

            return View();
        }

        public ActionResult alterarAlunoData(int id)
        {
            using (ProjetoModel model = new ProjetoModel())
            {
                model.AlterarAlunoProjeto(id, 3);
            }
            return RedirectToAction("edit");
        }
    }
}