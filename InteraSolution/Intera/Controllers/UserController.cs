using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intera.Models;
using Intera.Entity;
using System.IO;

namespace Intera.Controllers
{
    [Autoriza]
    public class UserController : Controller
    {
        // GET: User
        //public ActionResult manage()
        //{
        //    if (Session["user"] != null)
        //    {
        //        Pessoa p = new Pessoa();
        //        p = (Pessoa)Session["user"];
        //        ViewBag.user = p.Nome;
        //        ViewBag.Status = p.Status;
        //    }

        //    PessoaModel model = new PessoaModel();
        //    List<Pessoa> lista = model.Read();


        //    return View(lista);
        //}
        public ActionResult manage(FormCollection form)
        {
            if (Session["user"] != null)
            {
                Pessoa u = new Pessoa();
                u = (Pessoa)Session["user"];
                ViewBag.user = u.Nome;
                ViewBag.Status = u.Status;
            }
            string busca = null;
            busca = form["Search"];
            List<Pessoa> lista = new List<Pessoa>();
            if (busca == null)
            {
                busca = "";
            }
            using(PessoaModel model = new PessoaModel())
            {
                lista = model.Search(busca);
            }   
            return View(lista);
        }

        public ActionResult Delete(int id)
        {
            using (PessoaModel model = new PessoaModel())
            {
                model.Delete(id);
            }
            return RedirectToAction("Manage");
        }

        public ActionResult create()
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
        public ActionResult create(FormCollection form)
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
                if (verificar == 1)
                {
                    model.CreateAluno(a);
                }
                else if (verificar == 2)
                {
                    model.CreateProfessor(p);
                }
            }
            return RedirectToAction("Manage");
        }

        public ActionResult update(int id)
        {
            if (Session["user"] != null)
            {
                Pessoa u = new Pessoa();
                u = (Pessoa)Session["user"];
                ViewBag.user = u.Nome;
                ViewBag.Status = u.Status;
            }
            Pessoa p = new Pessoa();
            using (PessoaModel model = new PessoaModel())
            {
                p.Status = model.UpdateReadPessoa(id);
            }

            using (PessoaModel model2 = new PessoaModel())
            {
                if(p.Status == 1)
                {
                    Aluno a = model2.UpdateReadAluno(id);
                    ViewBag.IdPessoa = a.IdPessoa;
                    ViewBag.Nome = a.Nome;
                    ViewBag.Status = a.Status;
                    ViewBag.Email = a.Email;
                    ViewBag.Senha = a.Senha;
                    ViewBag.RaRs = a.Ra;
                    ViewBag.Curso = a.Curso;
                    return View(a);
                }
                Professor pr = model2.UpdateReadProfessor(id);
                ViewBag.IdPessoa = pr.IdPessoa;
                ViewBag.Nome = pr.Nome;
                ViewBag.Status = pr.Status;
                ViewBag.Email = pr.Email;
                ViewBag.Senha = pr.Senha;
                ViewBag.RaRs = pr.Rs;
                return View(pr);
            }
        }

        [HttpPost]
        public ActionResult update(int id, FormCollection form)
        {
            int verificar = 0;
            verificar = Convert.ToInt32(form["Type"]);
            Aluno a = new Aluno();
            Professor p = new Professor();

            if (verificar == 1)
            {
                a.IdPessoa = id;
                a.Nome = form["Nome"];
                a.Email = form["Email"];
                a.Senha = form["Senha"];
                a.Ra = form["RaRs"];
                a.Curso = form["Curso"];
            }
            else if (verificar ==2)
            {
                p.IdPessoa = id;
                p.Nome = form["Nome"];
                p.Email = form["Email"];
                p.Senha = form["Senha"];
                p.Rs = form["RaRs"];
            }

            using (PessoaModel model = new PessoaModel())
            {
                if (verificar == 1)
                {
                    model.UpdateAluno(a);
                }
                else if (verificar == 2)
                {
                    model.UpdateProfessor(p);
                }
            }
            return RedirectToAction("Manage");
        }

        public ActionResult view(int id)
        {
            if (Session["user"] != null)
            {
                Pessoa u = new Pessoa();
                u = (Pessoa)Session["user"];
                ViewBag.user = u.Nome;
                ViewBag.Status = u.Status;
            }
            Pessoa p = new Pessoa();
            using (PessoaModel model = new PessoaModel())
            {
                p.Status = model.UpdateReadPessoa(id);
            }

            using (PessoaModel model2 = new PessoaModel())
            {
                if (p.Status == 1)
                {
                    Aluno a = model2.UpdateReadAluno(id);
                    ViewBag.IdPessoa = a.IdPessoa;
                    ViewBag.Nome = a.Nome;
                    ViewBag.Status = a.Status;
                    ViewBag.Email = a.Email;
                    ViewBag.Senha = a.Senha;
                    ViewBag.RaRs = a.Ra;
                    ViewBag.Curso = a.Curso;
                    return View(a);
                }
                Professor pr = model2.UpdateReadProfessor(id);
                ViewBag.IdPessoa = pr.IdPessoa;
                ViewBag.Nome = pr.Nome;
                ViewBag.Status = pr.Status;
                ViewBag.Email = pr.Email;
                ViewBag.Senha = pr.Senha;
                ViewBag.RaRs = pr.Rs;
                return View(pr);
            }
        }

        public ActionResult profile()
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

        public ActionResult editprofile()
        {
            
            return View();
        }

        public ActionResult Upload()
        {

            return View();
        }

        public ActionResult FileUpload()
        {
            int arquivosSalvos = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];

                if (arquivo.ContentLength > 0)
                {
                    var uploadPath = Server.MapPath("~/Imagens");
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }
            }

            ViewData["Message"] = String.Format("{0} arquivo(s) salvo(s) com sucesso.", arquivosSalvos);
            return View("Upload");
        }
    }
}