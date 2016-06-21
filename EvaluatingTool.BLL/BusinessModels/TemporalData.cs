using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace EvaluatingTool.BLL.BusinessModels
{
    public class TemporalData
    {
        string urlAddress;
        public TemporalData(string url)
        {
            urlAddress = url;
        }

        public async Task<int> GetResponseTimeDataAsync()
        {
            int time = 0;
            if (urlAddress != null)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.
                        Create(urlAddress);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36";

                Stopwatch timer = Stopwatch.StartNew();
                try
                {
                    using (WebResponse response = await request.GetResponseAsync())
                    {
                        timer.Stop();
                        time = timer.Elapsed.Milliseconds;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return time;
        }
    }
}
