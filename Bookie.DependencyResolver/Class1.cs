using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bookie.Core;
using Bookie.Core.Interfaces;
using Bookie.Repository;
using Bookie.Repository.Interfaces;

namespace Bookie.DependencyResolver
{
    public class Class1
    {
        public IContainer Go()
        {
                    var builder = new ContainerBuilder();
                    builder.RegisterType<BookRepository>().As<IBookRepository>();
                     builder.RegisterType<BookCore>().As<IBookCore>();
                     var container = builder.Build();
            return container;
        }

    }
}
