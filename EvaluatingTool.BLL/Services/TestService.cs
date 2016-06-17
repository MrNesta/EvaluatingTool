using EvaluatingTool.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluatingTool.BLL.DTO;
using EvaluatingTool.DAL.Interfaces;
using EvaluatingTool.DAL.Entities;
using EvaluatingTool.BLL.BusinessModels;
using EvaluatingTool.BLL.Clients;
using EvaluatingTool.BLL.Infrastructure;

namespace EvaluatingTool.BLL.Services
{
    public class TestService : ITestService
    {
        IUnitOfWork Database { get; set; }
        private List<Page> pages;
        private int percent;

        public TestService(IUnitOfWork db)
        {
            Database = db;
            pages = new List<Page>();
        }
        
        private int LastTestIndex
        {
            get 
            {
                return GetAllTests().Select(t => t.TestId).LastOrDefault();
            }
        }

        public async Task MakeTestAsync(string host)
        {            
            List<string> urlAddresses = new LinkUrl(host).GetURLAddresses();
            
            if (urlAddresses == null)
                throw new ValidationException("Don't have any URL", "");

            ProgressHub.SendMessage("determining sitemap", 5);

            List<Task> tasks = new List<Task>();

            percent = 500;
            int execPer = 8500 / urlAddresses.Count;
            foreach (var url in urlAddresses)
            {
                tasks.Add(MakeOnePageAsync(url, execPer));
            }
            await Task.WhenAll(tasks);

            ProgressHub.SendMessage("evaluating the time server response", 90);

            Test test = new Test()
            {
                Host =  Database.Hosts.GetAll()
                .Where(h => h.HostURL == host).FirstOrDefault() 
                ?? new Host() { HostURL = host},
                TestDate = DateTime.Now,
                Pages = pages
            };

            Database.Tests.Create(test);
            Database.Save();

            ProgressHub.SendMessage("saving test", 95);
        }

        async Task MakeOnePageAsync(string url, int executionPercent)
        {
            Page page = await new PageClient(url).MakePageAsync();
            pages.Add(page);
            percent += executionPercent;
            ProgressHub.SendMessage("", percent/100);
        }

        public IEnumerable<TestDTO> GetAllTests()
        {
            var testsDTO = from test in Database.Tests.GetAll().AsQueryable()
                           select new TestDTO()
                           {
                               TestId = test.TestId,
                               HostURL = test.Host.HostURL,
                               TestDate = test.TestDate
                           };

            return testsDTO;
        }

        public TestDTO GetTest(int? id)
        {
            id = id ?? LastTestIndex;

            Test test = Database.Tests.Get(id.Value);

            TestDTO testDTO = new TestDTO()
            {
                TestId = id.Value,
                HostURL = test.Host.HostURL,
                TestDate = test.TestDate
            };

            return testDTO;
        }

        public IEnumerable<PageDTO> GetTestPages(int? id)
        {

            id = id ?? LastTestIndex;
            Test test = Database.Tests.Get(id.Value);
            if (test == null)
                throw new ValidationException("Test not found", "");

            var pages = test.Pages.OrderByDescending(p => p.ResponseTime).AsQueryable().ToList();
            if (pages == null | pages.Count() == 0)
                throw new ValidationException("Pages not found", "");

            var allPages = Database.Pages.GetAll().OrderByDescending(p => p.ResponseTime).AsQueryable().ToList();
            var pagesDTO = from page in pages.AsQueryable()
                           join maxpage in allPages
                           .Distinct(new PageEqualityComparer()) 
                           on page.URL equals maxpage.URL
                           join minpage in allPages.OrderBy(p => p.ResponseTime)
                           .Distinct(new PageEqualityComparer()) 
                           on page.URL equals minpage.URL
                           select new PageDTO()
                           {
                               Id = page.Id,
                               URL = page.URL,
                               ResponseTime = page.ResponseTime,
                               MaxTime = maxpage.ResponseTime,
                               MinTime = minpage.ResponseTime
                           };
           
            return pagesDTO;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
