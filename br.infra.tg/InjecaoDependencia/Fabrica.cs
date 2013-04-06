using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.InjecaoDependencia;

namespace br.infra.tg.InjecaoDependencia
{
    public class Fabrica
    {
        private static Fabrica _instancia;
        public StandardKernel Kernel { get; set; }

        public static Fabrica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (typeof(Fabrica))
                    {
                        _instancia = new Fabrica();
                    }
                }

                return _instancia;
            }
        }

        private Fabrica()
        {
            var modulos = new NinjectModule[]
                          {
                              new NinjectPersistencia(),
                              new ServicoModule()
                          };

            Kernel = new StandardKernel(modulos);
        }

        public T Obter<T>()
        {
            return Kernel.Get<T>();
        }

        public T ObterRepositorio<T>(IUnidadeTrabalho unidadeTrabalho)
        {
            return Kernel.Get<T>(new ConstructorArgument("unidadeTrabalho", unidadeTrabalho));
        }
    }
}
