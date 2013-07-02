using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public class RepositorioCliente :RepositorioBase<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
