using Bookie.Common;
using Bookie.Common.Entities;
using Bookie.Common.EventArgs;
using Bookie.Common.Interfaces;
using Bookie.Core.Interfaces;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Bookie.Core.AuthorCore
{
    [ImplementPropertyChanged]
    public class AuthorCore : IAuthorCore
    {
        private readonly ILog _log;
        private readonly Ctx _ctx;

        public AuthorCore(ILog log, Ctx ctx)
        {
            _log = log;
            _ctx = ctx;
            _log.Debug(MethodName.Get());
        }

        public event EventHandler<AuthorEventArgs> AuthorChanged;

        public ObservableCollection<Author> GetAllAuthors()
        {
            _log.Debug(MethodName.Get());
            return new ObservableCollection<Author>(_ctx.Authors.ToList());
        }

        public int Persist(Author author)
        {
            if (_ctx.Entry(author).State == EntityState.Detached)
            {
                _ctx.Authors.Add(author);
            }

            _ctx.SaveChanges();
            return 0;
        }

        private void OnAuthorChanged(AuthorEventArgs e)
        {
            AuthorChanged?.Invoke(this, e);
        }
    }
}