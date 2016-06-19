using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using Bookie.DependencyResolver;
using Bookie.UserControls.Authors;
using Bookie.UserControls.Books;
using Bookie.ViewModels;
using Bookie.Views;
using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Bookie
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = Resolver.Bootstrap();
            var settings = container.Resolve<ISettings>();

            container.RegisterType<MainWindow>();
            container.RegisterType<App>();

            container.RegisterType<MainWindowViewModel>();

            container.RegisterType<BooksControl>();
            container.RegisterType<BooksControlViewModel>();

            container.RegisterType<BookDetailsWindow>(new ContainerControlledLifetimeManager());
            container.RegisterType<BookDetailsWindowViewModel>(new ContainerControlledLifetimeManager());

            container.RegisterType<AuthorDetailsWindow>();
            container.RegisterType<AuthorDetailsWindowViewModel>();

            container.RegisterType<AuthorsControl>();
            container.RegisterType<AuthorsControlViewModel>();

            var plugins = container.Resolve<ISupportedFormats>();
            var log = container.Resolve<ILog>();

#if EMPTYDATABASE
            if (Directory.Exists(settings.ApplicationPath))
            {
                try
                {
                    Directory.Delete(settings.ApplicationPath, true);
                }
                catch (Exception)
                {
                    // Issues deleting log file
                }
            }
#endif

            RunApplication(log, plugins, settings, container);
        }

        private static void RunApplication(ILog log, ISupportedFormats plugins, ISettings settings,
            UnityContainer container)
        {
            TextOptions.TextFormattingModeProperty.OverrideMetadata(typeof(Window),
                new FrameworkPropertyMetadata(TextFormattingMode.Display,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.Inherits));
            log.Info("Application Started");
            plugins.LoadFromPath("Plugins", true);
            try
            {
                if (!Directory.Exists(settings.ApplicationPath))
                {
                    Directory.CreateDirectory(settings.ApplicationPath);
                }
                var mainWindow = container.Resolve<MainWindow>();
                // mainWindow.Closed += MainWindow_Closed;
                var app = new App();
                app.InitializeComponent();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                log.Error("Unhandled exception", ex);
            }
        }
    }
}