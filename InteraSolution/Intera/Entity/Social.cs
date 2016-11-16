using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intera.Entity
{
    public class Social : Pessoa
    {
        public int IdSocial { get; set; }
        public string Nick { get; set; }
    }
}