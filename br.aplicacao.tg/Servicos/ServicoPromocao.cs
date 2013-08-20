﻿using System;
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
        }

        public bool SalvarPromocao(DTOPromocao dtoPromocao)
        {
            try
            {
                if (dtoPromocao.IdPromocao != 0) // Edicao
                {
                    var promocao = ObterPromocaoPorId(dtoPromocao.IdPromocao);
                    promocao.AdicionarNome(dtoPromocao.Nome);
                    promocao.AdicionarDescricao(dtoPromocao.Descricao);
                    promocao.AdicionarDataEntrada(DateTime.Now);
                    promocao.AdicionarDataLiberacao(dtoPromocao.DataLiberacao);
                    _repositorioPromocao.Alterar(promocao);
                }
                else // Inclusao
                {
                    var promocao = new Promocao();
                    promocao.AdicionarNome(dtoPromocao.Nome);
                    promocao.AdicionarDescricao(dtoPromocao.Descricao);
                    promocao.AdicionarDataEntrada(DateTime.Now);
                    promocao.AdicionarDataLiberacao(dtoPromocao.DataLiberacao);
                    _repositorioPromocao.Adicionar(promocao);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
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
            clientePromocao.Promocao = listaPromocoes.Select(ObterPromocao).ToList();
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

        public DTOPromocao ObterPromocao(ClientePromocao clientePromocao)
        {
            return new DTOPromocao
            {
                IdPromocao = clientePromocao.Promocao.Id,
                Nome = clientePromocao.Promocao.Nome,
                Ativo = clientePromocao.Ativo,
                DataCadastro = clientePromocao.Promocao.DataEntrada,
                DataLiberacao = clientePromocao.DataLiberacao,
                DataExpiracao = clientePromocao.DataExpiracao,
                Descricao = clientePromocao.Promocao.Descricao,
                ImagemUrl = "",
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
