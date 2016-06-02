using Bookie.Common.Entities;
using Bookie.Common.Exceptions;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Bookie.Core
{
    public class Importer : IImporter
    {
        public readonly BackgroundWorker Worker;
        private string[] _allFiles;
        private readonly IBookCore _bookCore;
        private readonly ILog _log;
        private readonly ISupportedFormats _supportedFormats;
        private readonly ISettings _settings;

        public Importer(IBookCore bookCore, ILog log, ISupportedFormats supportedFormats, ISettings settings)
        {
            _settings = settings;
            _supportedFormats = supportedFormats;
            _bookCore = bookCore;
            _log = log;
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
        }

        public void ProgressCancel()
        {
            if (Worker.IsBusy)
            {
                Worker.CancelAsync();
            }
        }

        public void StartScan(string path, bool includeSubDirectories, string searchPattern = "*.*")
        {
            _log.Info($"Scanning {path} for {searchPattern} - Include Subdirectories: {includeSubDirectories}");
            _allFiles = Directory.GetFiles(path, searchPattern, includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            _log.Info($"Found {_allFiles.Count()} files to import");
            Worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var book = e.UserState as Book;
            _bookCore.BooksChanged(new Common.EventArgs.BookEventArgs {Book = book, State = Common.EventArgs.BookEventArgs.BookState.Added});

            //ProgressArgs.OperationName = "Testing";
            //ProgressArgs.ProgressBarText = "Testing " + e.ProgressPercentage + "%";
            //ProgressArgs.ProgressPercentage = e.ProgressPercentage;
            //ProgressArgs.ProgressText = "Text";
            //OnProgressChange(ProgressArgs);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = 0; i < _allFiles.Count(); i++)
            {
                if (Worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                var bookfile = new BookFile { FullPathAndFileName = _allFiles[i] };
                var book = new Book { Title = Path.GetFileNameWithoutExtension(_allFiles[i]) };

                var pluginForBook = _supportedFormats.LoadedPlugins.FirstOrDefault(x => x.Plugin.FileExtension == bookfile.FileExtension);
                if (pluginForBook != null)
                {
                    try
                    {
                        var coverimage = new CoverImage();

                        var cover = pluginForBook.Plugin.ExtractCover(bookfile.FullPathAndFileName);
                        if (cover != null)
                        {
                            var coverFileName = Path.Combine(_settings.ImageCoversPath, Path.GetFileNameWithoutExtension(bookfile.FileName) + ".png");
                            cover.Save(coverFileName);
                            coverimage.FullPathAndFileName = coverFileName;
                        }
                        coverimage.AddBook(book);
                    }
                    catch (BookieException ex)
                    {
                        _log.Error("Error extracting cover image from file " + bookfile.FullPathAndFileName, ex);
                    }

                    try
                    {
                        //Supported format so process
                        var metadata = pluginForBook.Plugin.ExtractMetadata(bookfile.FullPathAndFileName);
                        book.Title = metadata.Title;
                        book.Isbn10 = metadata.Isbn;
                        book.Abstract = metadata.Abstract;
                        book.DatePublished = metadata.PublishedDate;
                        book.Pages = metadata.PageCount;
                        if (metadata.Authors != null && metadata.Authors.Count > 0)
                        {
                            foreach (var author in metadata.Authors)
                            {
                                book.AddAuthor(author);
                            }
                        }
                    }
                    catch (BookieException ex)
                    {
                        _log.Error("Error extracting metadata from file " + bookfile.FullPathAndFileName, ex);
                    }
                }
                else
                {
                    _log.Info($"{bookfile.FullPathAndFileName} : ({bookfile.FileExtension}) is not a supported format");
                    continue;
                }

                book.AddBookFile(bookfile);
                _bookCore.Persist(book);

                var percentage = Utils.CalculatePercentage(i, 1, _allFiles.Length);
                Worker.ReportProgress(percentage, book);
            }
        }
    }
}