using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluatingTool.DAL.Interfaces;
using EvaluatingTool.DAL.Entities;
using EvaluatingTool.DAL.EF;
using System.Data.Entity;

namespace EvaluatingTool.DAL.Repositories
{
    public class PageRepository : IRepository<Page>
    {
        private SiteMapContext db;

        public PageRepository(SiteMapContext context)
        {
            db = context;
        }

        public Page Get(int id)
        {
            return db.Pages.Find(id);
        }
        public IEnumerable<Page> GetAll()
        {
            return db.Pages;
        }

        public IEnumerable<Page> Find(Func<Page, bool> predicat)
        {
            return db.Pages.Where(predicat);
        }

        public void Create(Page item)
        {
            db.Pages.Add(item);
        }

        public void Update(Page item)
        {
            db.Entry(item).State = EntityState.Modified;
        }     

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Delete(int id)
        {
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                db.Entry(page).State = EntityState.Deleted;
            }
        }
    }
}
