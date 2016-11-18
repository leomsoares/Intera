using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intera.Entity
{
    public class Mensagem : Pessoa
    {
        public int IdMensagem { get; set; }
        public string Mensagem { get; set; }
    }
}