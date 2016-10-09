using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intera.Entity
{
    public class Pessoa
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Imagem { get; set; }
    }
}