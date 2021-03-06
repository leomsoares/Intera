﻿using System;
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
            using (PessoaModel model = new PessoaModel())
            {
                lista = model.Search(busca);
            }


            List<Pessoa> listManage = new List<Pessoa>();
            foreach (var item in lista)
            {
                switch (item.Status)
                {
                    case 1:
                        using (PessoaModel model = new PessoaModel())
                        {
                            listManage.Add((Aluno)model.ReadAluno(item.IdPessoa));
                        }
                        break;
                    case 2:
                        using (PessoaModel model = new PessoaModel())
                        {
                            listManage.Add((Professor)model.ReadProfessor(item.IdPessoa));
                        }
                        break;
                }
            }

            return View(listManage);
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
            if (Session["user"] != null)
            {
                Pessoa u = new Pessoa();
                u = (Pessoa)Session["user"];
                ViewBag.user = u.Nome;
                ViewBag.Status = u.Status;
            }
            int verificar = 0;
            verificar = Convert.ToInt32(form["Type"]);

            Aluno a = new Aluno();
            Professor p = new Professor();
            string email = form["Email"];
            using (PessoaModel model = new PessoaModel())
            {
                bool retorno = false;
                retorno = model.VerificarEmail(email);
                if (retorno)
                {
                    ViewBag.MensagemErroEmail = "This email is already in use";
                    return View();
                }
                else
                {
                    if (verificar == 1)
                    {
                        a.Nome = form["Nome"];
                        a.Email = form["Email"];
                        a.Senha = form["Senha"];
                        a.Ra = form["RaRs"];
                        a.Curso = form["Curso"];

                        int id = model.CreateAluno(a);
                        model.CreateSocial(id);
                    }
                    else if (verificar == 2)
                    {
                        p.Nome = form["Nome"];
                        p.Email = form["Email"];
                        p.Senha = form["Senha"];
                        p.Rs = form["RaRs"];

                        int id = model.CreateProfessor(p);
                        model.CreateSocial(id);
                    }
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
                if (p.Status == 1)
                {
                    ViewBag.Update = model2.UpdateReadAluno(id);
                    return View();
                }
                ViewBag.Update = model2.UpdateReadProfessor(id);
                return View();
            }
        }

        [HttpPost]
        public ActionResult update(int id, FormCollection form)
        {
            if (Session["user"] != null)
            {
                Pessoa u = new Pessoa();
                u = (Pessoa)Session["user"];
                ViewBag.user = u.Nome;
                ViewBag.Status = u.Status;
            }

            int verificar = 0;
            verificar = Convert.ToInt32(form["Type"]);
            Aluno a = new Aluno();
            Professor p = new Professor();
            string oldSenha = form["oldPassword"];
            string email = form["Email"];

            if (verificar == 1)
            {
                a.IdPessoa = id;
                a.Nome = form["Nome"];
                a.Email = form["Email"];
                a.Senha = form["Senha"];
                a.Ra = form["RaRs"];
                a.Curso = form["Curso"];
            }
            else if (verificar == 2)
            {
                p.IdPessoa = id;
                p.Nome = form["Nome"];
                p.Email = form["Email"];
                p.Senha = form["Senha"];
                p.Rs = form["RaRs"];
            }

            using (PessoaModel model = new PessoaModel())
            {
                Session["Mensagem"] = null;
                bool retornoEmail = false;

                string emailAntigo = model.GetEmail(id);
                if (emailAntigo != email)
                {
                    retornoEmail = model.VerificarEmail(email);
                }

                if (retornoEmail == false)
                {
                    if (verificar == 1)
                    {
                        if (oldSenha != "")
                        {
                            bool retornoSenha = model.VerificarSenha(oldSenha, id);
                            if (retornoSenha)
                            {
                                model.UpdateAluno(a);
                                model.TrocarSenha(a.Senha, a.IdPessoa);
                            }
                            else
                            {
                                ViewBag.MensagemSenha = "Invalid old password!";
                                ViewBag.Update = model.UpdateReadAluno(a.IdPessoa);
                                return View();
                            }
                        }
                        else
                        {
                            model.UpdateAluno(a);
                        }
                    }
                    else if (verificar == 2)
                    {
                        if (oldSenha != "")
                        {
                            bool retorno = model.VerificarSenha(oldSenha, p.IdPessoa);
                            if (retorno)
                            {
                                model.UpdateProfessor(p);
                                model.TrocarSenha(p.Senha, p.IdPessoa);
                            }
                            else
                            {
                                ViewBag.MensagemSenha = "Invalid old password!";
                                ViewBag.Update = model.UpdateReadProfessor(p.IdPessoa);
                                return View();
                            }
                        }
                        else
                        {
                            model.UpdateProfessor(p);
                        }
                    }
                }
                else
                {
                    ViewBag.MensagemErroEmail = "This email is already in use";
                    ViewBag.Update = model.UpdateReadAluno(id);
                    return View();
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
            Pessoa p = new Pessoa();
            if (Session["user"] != null)
            {
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
                ViewBag.Email = p.Email;
                ViewBag.Status = p.Status;
            }
            List<Social> lista = new List<Social>();
            using (PessoaModel model = new PessoaModel())
            {
                lista = model.ReadSocialAluno(p.IdPessoa);
                if (p.Status == 1)
                {
                    ViewBag.Projetos = model.ReadProjetoProfileAluno(p.IdPessoa);
                }
                else if (p.Status == 2)
                {
                    ViewBag.Projetos = model.ReadProjetoProfileProf(p.IdPessoa);
                }
                else if (p.Status == 3)
                {
                    ViewBag.Projetos = null;
                }
            }

            return View(lista);
        }

        public ActionResult editprofile()
        {
            Pessoa u = new Pessoa();
            List<Social> social = new List<Social>();
            if (Session["user"] != null)
            {
                u = (Pessoa)Session["user"];
                ViewBag.IdPessoa = u.IdPessoa;
                ViewBag.user = u.Nome;
                ViewBag.Email = u.Email;
                ViewBag.Status = u.Status;
                ViewBag.Senha = u.Senha;
            }

            using (PessoaModel model = new PessoaModel())
            {
                social = model.ReadSocial(u.IdPessoa);

            }

            return View(social);
        }

        [HttpPost]
        public ActionResult editprofile(FormCollection form)
        {
            Pessoa pessoa = new Pessoa();
            List<Social> lista = new List<Social>();

            string oldSenha = form["oldPassword"];
            pessoa = (Pessoa)Session["user"];
            ViewBag.IdPessoa = pessoa.IdPessoa;
            ViewBag.user = pessoa.Nome;
            ViewBag.Email = pessoa.Email;
            ViewBag.Status = pessoa.Status;
            ViewBag.Senha = pessoa.Senha;


            pessoa.Email = form["Email"];
            pessoa.Senha = form["Senha"];

            lista.Add(new Social { Nick = form["Facebook"], IdSocial = 1 });
            lista.Add(new Social { Nick = form["Twitter"], IdSocial = 2 });
            lista.Add(new Social { Nick = form["GooglePlus"], IdSocial = 3 });
            lista.Add(new Social { Nick = form["Linkedin"], IdSocial = 4 });

            using (PessoaModel model = new PessoaModel())
            {
                if (oldSenha != "")
                {
                    bool retorno = model.VerificarSenha(oldSenha, pessoa.IdPessoa);
                    if (retorno)
                    {
                        model.UpdateProfile(pessoa, lista);
                        model.TrocarSenha(pessoa.Senha, pessoa.IdPessoa);
                    }
                    else
                    {
                        ViewBag.MensagemSenha = "Invalid old password!";
                        List<Social> listaErro = model.ReadSocial(pessoa.IdPessoa);
                        return View(listaErro);
                    }
                }
                else
                {
                    model.UpdateProfile(pessoa, lista);
                }
            }

            return RedirectToAction("Profile");
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