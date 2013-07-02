using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool VerificaExistenciaUsuario(string email)
        {
            var consumidor = _repositorioCliente.ObterTodosOnde(x => x.Email == email).FirstOrDefault();
            return consumidor != null;
        }

        public bool ValidarUsuario(string email, string senha)
        {
            var consumidor = _repositorioCliente.ObterTodosOnde(x => x.Email == email).FirstOrDefault();

            if (consumidor == null)
                return false;

            if (consumidor.Senha.ToLower() == _servicoCriptografia.GetMD5Hash(senha).ToLower())
                return true;

            return false;
        }       
    }
}
