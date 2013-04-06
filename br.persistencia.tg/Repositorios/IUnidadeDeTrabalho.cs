using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace br.persistencia.tg.Repositorios
{
    public interface IUnidadeDeTrabalho : IDisposable
    {
        bool ASessaoEstaAberta { get; }
        bool ATransacaoEstaAtiva { get; }
        void Inicializar();
        void Salvar();
    }
}
