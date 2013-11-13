using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using br.aplicacao.tg.DTO;
using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoUsuario
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioUsuario repositorioUsuario
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioUsuario = repositorioUsuario;
        }

        public DTOUsuario CriarUsuario(string stDtoComunicacao)
        {
            DTOUsuario objUsuario;

            try
            {
                objUsuario = new JavaScriptSerializer().Deserialize<DTOUsuario>(stDtoComunicacao);

                if(!VerificaExistenciaUsuario(objUsuario.Email))
                {
                    var consumidor = new Usuario(
                                                        objUsuario.Nome,
                                                        objUsuario.Email,
                                                        objUsuario.Contato,
                                                        ServicoCriptografia.Encrypt(objUsuario.Senha).ToLower() 
                                                    );
                    
                    _repositorioUsuario.Adicionar(consumidor);
                    _unidadeDeTrabalho.Salvar();
                }

                return ObterUsuario(objUsuario.Email);
            }
            catch (Exception ex)
            {
                ExceptionCustom.Log(ex);
                throw;
            }    
        }

        public DTOUsuario ObterUsuario(string email)
        {
            var usuario = _repositorioUsuario.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
            var dto = new DTOUsuario()
            {
                Contato = usuario.Contato,
                DataEntrada = usuario.DataEntrada,
                Email = usuario.Email,
                Nome = usuario.Nome,
                Senha = ServicoCriptografia.Decrypt(usuario.Senha)
            };
            return dto;
        }

        public bool VerificaExistenciaUsuario(string email)
        {
            var consumidor = _repositorioUsuario.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
            return consumidor != null;
        }

        public bool ValidarUsuario(string email, string senha)
        {
            var consumidor = _repositorioUsuario.ObterTodosOnde(x => x.Email == email).FirstOrDefault();

            if (consumidor == null)
                return false;

            if (consumidor.Senha.ToLower() == ServicoCriptografia.Encrypt(senha).ToLower())
                return true;

            return false;
        }       
    }
}
