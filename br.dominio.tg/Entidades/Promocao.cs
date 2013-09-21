using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace br.dominio.tg.Entidades
{
    public class Promocao : EntidadeBase
    {
        public virtual string Nome { get; protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual string ImagemUrl { get; protected set; }

        private ICollection<ClientePromocao> _clientePromocao;
        public virtual IEnumerable<ClientePromocao> ClientePromocao
        {
            get { return _clientePromocao; }
        }

        private ICollection<PromocaoAcesso> _promocaoAcessos;
        public virtual IEnumerable<PromocaoAcesso> PromocaoAcessos
        {
            get { return _promocaoAcessos; }
        }

        public Promocao()
        {
            this.DataEntrada = DateTime.Now;
        }
        public virtual void AdicionarNome(string nome)
        {
            this.Nome = nome;
        }
        public virtual void AdicionarDescricao(string descricao)
        {
            this.Descricao = descricao;
        }

        public virtual void AdicionarAcesso(PromocaoAcesso promocaoAcesso)
        {
            if (this._promocaoAcessos == null)
                this._promocaoAcessos = new Collection<PromocaoAcesso>();
            this._promocaoAcessos.Add(promocaoAcesso);
        }
    }
}
