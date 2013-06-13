using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VnFeeds
{
    public static class Define
    {
        public const string FeedData_FormatName = "{0}-{1}.xml";// {0} MagazineName - {1} Category Name

        public const int SummaryFeedsNum = 9;

        public const string FeedContainerFolderName = "FeedContainer";


        public static async Task<string> DownloadStringAsync(Uri uri)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(uri);
        }

        public static async Task<Stream> DownloadStreamAsync(Uri uri)
        {
            HttpClient client = new HttpClient();
            return await client.GetStreamAsync(uri);

            //HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true, AllowAutoRedirect = true };
            //HttpClient client = new HttpClient(handler);

            //client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; ARM; Trident/6.0)");

            //HttpResponseMessage response = await client.GetAsync(uri);

            //response.EnsureSuccessStatusCode();

            //return await response.Content.ReadAsStreamAsync();
        }
    }
}
