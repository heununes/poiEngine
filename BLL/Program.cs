using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace poiEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            BLL.FeedsSingleton feeds = BLL.FeedsSingleton.Instance;
            Feed[] rssData = feeds.getRssFeeds();

            

            //for (int i = 0; i < rssData.GetLength(0); i++)
            //{
            //    Console.WriteLine(rssData[i].Date);
            //    Console.WriteLine(rssData[i].Title);
            //    Console.WriteLine(rssData[i].Descritpion);
            //    Console.WriteLine(rssData[i].Content);
            //    Console.WriteLine(rssData[i].Link);

            //    Console.WriteLine("################################################");
            //}
        }
    }
}
