using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intera.Entity
{
    public class Projeto
    {
        public int IdProjeto { get; set; }
        public int IdProfessor { get; set; }
        public int IdCoorientador { get; set; }
        public int IdTipo { get; set; }
        public string NomeProjeto { get; set; }
        public int Status { get; set; }
        public string Link { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public string Descricao { get; set; }
    }
}