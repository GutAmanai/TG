using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public class RepositorioPromocaoAcesso : RepositorioBase<PromocaoAcesso>, IRepositorioPromocaoAcesso
    {
        public RepositorioPromocaoAcesso(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
