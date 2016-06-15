using poiEngine.BLL;
using poiEngine.BLL.Poi;
using System;

namespace poiEngine.DAL
{
    public class poiSingleton
    {

        private static poiSingleton instance;

        protected poiDatabaseTableAdapters.poiTableAdapter poi = new DAL.poiDatabaseTableAdapters.poiTableAdapter();

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

        public poiDatabase.poiDataTable getPoiById (int id)
        {
            return this.poi.GetDataById(id);

        }

        public int storePoi(String title, String comments, DateTime date, String url, String[] category, String descritpion, String content, String requestType, long urlId)
        {
            return this.poi.Insert(title, descritpion, url, date);
        }
    }
}
