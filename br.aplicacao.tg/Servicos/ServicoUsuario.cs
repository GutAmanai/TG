using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
