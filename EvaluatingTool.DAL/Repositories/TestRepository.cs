using EvaluatingTool.DAL.EF;
using EvaluatingTool.DAL.Entities;
using EvaluatingTool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvaluatingTool.DAL.Repositories
{
    public class TestRepository : IRepository<Test>
    {
        private SiteMapContext db;

        public TestRepository(SiteMapContext context)
        {
            db = context;
        }
        public void Create(Test item)
        {
            db.Tests.Add(item);
        }

        public void Delete(int id)
        {
            Test test = db.Tests.Find(id);
            if (test != null)
            {
                db.Entry(test).State = EntityState.Deleted;
            }
        }

        public IEnumerable<Test> Find(Func<Test, bool> predicate)
        {
            return db.Tests.Include(t => t.Host).Include(t => t.Pages).Where(predicate);
        }

        public Test Get(int id)
        {
            return db.Tests.Include(t => t.Host).Where(t => t.TestId == id).FirstOrDefault();
        }

        public IEnumerable<Test> GetAll()
        {
            return db.Tests.Include(t => t.Host).Include(t => t.Pages);
        }

        public void Update(Test item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
