﻿using System;
using System.Collections.Generic;
using System.Linq;
using br.aplicacao.tg.DTO;
using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoCliente
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioCliente _repositorioCliente;

        public ServicoCliente(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioCliente repositorioCliente
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioCliente = repositorioCliente;
        }

        public bool VerificaExistenciaCliente(string email)
        {
            var consumidor = _repositorioCliente.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
            return consumidor != null;
        }

        public bool ValidarCliente(string email, string senha)
        {
            var consumidor = _repositorioCliente.ObterTodosOnde(x => x.Email == email).FirstOrDefault();

            if (consumidor == null)
                return false;

            if (consumidor.Senha.ToLower() == ServicoCriptografia.Encrypt(senha).ToLower())
                return true;

            return false;
        }

        public bool SalvarCliente(DTOCliente dtoCliente)
        {
            try
            {
                if (dtoCliente.IdCliente != 0) // Edicao
                {
                    var cliente = ObterClientePorId(dtoCliente.IdCliente);
                    cliente.AdicionarNome(dtoCliente.Nome);
                    cliente.AdicionarEmail(dtoCliente.Email.ToLower());
                    cliente.AdicionarDocumento(dtoCliente.Cnpj);
                    cliente.AdicionarDataEntrada(DateTime.Now);
                    cliente.AdicionarContato(dtoCliente.Contato);
                    cliente.AdicionarResponsavel(dtoCliente.Responsavel);
                    cliente.AdicionarSenha(ServicoCriptografia.Encrypt(dtoCliente.Senha));

                    // -- Adiciona Remove e adiciona Localização
                    cliente.RemoverLocalizacao();

                    foreach (var localizacao in dtoCliente.Localizacoes)
                        cliente.AdicionarLocalizacao(new ClienteLocalizacao(cliente,localizacao.Latitude,localizacao.Longitude));

                    _repositorioCliente.Alterar(cliente);
                }
                else // Inclusao
                {
                    var cliente = new Cliente();
                    cliente.AdicionarNome(dtoCliente.Nome);
                    cliente.AdicionarEmail(dtoCliente.Email.ToLower());
                    cliente.AdicionarDocumento(dtoCliente.Cnpj);
                    cliente.AdicionarDataEntrada(DateTime.Now);
                    cliente.AdicionarContato(dtoCliente.Contato);
                    cliente.AdicionarResponsavel(dtoCliente.Responsavel);
                    cliente.AdicionarSenha(ServicoCriptografia.Encrypt(dtoCliente.Senha));

                    // -- Adiciona Remove e adiciona Localização
                    cliente.RemoverLocalizacao();

                    foreach (var localizacao in dtoCliente.Localizacoes)
                        cliente.AdicionarLocalizacao(new ClienteLocalizacao(cliente, localizacao.Latitude, localizacao.Longitude));

                    _repositorioCliente.Adicionar(cliente);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionCustom.Log(ex);
                return false;
            }
        }

        public DTOCliente ObterDTOCliente(int id)
        {
            return ObterDtoCliente(ObterClientePorId(id));
        }

        public DTOCliente ObterDTOCliente(string email)
        {
            return ObterDtoCliente(ObterClientePorEmail(email));
        }

        public bool EmailIsExist(string email)
        {
            return ObterClientePorEmail(email) != null;
        }

        private Cliente ObterClientePorId(int id)
        {
            return _repositorioCliente.ObterPorId(id);
        }

        private Cliente ObterClientePorEmail(string email)
        {
            return _repositorioCliente.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
        }

        private DTOCliente ObterDtoCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                return new DTOCliente()
                {
                    IdCliente = cliente.Id,
                    Cnpj = cliente.Documento,
                    Contato = cliente.Contato,
                    Email = cliente.Email,
                    FotoUrl = "",
                    Nome = cliente.Nome,
                    Responsavel = cliente.Responsavel,
                    Senha = ServicoCriptografia.Decrypt(cliente.Senha),
                    Localizacoes = ObterLocalizacao(cliente)
                };
            }
            return new DTOCliente();
        }

        public List<DTOLocalizacao> ObterLocalizacao(Cliente cliente)
        {
            var localicacoes = new List<DTOLocalizacao>();
            if (cliente.ClienteLocalizacao != null)
                if (cliente.ClienteLocalizacao.Any())
                    localicacoes.AddRange(cliente.ClienteLocalizacao.Select(x => new DTOLocalizacao(x.Id, x.Latitude, x.Longitude)));
            return localicacoes;
        }
    }
}
