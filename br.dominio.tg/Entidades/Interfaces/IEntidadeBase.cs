using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.dominio.tg.Entidades.Interfaces
{
    public interface IEntidadeBase
    {
        int Id { get; }
        bool Equals(object obj);
        int GetHashCode();
    }
}
