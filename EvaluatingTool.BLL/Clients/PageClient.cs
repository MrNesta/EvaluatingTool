using EvaluatingTool.BLL.BusinessModels;
using EvaluatingTool.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace EvaluatingTool.BLL.Clients
{
    public class PageClient
    {
        private Func<Page> operation;        
        Page page;
        string url;
        TemporalData timeData;
        public PageClient(string urlAddress)
        {
            operation = new Func<Page>(MakePage);
            url = urlAddress;
            timeData = new TemporalData(urlAddress);
            page = new Page();
        }
        public Page MakePage()
        {
            int time = 0;
            Task task = timeData.GetResponseTimeDataAsync().
                ContinueWith(taskWithResponse =>
            {
                time = taskWithResponse.Result;
            });

            task.Wait();
            page.URL = url;
            page.ResponseTime = time;
            return page;
        }

        public async Task<Page> MakePageAsync()
        {
            return await Task<Page>.Factory.StartNew(MakePage);
        }

        public IAsyncResult BeginMakePage(AsyncCallback callback)
        {
            return operation.BeginInvoke(callback, null);
        }

        public Page EndMakePage(IAsyncResult asyncResult)
        {
            return operation.EndInvoke(asyncResult);
        }
    }
}
