using System;
using System.Configuration;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace br.persistencia.tg.Infra
{
    public class SessionFactory
    {
        [ThreadStatic]
        private static SessionFactory _instancia;
        private readonly ISessionFactory _sessionFactory;
        private static string _assembliesFolder;
        private static readonly object SingletonLock = new object();

        // O SessionFactory é Singleton.
        public static SessionFactory Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (SingletonLock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new SessionFactory();
                        }
                    }
                }

                return _instancia;
            }
        }

        private SessionFactory()
        {
            _assembliesFolder = AppDomain.CurrentDomain.RelativeSearchPath;

            if (string.IsNullOrWhiteSpace(_assembliesFolder))
                _assembliesFolder = AppDomain.CurrentDomain.BaseDirectory;

            //SchemaMetadataUpdater.QuoteTableAndColumns(FluentlyConfig.BuildConfiguration());

            _sessionFactory = FluentlyConfig.BuildSessionFactory();

        }

        public ISession ObterSessao()
        {
            return _sessionFactory.OpenSession();
        }

        public IStatelessSession ObterSessaoSemEstado()
        {
            return _sessionFactory.OpenStatelessSession();
        }

        public FluentConfiguration FluentlyConfig
        {
            get
            {
                var fluentConfiguration = Fluently.Configure()
                    .Database(GetDatabaseConfigurations)
                    .Mappings(m =>
                              m.FluentMappings
                                  .AddFromAssembliesInPath(_assembliesFolder)
                                  .Conventions.Add<CustomTableNameConvention>()
                                  //.Conventions.Add<CustomPrimaryKeyConvention>()
                                  .Conventions.Add<CustomForeignKeyConvention>()
                                  .Conventions.Add<CustomForeignKeyConstraintOneToManyConvention>()
                                  .Conventions.Add<CustomJoinedSubclassConvention>()
                                  .Conventions.Add<CustomManyToManyTableNameConvention>()
                                  .Conventions.Add<StringColumnLengthConvention>()
                                  .Conventions.Add<StringPropertyConvention>()
                                  .Conventions.Add<ColumnNullabilityConvention>()
                                  .Conventions.Add(DefaultLazy.Always()))
                    .ExposeConfiguration(cfg => cfg.SetProperty("generate_statistics", "true"));
                //.SetProperty("hbm2ddl.keywords", "auto-quote"));

                var database = ConfigurationManager.AppSettings["Database"];

                if (database.Equals("Oracle"))
                    fluentConfiguration.Mappings(m => m.FluentMappings.Conventions.Add<OraclePrimaryKeySequenceConvention>());

                return fluentConfiguration;
            }
        }

        private static IPersistenceConfigurer GetDatabaseConfigurations()
        {
            var database = System.Configuration.ConfigurationManager.AppSettings["Database"];

            switch (database)
            {
                case "SQLite":
                    {
                        return SQLiteConfiguration
                                    .Standard
                                    .UsingFile(string.Format("{0}DbTests.db", _assembliesFolder))
                                    .ShowSql();
                    }
                case "SQLServer":
                    {
                        return MsSqlConfiguration
                                    .MsSql2008
                                    .ConnectionString(c => c.FromConnectionStringWithKey("SQLServerConn"))
                                    .ShowSql();
                    }
                case "Oracle":
                    {
                        return OracleDataClientConfiguration
                                    .Oracle10
                                    .ConnectionString(c => c.FromConnectionStringWithKey("OracleConn"))
                                    .ShowSql();
                    }
                case "MySQL":
                    {
                        return MySQLConfiguration
                                    .Standard
                                    .ConnectionString(c => c.FromConnectionStringWithKey("MySQLConn"))
                                    .ShowSql();
                    }
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Não há suporte para o banco de dados {0}!", database));
            }
        }



    }
}
