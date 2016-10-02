using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;
using HtmlAgilityPack;
using System.Net.Http;


namespace poiEngine.BLL
{
    class WebCrawler
    {
        private static string requestType = "search_engine";
        public WebCrawler() { }

        public string[] getDataFromUrl()
        {
            bool result = getUrlAddresses("html");
            var tempData = new List<string>();
            result = getUrlAddresses("rss");

            return tempData.ToArray<string>();
        }

        protected bool getUrlAddresses(string type)
        {
            DAL.enderecosUrlSingleton enderecos = DAL.enderecosUrlSingleton.Instance;
            DAL.poiDatabase.enderecosURLDataTable rows = enderecos.getUrlsByType(requestType);
            DAL.siteTypeSingleton siteType = DAL.siteTypeSingleton.Instance;
            int siteTypeId = (int)siteType.getIdByType(type);

            foreach (DAL.poiDatabase.enderecosURLRow row in rows)
            {
                foreach (string link in fetchDataFromWeb(row.url, type))
                {
                    enderecos.storeUrlsArray(link, siteTypeId);
                }
            }

            return true;
        }

        protected string[] fetchDataFromWeb (string pageUrl, string type)
        {

            DAL.searchKeyWordsSingleton searchKeyWords = DAL.searchKeyWordsSingleton.Instance;
            DAL.poiDatabase.searchKeywordsDataTable keyWodsRows = searchKeyWords.getSearchKeyWords();

            List<string> dataTemp = new List<string>();

            foreach (DAL.poiDatabase.searchKeywordsRow row in keyWodsRows)
            {
                string queryString = row.searchQuery.Replace(" ", "+");
                string query = "";

                switch (type)
                {
                    case "rss":
                        query = "?q=feed:" + queryString + "&go=Submit&qs=ds&form=QBLH";
                        break;
                    case "html":
                    default:
                        query = "?q=" + queryString + "&go=Submit&qs=ds&form=QBLH";
                        break;
                }

                string requestUrl = pageUrl + "?q=" + query + "&go=Submit&qs=ds&form=QBLH";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
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

                    foreach (string link in ParseHtmlContents(data))
                    {
                        dataTemp.Add(link);
                    }
                    response.Close();
                    readStream.Close();
                }
                
            }

            return dataTemp.ToArray();
        }

        public string[] ParseHtmlContents(string source)
        {
            List<string> hrefTags = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(source);
            try
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes(string.Format("//li[contains(@class,'{0}')]", "b_algo")))
                {
                    string link = node.FirstChild.FirstChild.GetAttributeValue("href", "");
                    if (link != "")
                    {
                        hrefTags.Add(link);
                    }
                }
            } catch (Exception e)
            {
                hrefTags = new List<string>();
            }

            return hrefTags.ToArray();
        }
        
    }
}
