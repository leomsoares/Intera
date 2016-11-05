﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intera.Models;
using Intera.Entity;

namespace Intera.Controllers
{
    [Autoriza]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult manage()
        {
            if (Session["user"] != null)
            {
                Pessoa p = new Pessoa();
                p = (Pessoa)Session["user"];
                ViewBag.user = p.Nome;
            }

            PessoaModel model = new PessoaModel();
            List<Pessoa> lista = model.Read();


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
                Pessoa user = new Pessoa();
                user = (Pessoa)Session["user"];
                ViewBag.user = user.Nome;
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
        public ActionResult update(FormCollection form)
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
                    model.UpdateAluno(a);
                }
                else if (verificar == 2)
                {
                    model.UpdateProfessor(p);
                }
            }
            return View();
        }
    }
}