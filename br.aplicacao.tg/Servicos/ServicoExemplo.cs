using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.dominio.Entidades;
using br.dominio.Repositorios;
using br.persistencia.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoExemplo
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioExemplo _repositorioExemplo;

        public ServicoExemplo(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioExemplo repositorioExemplo
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioExemplo = repositorioExemplo;
        }

        public Exemplo ObterPorId(int id)
        {
            return _repositorioExemplo.ObterPorId(id);
        }

        public void DeletarExemplo(int id)
        {
            _repositorioExemplo.Remover(_repositorioExemplo.ObterPorId(id));
            _unidadeDeTrabalho.Salvar();
        }

        public void DeletarExemplo(Exemplo exemplo)
        {
            _repositorioExemplo.Remover(exemplo);
            _unidadeDeTrabalho.Salvar();
        }

        public IEnumerable<Exemplo> ObterTodos()
        {
            return _repositorioExemplo.ObterTodos();
        }

    }
}
