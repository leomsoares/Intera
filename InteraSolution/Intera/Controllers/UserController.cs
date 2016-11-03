using System;
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
            List<Pessoa> lista = model.PessoaRead();


            return View(lista);
        }

        [HttpPost]
        public ActionResult manage(FormCollection form)
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

        public ActionResult edit()
        {
            return View();
        }
    }
}