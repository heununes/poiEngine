﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Threading;

namespace poiEngine.BLL
{
    class FeedsSingleton
    {
        private static FeedsSingleton instance;

        private FeedsSingleton() { }

        public static FeedsSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedsSingleton();
                }
                return instance;
            }
        }

        private static string requestType = "rss";

        public Feed[] getRssFeeds()
        {
            DAL.enderecosUrlSingleton enderecos = DAL.enderecosUrlSingleton.Instance;
            DAL.poiDatabase.enderecosURLDataTable rows = enderecos.getUrlsByType(requestType);
            var tempRssData = new List<Feed>();

            foreach (DAL.poiDatabase.enderecosURLRow row in rows)
            {
                
                XmlReader reader = XmlReader.Create(row.url);
                SyndicationFeed feedsObject = SyndicationFeed.Load(reader);
                reader.Close();
                Feed feed = new Feed();

                Poi.Poi poi = new Poi.Poi();


                foreach (SyndicationItem item in feedsObject.Items)
                {
                    int i = 0;
                    feed.Title = item.Title.Text;
                    feed.Link = item.Links[0].Uri.AbsoluteUri;
                    feed.Content = item.Summary.Text;
                    string[] categories = new string[item.Categories.LongCount()];
                    foreach (SyndicationCategory category in item.Categories)
                    {
                        categories[i] = category.Name;
                        i++;
                    }
                    feed.Category = categories;
                    feed.Date = item.PublishDate.Date;

                    Boolean result = poi.storePoi(requestType, feed, row.id_enderecosURL);

                    if (result == true)
                    {
                        tempRssData.Add(feed);
                    }
                }
            }

            // TODO store poi's in database with threads ??
            //Poi.ThreadPoi poi = new Poi.ThreadPoi(tempRssData.ToArray<Feed>());

            return tempRssData.ToArray<Feed>();
        }
    }
}
