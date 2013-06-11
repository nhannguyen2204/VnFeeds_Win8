using System;
using System.Collections.Generic;
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
    }
}
