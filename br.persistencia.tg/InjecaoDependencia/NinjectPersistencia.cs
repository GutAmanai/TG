﻿using Ninject.Modules;
using br.dominio.tg.Repositorios;
using br.persistencia.tg.Repositorios;

namespace br.persistencia.tg.InjecaoDependencia
{
    public class NinjectPersistencia : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnidadeDeTrabalho>().To<UnidadeDeTrabalho>();
            Bind<IRepositorioCliente>().To<RepositorioCliente>();
            Bind<IRepositorioUsuario>().To<RepositorioUsuario>();
            Bind<IRepositorioPromocao>().To<RepositorioPromocao>();
            Bind<IRepositorioClientePromocao>().To<RepositorioClientePromocao>();
            Bind<IRepositorioClienteLocalizacao>().To<RepositorioClienteLocalizacao>();
            Bind<IRepositorioPromocaoAcesso>().To<RepositorioPromocaoAcesso>();
        }
    }
}
