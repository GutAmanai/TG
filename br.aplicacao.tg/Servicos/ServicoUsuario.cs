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
        private readonly IRepositorioConsumidor _repositorioConsumidor;
        private readonly ServicoCriptografia _servicoCriptografia;

        public ServicoUsuario(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioConsumidor repositorioConsumidor
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioConsumidor = repositorioConsumidor;
            _servicoCriptografia = new ServicoCriptografia();
        }

        public DTOConsumidor CriarConsumidor(string stDtoComunicacao)
        {
            DTOConsumidor objConsumidor;

            try
            {
                objConsumidor = new JavaScriptSerializer().Deserialize<DTOConsumidor>(stDtoComunicacao);

                if(!VerificaExistenciaUsuario(objConsumidor.Email))
                {
                    var consumidor = new Consumidor(
                                                        objConsumidor.Nome,
                                                        objConsumidor.Email,
                                                        objConsumidor.Contato,
                                                        _servicoCriptografia.GetMD5Hash(objConsumidor.Senha).ToLower() 
                                                    );
                    
                    _repositorioConsumidor.Adicionar(consumidor);
                    _unidadeDeTrabalho.Salvar();
                }

                return ObterConsumidor(objConsumidor.Email);
            }
            catch (Exception)
            {
                
                throw;
            }    
        }

        public DTOConsumidor ObterConsumidor(string email)
        {
            var consumidor = _repositorioConsumidor.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
            var dto = new DTOConsumidor()
            {
                Contato = consumidor.Contato,
                DataEntrada = consumidor.DataEntrada,
                Email = consumidor.Email,
                Nome = consumidor.Nome,
                Senha = consumidor.Senha
            };
            return dto;
        }

        public bool VerificaExistenciaUsuario(string email)
        {
            var consumidor = _repositorioConsumidor.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
            return consumidor != null;
        }

        public bool ValidarUsuario(string email, string senha)
        {
            var consumidor = _repositorioConsumidor.ObterTodosOnde(x => x.Email == email).FirstOrDefault();

            if (consumidor == null)
                return false;

            if (consumidor.Senha.ToLower() == _servicoCriptografia.GetMD5Hash(senha).ToLower())
                return true;

            return false;
        }       
    }
}
