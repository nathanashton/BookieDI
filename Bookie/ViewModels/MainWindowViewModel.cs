using Bookie.Core.Interfaces;
using Bookie.Helpers;
using Bookie.UserControls.Authors;
using Bookie.UserControls.Books;
using Ookii.Dialogs.Wpf;
using PropertyChanged;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bookie.ViewModels
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        private readonly AuthorsControl _authorControl;
        private readonly BooksControl _bookControl;
        private readonly IBookCore _bookCore;
        private readonly IImporter _importer;
        private readonly ISupportedFormats _supportedFormats;

        public MainWindowViewModel(IBookCore bookCore, IImporter importer, BooksControl bookControl,
            AuthorsControl authorscontol, ISupportedFormats supportedFormats)
        {
            _authorControl = authorscontol;
            _supportedFormats = supportedFormats;
            _bookControl = bookControl;
            _bookCore = bookCore;
            _importer = importer;
            View = bookControl;
        }

        public ContentControl View { get; set; }

        public string Time { get; set; }

        public ICommand AddFilesCommand
        {
            get { return new RelayCommand(AddFiles, x => true); }
        }

        public ICommand AddFromFolderCommand
        {
            get { return new RelayCommand(AddFromFolder, x => true); }
        }

        public ICommand BooksCommand
        {
            get { return new RelayCommand(BookView, x => true); }
        }

        public ICommand AuthorsCommand
        {
            get { return new RelayCommand(AuthorView, x => true); }
        }

        private void BookView(object obj)
        {
            View = _bookControl;
        }

        private void AuthorView(object obj)
        {
            View = _authorControl;
        }

        private void AddFromFolder(object obj)
        {
            var dialog = new VistaFolderBrowserDialog
            {
                Description = "Select a Folder",
                UseDescriptionForTitle = true
            };
            if (dialog.ShowDialog() != true) return;
            var msg = MessageBox.Show("Scan Sub-Folders?", "Scan", MessageBoxButton.YesNo, MessageBoxImage.Question);
            _importer.AddFromFolder(dialog.SelectedPath, msg == MessageBoxResult.Yes);
        }

        private void AddFiles(object obj)
        {
            var supportedFormatsString = new StringBuilder();

            for (var index = 0; index < _supportedFormats.LoadedPlugins.Count; index++)
            {
                var file = _supportedFormats.LoadedPlugins[index];
                supportedFormatsString.Append($"*{file.Plugin.FileExtension}");
                if (index != _supportedFormats.LoadedPlugins.Count - 1)
                {
                    supportedFormatsString.Append(";");
                }
            }
            var filter = $"Supported Formats ({supportedFormatsString})|{supportedFormatsString}";
            var openFileDialog = new VistaOpenFileDialog { Filter = filter };
            if (openFileDialog.ShowDialog() != true) return;
            var files = openFileDialog.FileNames;
            _importer.AddBooks(files);
        }
    }
}