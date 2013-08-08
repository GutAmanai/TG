using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace br.dominio.tg.Entidades
{
    public class Cliente : EntidadeBase
    {
        public virtual string Nome { get;  protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual string Documento { get; protected set; }
        public virtual string Responsavel { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Contato { get; protected set; }
        public virtual string FotoUrl { get; protected set; }
        public virtual string Senha { get; protected set; }

        private ICollection<ClientePromocao> _clientePromocao;
        public virtual IEnumerable<ClientePromocao> ClientePromocao
        {
            get { return _clientePromocao; }
        }

        private ICollection<ClienteLocalizacao> _clienteLocalizacao;
        public virtual IEnumerable<ClienteLocalizacao> ClienteLocalizacao
        {
            get { return _clienteLocalizacao; }
        }

        public virtual void AdicionarClientePromocao(ClientePromocao clientePromocao)
        {
            if(_clientePromocao == null)
                _clientePromocao = new Collection<ClientePromocao>();

            _clientePromocao.Add(clientePromocao);
        }

        public Cliente()
        {
            
        }

        public virtual void AdicionarNome(string nome)
        {
            this.Nome = nome;
        }

        public virtual void AdicionarEmail(string email)
        {
            this.Email = email;
        }

        public virtual void AdicionarDocumento(string cnpj)
        {
            this.Documento = cnpj;
        }

        public virtual void AdicionarDataEntrada(DateTime now)
        {
            this.DataEntrada = now;
        }

        public virtual void AdicionarContato(string contato)
        {
            this.Contato = contato;
        }

        public virtual void AdicionarSenha(string senha)
        {
            this.Senha = senha;
        }

        public virtual void AdicionarResponsavel(string responsavel)
        {
            this.Responsavel = responsavel;
        }

        public virtual void RemoverLocalizacao()
        {
            if(this._clienteLocalizacao == null)
                this._clienteLocalizacao = new Collection<ClienteLocalizacao>();
            this._clienteLocalizacao.Clear();
        }

        public virtual void AdicionarLocalizacao(ClienteLocalizacao clienteLocalizacao)
        {
            if (this._clienteLocalizacao == null)
                this._clienteLocalizacao = new Collection<ClienteLocalizacao>();
            this._clienteLocalizacao.Add(clienteLocalizacao);
        }
    }
}
