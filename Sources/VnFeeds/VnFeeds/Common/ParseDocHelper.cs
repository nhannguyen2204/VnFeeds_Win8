using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VnFeeds.DataModel;

namespace VnFeeds.Common
{
    public static class ParseDocHelper
    {
        public static async Task<DataGroup> NewsGoVnGroup_Parse(string xmlString, DataModel.DataGroup group)
        {
            XDocument xdoc = XDocument.Load(xmlString);
            group.Title = xdoc.Element("title").Value;
            group.Subtitle = xdoc.Element("title").Value;
            group.Description = xdoc.Element("description").Value;

            var items = xdoc.Elements("item");
            foreach (var item in items)
            {
                DataItem dataItem = new DataItem();
                dataItem.Title = item.Element("title").Value;
                dataItem.Description = item.Element("description").Value;
                dataItem.Link = new Uri(item.Element("link").Value,UriKind.Absolute);
                dataItem.PubDate = item.Element("pubDate").Value;

                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(dataItem.Description);
                HtmlAgilityPack.HtmlNode imageLink = new HtmlAgilityPack.HtmlNode(HtmlAgilityPack.HtmlNodeType.Element,htmlDoc,3);

                dataItem.ImageUri = new Uri(imageLink.InnerText,UriKind.Absolute);

                //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                //string htmlString = await client.GetStringAsync(dataItem.Link);

                group.Items.Add(dataItem);
            }
            return group;
        }
    }
}
