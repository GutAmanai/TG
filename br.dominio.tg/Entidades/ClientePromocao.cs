using System;

namespace br.dominio.tg.Entidades
{
    public class ClientePromocao : EntidadeBase
    {
        public virtual Cliente Cliente { get; set; }
        public virtual Promocao Promocao { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual DateTime DataExpiracao { get; set; }

        public ClientePromocao(Cliente cliente, Promocao promocao)
        {
            this.Cliente = cliente;
            this.Promocao = promocao;
        }
    }
}
