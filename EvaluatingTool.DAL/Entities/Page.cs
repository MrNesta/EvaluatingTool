using System.Collections.Generic;

namespace EvaluatingTool.DAL.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int ResponseTime { get; set; }

    }

    public class PageEqualityComparer: IEqualityComparer<Page>
    {

        public bool Equals(Page x, Page y)
        {
            if (x.URL == y.URL)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Page obj)
        {
            string page = "Page";
            return obj.URL.GetHashCode() * page.GetHashCode();
        }
    }
}
