using System;
using System.Collections.Generic;

namespace EvaluatingTool.DAL.Entities
{
    public class Test
    {
        public int TestId { get; set; }
        public Host Host { get; set; }
        public virtual List<Page> Pages { get; set; }
        public DateTime TestDate { get; set; }
    }
}
