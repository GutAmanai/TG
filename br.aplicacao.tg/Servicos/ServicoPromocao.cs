using System;
using System.Collections.Generic;
using System.Linq;
using br.aplicacao.tg.DTO;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;
using br.dominio.tg.Entidades;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoPromocao
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioPromocao _repositorioPromocao;
        private readonly ServicoCriptografia _servicoCriptografia;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IRepositorioClientePromocao _repositorioClientePromocao;
        private readonly ServicoImagem ServicoImagem;

        public ServicoPromocao(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioPromocao repositorioPromocao,
                                IRepositorioCliente repositorioCliente,
                                IRepositorioClientePromocao repositorioClientePromocao
                             )
        {

            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioPromocao = repositorioPromocao;
            _repositorioCliente = repositorioCliente;
            _repositorioClientePromocao = repositorioClientePromocao;
            _servicoCriptografia = new ServicoCriptografia();
            ServicoImagem = new ServicoImagem(); 
        }

        public DTOPromocao SalvarPromocao(DTOPromocao dtoPromocao)
        {
            try
            {
                var cliente = _repositorioCliente.ObterPorId(dtoPromocao.IdCliente);
                ClientePromocao clientePromocao;

                if (dtoPromocao.IdPromocao != 0) // Edicao
                {
                    clientePromocao =_repositorioClientePromocao.ObterTodosOnde(x => x.Cliente.Id == dtoPromocao.IdCliente && x.Promocao.Id == dtoPromocao.IdPromocao).FirstOrDefault();
                    clientePromocao.Promocao.AdicionarNome(dtoPromocao.Nome);
                    clientePromocao.Promocao.AdicionarDescricao(dtoPromocao.Descricao);
                    clientePromocao.AdicionarDataLiberacao(dtoPromocao.DataLiberacaoToDate);
                    clientePromocao.AdicionarDataExpiracao(dtoPromocao.DataExpiracaoToDate);
                    clientePromocao.AdicionarStatus(dtoPromocao.Ativo);
                    _repositorioClientePromocao.Alterar(clientePromocao);
                }
                else // Inclusao
                {
                    var promocao = new Promocao();
                    promocao.AdicionarNome(dtoPromocao.Nome);
                    promocao.AdicionarDescricao(dtoPromocao.Descricao);

                    clientePromocao = new ClientePromocao(cliente, promocao);
                    clientePromocao.AdicionarDataLiberacao(dtoPromocao.DataLiberacaoToDate);
                    clientePromocao.AdicionarDataExpiracao(dtoPromocao.DataExpiracaoToDate);
                    clientePromocao.AdicionarStatus(dtoPromocao.Ativo);
                    
                    cliente.AdicionarClientePromocao(clientePromocao);
                    _repositorioCliente.Alterar(cliente);
                }

                return ObterPromocaoPorClientePromocao(clientePromocao);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Promocao ObterPromocaoPorId(int id)
        {
            return _repositorioPromocao.ObterPorId(id);
        }
        
        private Cliente ObterClientePorId(int id)
        {
            return _repositorioCliente.ObterPorId(id);
        }

        public DTORetornoPesquisaPromocao ObterDTOPromocao(DTOPesquisaPromocao pesquisaPromocao)
        {
            int NPaginas = 0;
            int NLinhas = 0;

            var listaPromocoes =
                _repositorioClientePromocao
                .ObterTodosOndeLazy(x => x.Cliente.Id == pesquisaPromocao.IdCliente)
                .Page(
                        pesquisaPromocao.NPagina,
                        pesquisaPromocao.QtdPagina,
                        x => x.DataExpiracao,
                        true,
                        out NLinhas,
                        out NPaginas
                    ).ToList();


            var clientePromocao = new DTORetornoPesquisaPromocao();
            clientePromocao.IdCliente = pesquisaPromocao.IdCliente;
            clientePromocao.NLinhas = NLinhas;
            clientePromocao.NPaginas = NPaginas;
            clientePromocao.Localizacoes = listaPromocoes.SelectMany(x => ObterLocalizacao(x.Cliente.ClienteLocalizacao)).ToList();
            clientePromocao.Promocao = listaPromocoes.Select(ObterPromocaoPorClientePromocao).ToList();
            return clientePromocao;
        }

        private IEnumerable<DTOLocalizacao> ObterLocalizacao(IEnumerable<ClienteLocalizacao> clienteLocalizacao)
        {
            foreach (var localizacao in clienteLocalizacao)
            {
                yield return new DTOLocalizacao
                                 {
                                     IdLocalizacao = localizacao.Id,
                                     Latitude = localizacao.Latitude,
                                     Longitude = localizacao.Longitude
                                 };
            }
        }

        public DTOPromocao ObterPromocaoPorClientePromocao(ClientePromocao clientePromocao)
        {
            return new DTOPromocao
            {
                IdPromocao = clientePromocao.Promocao.Id,
                IdCliente = clientePromocao.Cliente.Id,
                Nome = clientePromocao.Promocao.Nome,
                Ativo = clientePromocao.Ativo,
                DataCadastro = clientePromocao.Promocao.DataEntrada.ToString(),
                DataLiberacao = clientePromocao.DataLiberacao.ToString(),
                DataExpiracao = clientePromocao.DataExpiracao.ToString(),
                Descricao = clientePromocao.Promocao.Descricao,
                ImagemUrl = ServicoImagem.RecuperaImagemPromocao(clientePromocao.Cliente.Id, clientePromocao.Promocao.Id),
                TempImg = ""
            };
        }

        public DTOLocalizacao ObterLocalizacao(ClienteLocalizacao clienteLocalizacao)
        {
            return new DTOLocalizacao
            {
                IdLocalizacao = clienteLocalizacao.Id,
                Latitude = clienteLocalizacao.Latitude,
                Longitude = clienteLocalizacao.Longitude
            };
        }
    }
}
