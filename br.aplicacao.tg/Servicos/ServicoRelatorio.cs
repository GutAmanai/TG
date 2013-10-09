using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.aplicacao.tg.DTO.DTORelatorioAcesso;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoRelatorio
    {
        private readonly IRepositorioClientePromocao _repositorioClientePromocao;

        public ServicoRelatorio(
                                    IRepositorioClientePromocao repositorioClientePromocao
                               )
        {
           _repositorioClientePromocao = repositorioClientePromocao;
        }

        public List<DTOPromocaoQuantidade> ObterDadosAcesso(int idCliente)
        {
            var lista =  
                _repositorioClientePromocao
                .ObterTodosOnde(x => x.Cliente.Id == idCliente)
                .Select(
                    x =>
                    new DTOPromocaoQuantidade()
                        {
                            IdPromocao = x.Promocao.Id,
                            Nome = x.Promocao.Nome,
                            Acessos = x.Promocao.PromocaoAcessos
                                                    .GroupBy(y => y.DataEntrada.Date)
                                                    .Select(y => new DTOAcessos( y.Count(), y.Key))
                                                    .OrderBy(y => y.Data).ToList()
                        }
                )
                .ToList();
            
            return lista;
        }
    }
}
