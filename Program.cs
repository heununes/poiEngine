using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;


namespace poiEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            String url = "http://www.formacaosaude.com/categoria/formacao/feed/";
            Feed[] rssData = getRssFeeds(url);

            for (int i = 0; i < rssData.GetLength(0); i++)
            {
                Console.WriteLine(rssData[i]);
                Console.WriteLine("\n");
            }
        }

        private static Feed[] getRssFeeds(String Channel)
        {
            var tempRssData = new List<Feed>();
            XmlReader reader = XmlReader.Create(Channel);
            SyndicationFeed feedsObject = SyndicationFeed.Load(reader);
            reader.Close();
            Feed feed = new Feed();

            foreach (SyndicationItem item in feedsObject.Items)
            {
                int i = 0;
                feed.Title = item.Title.Text;
                feed.Link = item.Links[0].Uri.ToString();
                feed.Content = item.Summary.Text;
                String[] categories = new String[item.Categories.LongCount()];
                foreach (SyndicationCategory category in item.Categories)
                {
                    categories[i] = category.Name;
                    i++;
                }
                feed.Category = categories;
                tempRssData.Add(feed);
            }
            return tempRssData.ToArray<Feed>();
        }
    }
}
