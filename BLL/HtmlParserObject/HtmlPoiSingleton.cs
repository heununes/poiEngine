using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Linq;
using AngleSharp;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;
using System.Text.RegularExpressions;

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

        private static string requestType = "html";

        private string[,] recognizedTags = new string[1, 2] { 
            { "table", "tr" }
        };

        public string[,] RecognizedTags
        {
            get
            {
                return RecognizedTags;
            }
        }


        //public HtmlParserObject[] getHtmlContent()
        public bool getHtmlContent()
        {
            DAL.enderecosUrlSingleton enderecos = DAL.enderecosUrlSingleton.Instance;
            DAL.poiDatabase.enderecosURLDataTable rows = enderecos.getUrlsByType(requestType);
            var tempHtmlData = new List<HtmlPoi>();

            foreach (DAL.poiDatabase.enderecosURLRow row in rows)
            {

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(row.url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.imt.pt/workshops_seminarios.php");
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

                    string body = ExtractHeaderContentFromHtml(data);

                    //bool result = this.ParseHtmlContents(body);
                    string content = ExtractHtmlContentFromHtml(body);

                    response.Close();
                    readStream.Close();
                }
            }

            // TODO store poi's in database with threads ??
            //Poi.ThreadPoi poi = new Poi.ThreadPoi(tempRssData.ToArray<Feed>());

            return true;
        }


        public bool ParseHtmlContents(string source)
        {
            List<string> keywords = new List<string>
            {
                "seminiarios",
                "seminários",
                "curso",
                "master"
            };
            //Create a (re-usable) parser front-end
            var parser = new HtmlParser();

            //Parse source to document
            var document = parser.Parse(source);
            //Do something with document like the following

            //Console.WriteLine("Serializing the (original) document:");
            //Console.WriteLine(document.DocumentElement.OuterHtml);
            int i = 0;
            foreach (var node in document.Body.Children)
            {
                Console.WriteLine("##################################### " + i + " ########################################################");
                Console.WriteLine(node.TagName);
                Console.WriteLine(node.NodeName);
                Console.WriteLine(node.OuterHtml);
                Console.WriteLine(node.TextContent);
                Console.WriteLine("####################################################################################################");

                i++;
            }

            //var p = document.CreateElement("p");
            //p.TextContent = "This is another paragraph.";

            //Console.WriteLine("Inserting another element in the body ...");
            //document.Body.AppendChild(p);

            //Console.WriteLine("Serializing the document again:");
            //Console.WriteLine(document.DocumentElement.OuterHtml);

            return true;
        }


        private string ExtractHeaderContentFromHtml(string input)
        {
            List<string> tagsToRemove = new List<string>
            {
                "doctype",
                "head",
                "script",
                "style"
            };

            HtmlParser hp = new HtmlParser();
            List<IElement> tags = new List<IElement>();
            List<string> nodeTypes = new List<string>();
            var hpResult = hp.Parse(input);

            try
            {
                foreach (var tagToRemove in tagsToRemove)
                    tags.AddRange(hpResult.QuerySelectorAll(tagToRemove));

                foreach (var tag in tags)
                    tag.Remove();
            }
            catch (Exception ex)
            {
                //_errors.Add(string.Format("Error in cleaning html. {0}", ex.Message));
                Console.WriteLine(string.Format("Error in cleaning html. {0}", ex.Message));
            }

            var content = hpResult.QuerySelector("body");

            return (content).InnerHtml;
        }

        private string ExtractHtmlContentFromHtml(string input)
        {
            List<string> keywords = new List<string>
            {
                "seminiarios",
                "seminários",
                "curso",
                "master"
            };


            HtmlParser hp = new HtmlParser();
            List<IElement> tags = new List<IElement>();
            List<string> nodeTypes = new List<string>();
            var hpResult = hp.Parse(input);

            List<string> textNodesValues = new List<string>();
            try
            {
                //foreach (string keyword in keywords)
                    tags.AddRange(hpResult.QuerySelectorAll("a"));

                foreach (var tag in tags)
                {
                    Console.WriteLine(tag.TagName);
                }
                    

                var treeWalker = hpResult.CreateTreeWalker(hpResult, FilterSettings.Text);

                var textNode = treeWalker.ToNext();
                while (textNode != null)
                {
                    textNodesValues.Add(textNode.TextContent);
                    textNode = treeWalker.ToNext();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error in cleaning html. {0}", ex.Message));
            }

            return string.Join(" ", textNodesValues);
        }
    }
}
