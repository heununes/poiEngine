using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

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
            DAL.poiSingleton pois = DAL.poiSingleton.Instance;

            DAL.enderecosUrlSingleton enderecos = DAL.enderecosUrlSingleton.Instance;
            DAL.poiDatabase.enderecosURLDataTable rows = enderecos.getUrlsByType(requestType);

            DAL.categorySingleton categoriesSingleton = DAL.categorySingleton.Instance;
            DAL.poiDatabase.categoriesDataTable categoriesRows = categoriesSingleton.getCategories();

            string[] categoriesArray = categoriesSingleton.getCategoriesToArray(true);



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
                    string title = "";
                    try
                    {
                        title = item.Title.Text;
                    }
                    catch (NullReferenceException ex)
                    {
                        title = "";
                    }
                    if (this.verifyEligibilaty(categoriesArray, title.Trim().ToLower()))
                    {
                        int i = 0;
                        // verificar se o titulo contém alguma key word
                        feed.Title = title;
                        if (item.Links[0].Uri.IsAbsoluteUri)
                        {
                            feed.Link = item.Links[0].Uri.AbsoluteUri;
                        }
                        
                        feed.Content = item.Summary.Text;
                        if (item.Categories.LongCount() > 0)
                        {
                            string[] categories = new string[item.Categories.LongCount()];
                            foreach (SyndicationCategory category in item.Categories)
                            {
                                categories[i] = category.Name;
                                i++;
                            }
                            feed.Category = categories;
                        }
                        else
                        {
                            string[] categories = new string[1];
                            categories[0] = "Generic";
                            feed.Category = categories;
                        }

                        //feed.Date = item.PublishDate.Date;
                        Boolean result = false;
                        if (pois.alreadyExists(title) == false)
                        {
                            result = poi.storePoi(requestType, feed, row.id_enderecosURL);
                        }

                        if (result == true)
                        {
                            tempRssData.Add(feed);
                        }
                    }
                }
            }

            // TODO store poi's in database with threads ??
            //Poi.ThreadPoi poi = new Poi.ThreadPoi(tempRssData.ToArray<Feed>());

            return tempRssData.ToArray<Feed>();
        }

        protected bool verifyEligibilaty(string[] categories, string value)
        {
            bool result = false;
            foreach (string category in categories)
            {
                if (value.Contains(category))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
