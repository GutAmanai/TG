using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using br.aplicacao.tg.DTO;
using br.aplicacao.tg.ViewModel;
using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoCliente
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly ServicoCriptografia _servicoCriptografia;
        private readonly IRepositorioCliente _repositorioCliente;

        public ServicoCliente(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioCliente repositorioCliente
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _servicoCriptografia = new ServicoCriptografia();
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

            if (consumidor.Senha.ToLower() == _servicoCriptografia.Encrypt(senha).ToLower())
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
                    cliente.AdicionarEmail(dtoCliente.Email);
                    cliente.AdicionarDocumento(dtoCliente.Cnpj);
                    cliente.AdicionarDataEntrada(DateTime.Now);
                    cliente.AdicionarContato(dtoCliente.Contato);
                    cliente.AdicionarResponsavel(dtoCliente.Responsavel);
                    cliente.AdicionarSenha(_servicoCriptografia.Encrypt(dtoCliente.Senha));
                    _repositorioCliente.Alterar(cliente);
                }
                else // Inclusao
                {
                    var cliente = new Cliente();
                    cliente.AdicionarNome(dtoCliente.Nome);
                    cliente.AdicionarEmail(dtoCliente.Email);
                    cliente.AdicionarDocumento(dtoCliente.Cnpj);
                    cliente.AdicionarDataEntrada(DateTime.Now);
                    cliente.AdicionarContato(dtoCliente.Contato);
                    cliente.AdicionarResponsavel(dtoCliente.Responsavel);
                    cliente.AdicionarSenha(_servicoCriptografia.Encrypt(dtoCliente.Senha));
                    _repositorioCliente.Adicionar(cliente);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private Cliente ObterClientePorId(int id)
        {
            return _repositorioCliente.ObterPorId(id);
        }
        
        private Cliente ObterClientePorEmail(string email)
        {
            return _repositorioCliente.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
        }

        public ViewModelCliente ObterViewModelCliente(int id)
        {
            var cliente = ObterClientePorId(id);
            if(cliente != null)
            {
                return new  ViewModelCliente()
                            {
                                IdCliente = cliente.Id,
                                Cnpj = cliente.Documento,
                                Contato = cliente.Contato,
                                Email = cliente.Email,
                                FotoUrl = "",
                                Nome = cliente.Nome,
                                Responsavel = cliente.Responsavel,
                                Senha = _servicoCriptografia.Decrypt(cliente.Senha)
                            };
            }
            else
            {
                return new ViewModelCliente();
            }
        }

        public ViewModelCliente ObterViewModelCliente(string email)
        {
            var cliente = ObterClientePorEmail(email);
            if (cliente != null)
            {
                return new ViewModelCliente()
                {
                    IdCliente = cliente.Id,
                    Cnpj = cliente.Documento,
                    Contato = cliente.Contato,
                    Email = cliente.Email,
                    FotoUrl = "",
                    Nome = cliente.Nome,
                    Responsavel = cliente.Responsavel,
                    Senha = _servicoCriptografia.Decrypt(cliente.Senha)
                };
            }
            else
            {
                return new ViewModelCliente();
            }
        }

        public bool EmailIsExist(string email)
        {
            return ObterClientePorEmail(email) != null;
        }
    }
}
