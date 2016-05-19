using System;
using System.Collections.Generic;
using Core;
using DependencyResolver;
using Ninject;
using Ninject.Modules;

namespace UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();

            var modules = new List<INinjectModule>
            {
                new CoreLayer(),
                new DataLayer()
            };
            kernel.Load(modules);


            var k = kernel.Get<IBookCore>();
            var message = k.GetAllBooks();
            Console.WriteLine(message);


            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}