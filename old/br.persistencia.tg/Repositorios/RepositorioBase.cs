using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;
using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        protected RepositorioBase(IUnidadeDeTrabalho unidadeDeTrabalho)
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        protected ISession Sessao
        {
            get { return ((UnidadeDeTrabalho)_unidadeDeTrabalho).Sessao; }
        }

        protected IStatelessSession SessaoSemEstado
        {
            get { return ((UnidadeDeTrabalho)_unidadeDeTrabalho).SessaoSemEstado; }
        }

        public virtual int Count(Expression<Func<T, bool>> expressao)
        {
            return Sessao.Query<T>().Count(expressao);
        }

        public virtual bool Any(Expression<Func<T, bool>> expressao)
        {
            return Sessao.Query<T>().Any(expressao);
        }

        public virtual bool Contains(T entidade)
        {
            return Sessao.Query<T>().Contains(entidade);
        }

        public virtual T ObterPorId(int id)
        {
            return Sessao.Get<T>(id);
        }

        public virtual ICollection<T> ObterTodos()
        {
            return Sessao.Query<T>().ToList();
        }

        public virtual IQueryable<T> ObterTodosOndeLazy(Expression<Func<T, bool>> expressao)
        {
            return Sessao.Query<T>().Where(expressao);
        }

        public IQueryable<T> ObterTodosLazy()
        {
            return Sessao.Query<T>();
        }

        public virtual IEnumerable<T> ObterTodosOnde(Expression<Func<T, bool>> expressao)
        {
            return Sessao.Query<T>().Where(expressao).ToList();
        }

        public virtual void Adicionar(T entidade)
        {
            _unidadeDeTrabalho.Inicializar(); 
            Sessao.Save(entidade);
            _unidadeDeTrabalho.Salvar();
        }

        public virtual void Adicionar(IEnumerable<T> entidades)
        {
            _unidadeDeTrabalho.Inicializar(); 
            foreach (var entidade in entidades)
                SessaoSemEstado.Insert(entidade);
            _unidadeDeTrabalho.Salvar();
        }

        public virtual void Remover(T entidade)
        {
            _unidadeDeTrabalho.Inicializar(); 
            Sessao.Delete(entidade);
            _unidadeDeTrabalho.Salvar();
        }

        public virtual void Remover(IEnumerable<T> entidades)
        {
            _unidadeDeTrabalho.Inicializar(); 
            foreach (var entidade in entidades)
                SessaoSemEstado.Delete(entidade);
            _unidadeDeTrabalho.Salvar();
        }

        public void Alterar(T entidade)
        {
            _unidadeDeTrabalho.Inicializar(); 
            Sessao.Update(entidade);
            _unidadeDeTrabalho.Salvar();
        }
    }
}
