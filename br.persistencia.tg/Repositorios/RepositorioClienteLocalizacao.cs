using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public class RepositorioClienteLocalizacao : RepositorioBase<ClienteLocalizacao>, IRepositorioClienteLocalizacao
    {
        public RepositorioClienteLocalizacao(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
