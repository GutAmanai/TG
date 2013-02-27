using NHibernate;
using br.persistencia.tg.Infra;

namespace br.persistencia.tg.Repositorios
{
    class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        public ISession Sessao { get; private set; }
        public IStatelessSession SessaoSemEstado { get; private set; }

        public UnidadeDeTrabalho()
        {
            SessaoSemEstado = SessionFactory.Instancia.ObterSessaoSemEstado();

            this.Inicializar();
        }

        public bool ATransacaoEstaAtiva
        {
            get { return Sessao.Transaction.IsActive; }
        }

        public bool ASessaoEstaAberta
        {
            get { return Sessao.IsOpen; }
        }

        public bool ASessaoSemEstadoEstaAberta
        {
            get { return SessaoSemEstado.IsOpen; }
        }

        public void Inicializar()
        {
            if (Sessao == null || !ASessaoEstaAberta)
                Sessao = SessionFactory.Instancia.ObterSessao();

            if (SessaoSemEstado == null || !ASessaoSemEstadoEstaAberta)
                SessaoSemEstado = SessionFactory.Instancia.ObterSessaoSemEstado();

            this.Sessao.FlushMode = FlushMode.Commit;
            this.Sessao.BeginTransaction();

            this.SessaoSemEstado.BeginTransaction();
        }

        public void Salvar()
        {
            Sessao.Flush();
            Sessao.Transaction.Commit();

            SessaoSemEstado.Transaction.Commit();
        }

        public void Dispose()
        {
            if (this.Sessao.IsOpen)
                Sessao.Close();

            Sessao.Dispose();

            if (this.SessaoSemEstado.IsOpen)
                SessaoSemEstado.Close();

            SessaoSemEstado.Dispose();
        }
    }
}
