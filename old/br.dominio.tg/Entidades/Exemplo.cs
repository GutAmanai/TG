using System;

namespace br.dominio.tg.Entidades
{
    public class Exemplo : EntidadeBase
    {
        public virtual string Nome { get; protected set; }
        public virtual DateTime Data { get; protected set; }
    }
}
