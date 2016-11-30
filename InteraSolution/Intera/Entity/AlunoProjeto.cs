using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intera.Entity
{
    public class AlunoProjeto : Aluno
    {
        public int IdProjeto { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}