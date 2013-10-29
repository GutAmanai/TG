using System;

namespace br.dominio.tg.Repositorios
{
    public interface IUnidadeTrabalho : IDisposable
    {
        void Begin();
        void Commit();
    }
}
