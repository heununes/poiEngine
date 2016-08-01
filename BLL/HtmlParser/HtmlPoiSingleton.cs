using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Linq;
using AngleSharp;
using AngleSharp.Parser.Html;

namespace poiEngine.BLL
{
    class HtmlPoiSingleton
    {
        private static HtmlPoiSingleton instance;

        private HtmlPoiSingleton() { }

        public static HtmlPoiSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HtmlPoiSingleton();
                }
                return instance;
            }
        }

        private static String requestType = "html";

        //public HtmlParser[] getHtmlContent()
        public bool getHtmlContent()
        {
            DAL.enderecosUrlSingleton enderecos = DAL.enderecosUrlSingleton.Instance;
            DAL.poiDatabase.enderecosURLDataTable rows = enderecos.getUrlsByType(requestType);
            var tempHtmlData = new List<HtmlPoi>();

            foreach (DAL.poiDatabase.enderecosURLRow row in rows)
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(row.url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();
                    var parser = new HtmlParser();
                    var document = parser.Parse(data);

                    bool result = this.ParseHtmlContents(data);

                    response.Close();
                    readStream.Close();
                }

                //XmlReader reader = XmlReader.Create(row.url);
                //SyndicationFeed feedsObject = SyndicationFeed.Load(reader);
                //reader.Close();
                //Feed feed = new Feed();

                //Poi.Poi poi = new Poi.Poi();


                //foreach (SyndicationItem item in feedsObject.Items)
                //{
                //    int i = 0;
                //    feed.Title = item.Title.Text;
                //    feed.Link = item.Links[0].Uri.AbsoluteUri;
                //    feed.Content = item.Summary.Text;
                //    String[] categories = new String[item.Categories.LongCount()];
                //    foreach (SyndicationCategory category in item.Categories)
                //    {
                //        categories[i] = category.Name;
                //        i++;
                //    }
                //    feed.Category = categories;
                //    feed.Date = item.PublishDate.Date;

                //    Boolean result = poi.storePoi(requestType, feed, row.id_enderecosURL);

                //    if (result == true)
                //    {
                //        tempRssData.Add(feed);
                //    }
                //}
            }

            // TODO store poi's in database with threads ??
            //Poi.ThreadPoi poi = new Poi.ThreadPoi(tempRssData.ToArray<Feed>());

            return true;
        }
        

        public bool ParseHtmlContents(String source)
        {
            //Create a (re-usable) parser front-end
            var parser = new HtmlParser();
            
            //Parse source to document
            var document = parser.Parse(source);
            //Do something with document like the following

            Console.WriteLine("Serializing the (original) document:");
            Console.WriteLine(document.DocumentElement.OuterHtml);

            var p = document.CreateElement("p");
            p.TextContent = "This is another paragraph.";

            Console.WriteLine("Inserting another element in the body ...");
            document.Body.AppendChild(p);

            Console.WriteLine("Serializing the document again:");
            Console.WriteLine(document.DocumentElement.OuterHtml);

            return true;
        }
    }
}
