﻿using System.Data.Entity;
using EvaluatingTool.DAL.Entities;

namespace EvaluatingTool.DAL.EF
{
    public class SiteMapContext: DbContext
    {
        public SiteMapContext(string conectionString)
            : base(conectionString)
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
