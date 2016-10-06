using poiEngine.BLL;
using poiEngine.BLL.Poi;
using System;

namespace poiEngine.DAL
{
    public class poiSingleton
    {

        private static poiSingleton instance;

        protected poiDatabaseTableAdapters.poiTableAdapter poi = new DAL.poiDatabaseTableAdapters.poiTableAdapter();

        protected poiDatabaseTableAdapters.categoriesTableAdapter categoryAdapter = new DAL.poiDatabaseTableAdapters.categoriesTableAdapter();

        protected poiDatabaseTableAdapters.poiCategoriesTableAdapter poiCategoriesAdapter = new DAL.poiDatabaseTableAdapters.poiCategoriesTableAdapter();

        private poiSingleton() { }

        public static poiSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new poiSingleton();
                }
                return instance;
            }
        }

        public poiDatabase.poiDataTable getPoiById(int id)
        {
            return poi.GetDataById(id);

        }

        public poiDatabase.poiDataTable getPoiByTitle(string title)
        {
            try
            {
                poiDatabase.poiDataTable result = poi.GetPoiByTitle(title);
                return result;
            } catch (Exception ex)
            {
                return new poiDatabase.poiDataTable();
            }
        }

        public bool alreadyExists (string title)
        {
            poiDatabase.poiDataTable result = this.getPoiByTitle(title);

            if (result.Count > 0)
            {
                return true;
            }

            return false;
        }

        public int storePoi(string title, string comments, DateTime date, string url, string description, string content, string requestType, long urlId)
        {
            if (description == null && content != null)
            {
                description = content;
            }
            return poi.Insert(title, description, url, date);
        }

        public bool storeCategory(string[] categories, int poiId)
        {
            global::System.Nullable<long> categoryId = 0;
            global::System.Nullable<long> poiCategoryId = 0;
            bool result = false;

            foreach (var cat in categories)
            {
                try
                {
                    categoryId = (int)categoryAdapter.getCategoryIdByDescription(cat);
                }
                catch (InvalidOperationException e)
                {
                    categoryId = 0;
                }


                if (categoryId == 0)
                {
                    categoryId = categoryAdapter.Insert(cat);
                }

                if (categoryId > 0)
                {
                    try
                    {
                        poiCategoryId = (int)poiCategoriesAdapter.getPoiCategoriesByCategoryAndPoiIds((long)categoryId, (long)poiId);
                    }
                    catch (InvalidOperationException e)
                    {
                        poiCategoryId = 0;
                    }

                    if (poiCategoryId == 0)
                    {
                        poiCategoriesAdapter.Insert(categoryId, poiId);
                        result = true;
                    }
                }
                categoryId = 0;
                poiCategoryId = 0;
            }

            return result;
        }
    }
}
