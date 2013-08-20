using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public class RepositorioClientePromocao : RepositorioBase<ClientePromocao>, IRepositorioClientePromocao
    {
        public RepositorioClientePromocao(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
