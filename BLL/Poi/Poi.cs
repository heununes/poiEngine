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

//        private string[] category;

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

 /*       public string[] Category
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
        */

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
            title = poi.title;
            comments = poi.comments;
            date = poi.date;
            url = poi.url;
            //this.category = poi.category;
            descritpion = poi.descritpion;
            content = poi.content;
        }

        public Poi(
            string title,
            string comments,
            DateTime date,
            string url,
            string[] category,
            string description,
            string content
            )
        {
            this.title = title;
            this.comments = comments;
            this.date = date;
            this.url = url;
            //this.category = category;
            descritpion = description;
            this.content = content;
        }

        public Poi(Feed feed)
        {
            title = feed.Title;
            comments = feed.Comments;
            date = feed.Date;
            url = feed.Link;
            //this.category = feed.Category;
            descritpion = feed.Descritpion;
            content = feed.Content;
        }

        public void setPoi(Feed feed)
        {
            title = feed.Title;
            comments = feed.Comments;
            date = feed.Date;
            url = feed.Link;
            //this.category = this.setCategories(feed.Category);
            descritpion = feed.Descritpion;
            content = feed.Content;
        }

        public bool storePoi (string requestType, Feed feed, long urlId)
        {
            setPoi(feed);

            DAL.poiSingleton pois = DAL.poiSingleton.Instance;
            int poiId = pois.storePoi(Title, Comments, Date, Url, Descritpion, Content, requestType, urlId);
            
            if (poiId == 0)
            {
                return false;
            }

            bool result = pois.storeCategory(feed.Category, poiId);
            
            return true;
        }
    }
}


