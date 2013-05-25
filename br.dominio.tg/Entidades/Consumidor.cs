using System;

namespace br.dominio.tg.Entidades
{
    public class Consumidor : EntidadeBase
    {
        public virtual string Nome { get; set; }
        public virtual DateTime DataEntrada { get; set; }
        public virtual string Email { get; set; }
        public virtual string Contato { get; set; }
        public virtual string Senha { get; set; }
    }
}
