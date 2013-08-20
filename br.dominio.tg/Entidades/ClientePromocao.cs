using System;

namespace br.dominio.tg.Entidades
{
    public class ClientePromocao : EntidadeBase
    {
        public virtual Cliente Cliente { get; protected set; }
        public virtual Promocao Promocao { get; protected set; }
        public virtual bool Ativo { get; protected set; }
        public virtual DateTime DataExpiracao { get; protected set; }
        public virtual DateTime DataLiberacao { get; protected set; }

        protected ClientePromocao()
        {
        }

        public ClientePromocao(Cliente cliente, Promocao promocao) : this()
        {
            this.Cliente = cliente;
            this.Promocao = promocao;
        }
    }
}
