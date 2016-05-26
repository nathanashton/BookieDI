using Bookie.Common.Interfaces;
using Bookie.Repository.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.PropertyChanged;
using NHibernate.Tool.hbm2ddl;

namespace Bookie.Repository
{
    public class Database : IDatabase
    {
        private readonly ISettings _settings;


        public Database(ISettings settings)
        {
            _settings = settings;
            SessionFactory = CreateSessionFactory();
        }

        public ISessionFactory SessionFactory { get; set; }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(@"data source=" + _settings.DatabasePath + ";"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BookMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }


        public void BuildSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(false, true);
        }
    }
}