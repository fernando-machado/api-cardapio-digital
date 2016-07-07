using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Reflection;

namespace CardapioDigital.Persistencia.InfraNH
{
    public class SessionFactory
    {
        private static readonly object _syncRoot = new object();
        private static SessionFactory _instancia;
        private readonly ISessionFactory _sessionFactory;

        // O SessionFactory é Singleton.
        public static SessionFactory Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_syncRoot)
                    {
                        _instancia = new SessionFactory();
                    }
                }
                return _instancia;
            }
        }

        private SessionFactory()
        {
            _sessionFactory = FluentlyConfig.BuildSessionFactory();
        }

        public ISession ObterSessao()
        {
            return _sessionFactory.OpenSession();
        }

        public static FluentConfiguration FluentlyConfig
        {
            get
            {
                return
                    Fluently
                        .Configure()
                        .Database(MsSqlConfiguration
                                      .MsSql2012
                                      .UseOuterJoin()
                                      .ConnectionString(c => c.FromConnectionStringWithKey("CardapioDigital"))
                                      .ShowSql()
                                      .FormatSql())
                        .Mappings(m => m.FluentMappings
                                        .Conventions.Add<CustomTableNameConvention>()
                                        .Conventions.Add<CustomPrimaryKeyConvention>()
                                        .Conventions.Add<CustomForeignKeyConvention>()
                                        //.Conventions.Add<CustomForeignKeyConstraintOneToManyConvention>()
                                        .Conventions.Add<CustomJoinedSubclassConvention>()
                                        .Conventions.Add<CustomManyToManyTableNameConvention>()
                                        .Conventions.Add<StringColumnLengthConvention>()
                                        .Conventions.Add<ColumnNullabilityConvention>()
                                        .AddFromAssembly(Assembly.GetExecutingAssembly())
                                        );
            }
        }
    }
}
