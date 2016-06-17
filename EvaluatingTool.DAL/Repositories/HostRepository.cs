﻿using EvaluatingTool.DAL.EF;
using EvaluatingTool.DAL.Entities;
using EvaluatingTool.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EvaluatingTool.DAL.Repositories
{
    public class HostRepository : IRepository<Host>
    {
        private SiteMapContext db;

        public HostRepository(SiteMapContext context)
        {
            db = context;
        }
        public void Create(Host item)
        {
            db.Hosts.Add(item);
        }

        public void Delete(int id)
        {
            Host host = db.Hosts.Find(id);
            if (host != null)
            {
                db.Entry(host).State = EntityState.Deleted;
            }
        }

        public IEnumerable<Host> Find(Func<Host, bool> predicate)
        {
            return db.Hosts.Where(predicate);
        }

        public Host Get(int id)
        {
            return db.Hosts.Find(id);
        }

        public IEnumerable<Host> GetAll()
        {
            return db.Hosts;
        }

        public void Update(Host item)
        {
            db.Entry(item).State = EntityState.Modified; 
        }
    }
}
