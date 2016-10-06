using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poiEngine.DAL
{
    class categorySingleton
    {
    
        private static categorySingleton instance;

        protected poiDatabaseTableAdapters.categoriesTableAdapter categories = new DAL.poiDatabaseTableAdapters.categoriesTableAdapter();

        private categorySingleton() { }

        public static categorySingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new categorySingleton();
                }
                return instance;
            }
        }

        public poiDatabase.categoriesDataTable getCategories()
        {
            return categories.GetAllCategories();
        }

        public string[] getCategoriesToArray(bool tolowerCase)
        {
            poiDatabase.categoriesDataTable categories = getCategories();
            string[] returnArray = new string[categories.Count];
            
            int i = 0;
            foreach (poiDatabase.categoriesRow item in categories)
            {
                if (tolowerCase)
                {
                    returnArray[i] = (string)item.description.Trim().ToLower();
                } else
                {
                    returnArray[i] = (string)item.description;
                }
                
                i++;
            }

            return returnArray;
        }

    }
}
