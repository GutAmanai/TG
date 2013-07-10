using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace br.dominio.tg.Entidades
{
    public class Usuario : EntidadeBase
    {
        public virtual string Nome { get; protected set; }
        public virtual DateTime DataEntrada { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Contato { get; protected set; }
        public virtual string Senha { get; protected set; }

        private ICollection<QualificacaoPromocao> _qualificacaoPromocao;
        public virtual IEnumerable<QualificacaoPromocao> QualificacaoPromocao
        {
            get { return _qualificacaoPromocao; }
        }

        public virtual void AdicionarQualificacaoPromocao(QualificacaoPromocao qualificacaoPromocao)
        {
            if(_qualificacaoPromocao == null)
                _qualificacaoPromocao = new Collection<QualificacaoPromocao>();

            _qualificacaoPromocao.Add(qualificacaoPromocao);
        }

        protected Usuario()
        {
        }

        public Usuario(string nome, string email, string contato, string senha) :this()
        {
            this.Nome = nome;
            this.Email = email;
            this.Contato = contato;
            this.Senha = senha;
            this.DataEntrada = DateTime.Now;
        }
    }
}
