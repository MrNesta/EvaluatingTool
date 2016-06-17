using EvaluatingTool.DAL.Interfaces;
using System;
using EvaluatingTool.DAL.Entities;
using EvaluatingTool.DAL.EF;

namespace EvaluatingTool.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SiteMapContext db;

        private HostRepository hostRepository;
        private PageRepository pageRepository;
        private TestRepository testRepository;

        public EFUnitOfWork(string connection)
        {
            db = new SiteMapContext(connection);
        }
        public IRepository<Host> Hosts
        {
            get
            {
                if (hostRepository == null)
                {
                    hostRepository = new HostRepository(db);
                }
                return hostRepository;
            }
        }

        public IRepository<Page> Pages
        {
            get
            {
                if (pageRepository == null)
                {
                    pageRepository = new PageRepository(db);
                }
                return pageRepository;
            }
        }

        public IRepository<Test> Tests
        {
            get
            {
                if (testRepository == null)
                {
                    testRepository = new TestRepository(db);
                }
                return testRepository;
            }
        }

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
