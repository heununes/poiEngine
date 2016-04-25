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
            String url = "http://www.formacaosaude.com/feed/";
            String[,] rssData = getRssFeeds(url);

            for (int i=0; i < rssData.GetLength(0); i++)
            {
                for(int j=0; j < 2; j++)
                {
                    Console.WriteLine(rssData[i, j]);
                    Console.WriteLine("\n");
                }
                
            }
        }

        private static String[,] getRssFeeds(String Channel)
        {
            String[,] tempRssData = new String[100, 3];
            XmlReader reader = XmlReader.Create(Channel);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;
            }

            return tempRssData;
        }
    }
}
