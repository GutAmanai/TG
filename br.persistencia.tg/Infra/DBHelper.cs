using System;
using System.Configuration;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace br.persistencia.tg.Infra
{
    public static class DbHelper
    {
        public static void CriarBanco()
        {
            var config = SessionFactory.Instancia.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));

            config.BuildSessionFactory();
        }

        public static void AtualizarBanco()
        {
            var config = SessionFactory.Instancia.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));

            config.BuildSessionFactory();
        }

        public static void ExcluirBanco()
        {
            var config = SessionFactory.Instancia.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Drop(true, true));

            config.BuildSessionFactory();
        }

        public static void GerarScriptDeCriacaoDoSchema(string path = null)
        {
            var config = SessionFactory.Instancia.FluentlyConfig;

            var conn = SessionFactory.Instancia.ObterSessao().Connection;
            var writer = new StreamWriter(string.Format(@"{0}Create_Schema_{1}.sql"
                                                        , path ?? ConfigurationManager.AppSettings["AssembliesFolder"]
                                                        , DateTime.Now.ToString("yyyyMMdd_HHhmmss")));

            config.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, false, false, conn, writer));

            config.BuildSessionFactory();
        }

        public static void GerarScriptDeAtualizacaoDoSchema(string path = null)
        {
            var config = SessionFactory.Instancia.FluentlyConfig;

            config.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(script =>
            {
                using (var file = new FileStream(string.Format(@"{0}Update_Schema_{1}.sql", path ?? ConfigurationManager.AppSettings["AssembliesFolder"], DateTime.Now.ToString("yyyyMMdd_HHhmmss")), FileMode.Create, FileAccess.ReadWrite))
                using (var writer = new StreamWriter(file))
                {
                    writer.Write(script);
                    writer.Close();
                }
            }
            , false));

            config.BuildSessionFactory();
        }
    }
}
