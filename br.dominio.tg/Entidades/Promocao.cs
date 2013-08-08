using System;
using System.Collections.Generic;

namespace br.dominio.tg.Entidades
{
    public class Promocao : EntidadeBase
    {
        public virtual string Nome { get; protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual DateTime DataLiberacao { get; protected set; }
        public virtual DateTime DataExpiracao { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string ImagemUrl { get; protected set; }

        private ICollection<ClientePromocao> _clientePromocao;
        public virtual IEnumerable<ClientePromocao> ClientePromocao
        {
            get { return _clientePromocao; }
        }

        public Promocao()
        {

        }

        public virtual void AdicionarNome(string nome)
        {
            this.Nome = nome;
        }

        public virtual void AdicionarDataEntrada(DateTime now)
        {
            this.DataEntrada = now;
        }

        public virtual void AdicionarDataLiberacao(DateTime liberacao)
        {
            this.DataEntrada = liberacao;
        }

        public virtual void AdicionarDataExpiracao(DateTime expiracao)
        {
            this.DataEntrada = expiracao;
        }

        public virtual void AdicionarDescricao(string descricao)
        {
            this.Descricao = descricao;
        }
    }
}
