using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using br.dominio.tg.Entidades;
using br.dominio.tg.Repositorios;

namespace br.persistencia.tg.Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(IUnidadeDeTrabalho unidadeDeTrabalho) : base(unidadeDeTrabalho)
        {
        }
    }
}
