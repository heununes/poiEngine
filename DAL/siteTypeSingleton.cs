using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poiEngine.DAL
{
    class siteTypeSingleton
    {
        private static siteTypeSingleton instance;

        protected poiDatabaseTableAdapters.siteTypeTableAdapter siteType = new DAL.poiDatabaseTableAdapters.siteTypeTableAdapter();

        private siteTypeSingleton() { }

        public static siteTypeSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new siteTypeSingleton();
                }
                return instance;
            }
        }

        public int getIdByType (string type)
        {
            return (int) siteType.getIdByType(type);
        }

    }
}
