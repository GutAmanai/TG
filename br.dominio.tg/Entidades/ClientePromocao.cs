using System;

namespace br.dominio.tg.Entidades
{
    public class ClientePromocao : EntidadeBase
    {
        public virtual Cliente Cliente { get; protected set; }
        public virtual Promocao Promocao { get; protected set; }
        public virtual bool Ativo { get; protected set; }
        public virtual DateTime DataExpiracao { get; protected set; }

        public ClientePromocao(Cliente cliente, Promocao promocao)
        {
            this.Cliente = cliente;
            this.Promocao = promocao;
        }
    }
}
