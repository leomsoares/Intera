﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intera.Entity
{
    public class Referencia  : Projeto
    {
        public int IdReferencia { get; set; }
        public string DescricaoReferencia { get; set; }
    }
}