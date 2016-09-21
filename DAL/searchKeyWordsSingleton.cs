using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poiEngine.DAL
{
    class searchKeyWordsSingleton
    {
        private static searchKeyWordsSingleton instance;

        protected poiDatabaseTableAdapters.searchKeywordsTableAdapter searchKeyWords = new DAL.poiDatabaseTableAdapters.searchKeywordsTableAdapter();

        private searchKeyWordsSingleton() { }

        public static searchKeyWordsSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new searchKeyWordsSingleton();
                }
                return instance;
            }
        }

        public poiDatabase.searchKeywordsDataTable getSearchKeyWords()
        {
            return searchKeyWords.GetData();

        }
    }
}
