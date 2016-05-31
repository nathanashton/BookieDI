using Bookie.Common.Entities;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using System.ComponentModel;
using System.IO;

namespace Bookie.Core
{
    public class Importer : IImporter
    {
        public readonly BackgroundWorker Worker;
        private string[] _allFiles;
        private readonly IBookCore _bookCore;
        private readonly ILog _log;

        public Importer(IBookCore bookCore, ILog log)
        {
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
            _allFiles = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            Worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ProgressArgs.OperationName = "Testing";
            //ProgressArgs.ProgressBarText = "Testing " + e.ProgressPercentage + "%";
            //ProgressArgs.ProgressPercentage = e.ProgressPercentage;
            //ProgressArgs.ProgressText = "Text";
            //OnProgressChange(ProgressArgs);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (var i = 1; i < _allFiles.Length; i++)
            {
                if (Worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                var bookfile = new BookFile { FullPathAndFileName = _allFiles[i] };

                var book = new Book { Title = Path.GetFileNameWithoutExtension(_allFiles[i]) };

                book.AddBookFile(bookfile);
                _bookCore.Persist(book);

                var percentage = Utils.CalculatePercentage(i, 1, _allFiles.Length);
                Worker.ReportProgress(percentage, null);
            }
        }
    }
}