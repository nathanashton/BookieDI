using NHibernate;
using NHibernate.Cfg;

namespace Bookie.Repository
{
    public interface IDatabase
    {
        ISessionFactory SessionFactory { get; set; }

        ISessionFactory CreateSessionFactory();

        void BuildSchema(Configuration config);
    }
}