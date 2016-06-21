using EvaluatingTool.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace EvaluatingTool.BLL.BusinessModels
{
    public class LinkUrl
    {
        private string url;
        public LinkUrl(string hostUrl)
        {
            url = hostUrl;
        }

        public List<string> GetURLAddresses()
        {
            List<string> urlAddresses = new List<string>();
            string urlName = url.IndexOf('/', 9) != -1 ? url.Substring(0, url.IndexOf('/', 9)) : url;

            try
            {                
                using (HttpWebResponse resp = (HttpWebResponse)WebRequest.Create(url).GetResponse())
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());

                    string htmlPage = reader.ReadToEnd();
                    int curlock = 0; 

                    int i, j, start, end;
                    string href, urlAddress = null;
                    string lowcasePage = htmlPage.ToLower();
                    char[] invalid = { '!', '#', '"', '%', '&', '\'', '*',
                        ',', ';', '<', '=', '>', '?', '[', ']', '^',
                        '`', '{', '|', '}', ' ', '+' };

                    i = lowcasePage.IndexOf("<a", curlock);

                    while (i != -1)
                    {
                        j = lowcasePage.IndexOf("href=", i);

                        start = htmlPage.IndexOf('"', j) + 1;
                        end = htmlPage.IndexOf('"', start);
                        href = htmlPage.Substring(start, end - start);
                        curlock = end;

                        if (href.IndexOfAny(invalid) == -1)
                        {
                            if (href.StartsWith(urlName))
                            {
                                urlAddress = href;
                            }
                            else if (href.StartsWith("/"))
                            {
                                urlAddress = urlName + href;
                            }
                            else if (!href.StartsWith("http://") &
                                !href.StartsWith("https://"))
                            {
                                urlAddress = urlName + "/" + href;
                            }

                            if (urlAddress != null)
                            {
                                urlAddresses.Add(urlAddress);
                            }
                        }

                        i = lowcasePage.IndexOf("<a", curlock);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new ValidationException("Connection error from Url", "");
            }

            if (urlAddresses == null)
            {
                throw new ValidationException("Don't have any link URL", "");
            }
            return urlAddresses.Distinct().ToList();
        }
    }
}
