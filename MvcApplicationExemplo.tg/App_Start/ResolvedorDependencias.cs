using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using br.infra.InjecaoDependencia;

namespace MvcApplicationExemplo.tg.App_Start
{
    public class ResolvedorDependencias : IDependencyResolver
    {
        private IKernel Kernel { get; set; }

        public ResolvedorDependencias()
        {
            Kernel = Fabrica.Instancia.Kernel;
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType, new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType, new IParameter[0]);
        }
    }
}