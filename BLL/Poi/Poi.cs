using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poiEngine.BLL.Poi
{
    class Poi
    {
        // Field
        private string title;

        private string comments;

        private DateTime date;

        private string url;

        private string[] category;

        private string descritpion;

        private string content;

        private string type;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Comments
        {
            get
            {
                return comments;
            }

            set
            {
                comments = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string[] Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string Descritpion
        {
            get
            {
                return descritpion;
            }

            set
            {
                descritpion = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public Poi()
        {
        }

        public Poi(Poi poi)
        {
            this.title = poi.title;
            this.comments = poi.comments;
            this.date = poi.date;
            this.url = poi.url;
            this.category = poi.category;
            this.descritpion = poi.descritpion;
            this.content = poi.content;
        }

        public Poi(
            string title,
            string comments,
            DateTime date,
            string url,
            String[] category,
            string description,
            string content
            )
        {
            this.title = title;
            this.comments = comments;
            this.date = date;
            this.url = url;
            this.category = category;
            this.descritpion = description;
            this.content = content;
        }

        public Poi(Feed feed)
        {
            this.title = feed.Title;
            this.comments = feed.Comments;
            this.date = feed.Date;
            this.url = feed.Link;
            this.category = feed.Category;
            this.descritpion = feed.Descritpion;
            this.content = feed.Content;
        }

        public void setPoi(Feed feed)
        {
            this.title = feed.Title;
            this.comments = feed.Comments;
            this.date = feed.Date;
            this.url = feed.Link;
            this.category = feed.Category;
            this.descritpion = feed.Descritpion;
            this.content = feed.Content;
        }

        public bool storePoi (String requestType, Feed feed, long urlId)
        {
            this.setPoi(feed);

            DAL.poiSingleton pois = DAL.poiSingleton.Instance;
            int poiId = pois.storePoi(this.Title, this.Comments, this.Date, this.Url, this.Category, this.Descritpion, this.Content, requestType, urlId);

            // DAL.poiDatabaseTableAdapters.poiTableAdapter


            return false;
        }
    }
}


