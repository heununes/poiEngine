﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace poiEngine
{
    class HtmlParser
    {
        // Field
        private string title;

        private string link;

        private string comments;

        private DateTime date;

        private string creator;

        private string[] category;

        private string descritpion;

        private string content;

        public HtmlParser()
        {
        }

        public HtmlParser(Feed feed)
        {
            title = feed.title;
            link = feed.link;
            descritpion = feed.descritpion;
            content = feed.content;
            category = feed.category;
            creator = feed.creator;
            date = feed.date;
            comments = feed.comments;
        }

        public HtmlParser(
            string title, 
            string link, 
            string description, 
            string content, 
            string creator, 
            string comments,
            DateTime date,
            String[] category
            )
        {
            this.title = title;
            this.link = link;
            descritpion = description;
            this.content = content;
            this.category = category;
            this.creator = creator;
            this.date = date;
            this.comments = comments;
        }

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

        public string Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
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

        public string Creator
        {
            get
            {
                return creator;
            }

            set
            {
                creator = value;
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
    }
}