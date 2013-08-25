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
        public virtual DateTime DataEntrada { get; protected set; }

        protected ClientePromocao()
        {
            this.DataEntrada = DateTime.Now;
        }

        public ClientePromocao(Cliente cliente, Promocao promocao) : this()
        {
            this.Cliente = cliente;
            this.Promocao = promocao;
        }

        public virtual void AdicionarDataLiberacao(DateTime liberacao)
        {
            this.DataLiberacao = liberacao;
        }

        public virtual void AdicionarDataExpiracao(DateTime expiracao)
        {
            this.DataExpiracao = expiracao;
        }

        public virtual void AdicionarStatus(bool ativo)
        {
            this.Ativo = ativo;
        }
    }
}
