﻿using Bookie.Common;
using Bookie.Common.Interfaces;
using Bookie.Core;
using Bookie.Core.Interfaces;
using Bookie.Logging;
using Bookie.Repository;
using Bookie.Repository.Interfaces;
using Bookie.Repository.Repositories;
using Microsoft.Practices.Unity;

namespace Bookie.DependencyResolver
{
    public class Resolver
    {
        public static UnityContainer Container { get; set; }

        public static UnityContainer Bootstrap()
        {
            Container = new UnityContainer();

            Container.RegisterType<ISQLiteRepository, SQLiteRepository>();
            Container.RegisterType<IBookRepository, BookRepository>();
            Container.RegisterType<IBookCore, BookCore>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISettings, Settings>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ILog, Log>(new ContainerControlledLifetimeManager());
            return Container;
        }
    }
}