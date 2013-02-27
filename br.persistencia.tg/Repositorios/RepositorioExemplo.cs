using br.dominio.Entidades;
using br.dominio.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public class RepositorioExemplo : RepositorioBase<Exemplo>, IRepositorioExemplo
    {
        public RepositorioExemplo(IUnidadeDeTrabalho unidadeDeTrabalho)
            : base(unidadeDeTrabalho)
        {
        }
    }
}
