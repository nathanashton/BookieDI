using Bookie.Common.Interfaces;
using Bookie.DependencyResolver;
using Bookie.ViewModels;
using Bookie.Views;
using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Windows;

namespace Bookie
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = Resolver.Bootstrap();
            container.RegisterType<MainWindow>();
            container.RegisterType<MainWindowViewModel>();
            var log = container.Resolve<ILog>();
            var settings = container.Resolve<ISettings>();
            log.Info("Application Started");
            RunApplication(container, log, settings);
        }

        private static void RunApplication(UnityContainer container, ILog log, ISettings settings)
        {
            try
            {
                if (!Directory.Exists(settings.ApplicationPath))
                {
                    Directory.CreateDirectory(settings.ApplicationPath);
                }
                var application = new App();
                var mainWindow = container.Resolve<MainWindow>();
                application.Run(mainWindow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                log.Error("Unhandled exception", ex);
            }
        }
    }
}