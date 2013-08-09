using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using br.aplicacao.tg.DTO;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;
using br.aplicacao.tg.ViewModel;
using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.aplicacao.tg.Servicos
{
    public class ServicoPromocao
    {
        private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
        private readonly IRepositorioPromocao _repositorioPromocao;
        private readonly ServicoCriptografia _servicoCriptografia;
        private readonly IRepositorioCliente _repositorioCliente;

        public ServicoPromocao(
                                IUnidadeDeTrabalho unidadeDeTrabalho,
                                IRepositorioPromocao repositorioPromocao,
                                IRepositorioCliente repositorioCliente
                             )
        {

            _unidadeDeTrabalho = unidadeDeTrabalho;
            _repositorioPromocao = repositorioPromocao;
            _repositorioCliente = repositorioCliente;
            _servicoCriptografia = new ServicoCriptografia();
        }

        public bool ValidarPromocao(string nome)
        {
            var promocao = _repositorioPromocao.ObterTodosOnde(x => x.Nome == nome).FirstOrDefault();

            if (promocao == null)
                return false;

            return false;
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

        public ViewModelPromocao ObterViewModelPromocao(int id)
        {
            var promocao = ObterPromocaoPorId(id);
            if (promocao != null)
            {
                return new ViewModelPromocao()
                {
                    IdPromocao = promocao.Id,
                    DataEntrada = promocao.DataEntrada,
                    DataLiberacao = promocao.DataLiberacao,
                    DataExpiracao = promocao.DataExpiracao,
                    ImagemUrl = "",
                    Descricao = promocao.Descricao,
                    Nome = promocao.Nome
                };
            }
            else
            {
                return new ViewModelPromocao()
                {
                    DataLiberacao = DateTime.Today,
                    DataExpiracao = DateTime.Today.AddDays(2).AddSeconds(-1)
                };
            }
        }
    }
}
