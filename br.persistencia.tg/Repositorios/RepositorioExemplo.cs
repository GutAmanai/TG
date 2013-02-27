using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

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
