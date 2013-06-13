using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using VnFeeds.DataModel;

namespace VnFeeds.Common
{
    public static class ParseDocHelper
    {
        public static async Task<DataGroup> NewsGoVnGroup_Parse(string xmlString, DataModel.DataGroup group, int takeNum)
        {
            StringReader _stringReader = new StringReader(xmlString);
            XDocument _xdoc = XDocument.Load(_stringReader);
            var channelElement = _xdoc.Element("rss").Element("channel");
            if (channelElement != null)
            {
                group.Title = channelElement.Element("title").Value;
                group.Subtitle = channelElement.Element("title").Value;
                group.Description = channelElement.Element("description").Value;

                var items = channelElement.Elements("item");
                foreach (var item in items)
                {
                    if (group.Items.Count == takeNum && takeNum >= 0) break;

                    DataItem dataItem = new DataItem();
                    dataItem.Title = item.Element("title").Value;
                    dataItem.Description = StripHTML(item.Element("description").Value);
                    dataItem.Link = new Uri(item.Element("link").Value, UriKind.Absolute);
                    dataItem.PubDate = item.Element("pubDate").Value;

                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.Load(new StringReader(item.Element("description").Value));

                    HtmlAgilityPack.HtmlNode imageLink = getFirstNode("img", htmlDoc.DocumentNode);
                    dataItem.ImageUri = new Uri(imageLink.GetAttributeValue("src", string.Empty).Replace("96.62.jpg", "480.310.jpg"), UriKind.Absolute);

                    dataItem.Group = group;
                    group.Items.Add(dataItem);
                }
            }

            return group;
        }

        public static string StripHTML(string HTMLText)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(HTMLText, "");
        }


        private static HtmlAgilityPack.HtmlNode getFirstNode(string name, HtmlAgilityPack.HtmlNode parent)
        {
            while (parent.ChildNodes.Count > 0)
            {
                foreach (var item in parent.ChildNodes)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                    if (item.HasChildNodes)
                    {
                        var result = getFirstNode(name, item);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
            return null;
        }
    }
}
