﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.dominio.tg.Entidades
{
    public class Exemplo : EntidadeBase
    {
        public virtual string Nome { get; protected set; }
        public virtual DateTime Data { get; protected set; }
    }
}