using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoPromocao
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly ServicoCriptografia _servicoCriptografia;
        private readonly IRepositorioCliente _repositorioCliente;

        public ServicoPromocao(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioCliente repositorioCliente
                             )
        {
            _unidadeDeTrabalho = unidadeDeTrabalho;
            _servicoCriptografia = new ServicoCriptografia();
            _repositorioCliente = repositorioCliente;
        }



    }
}
