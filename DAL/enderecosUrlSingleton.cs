using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poiEngine.DAL
{
    public class enderecosUrlSingleton
    {

        private static enderecosUrlSingleton instance;

        protected poiDatabaseTableAdapters.enderecosURLTableAdapter enderecos = new DAL.poiDatabaseTableAdapters.enderecosURLTableAdapter();

        private enderecosUrlSingleton() { }

        public static enderecosUrlSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new enderecosUrlSingleton();
                }
                return instance;
            }
        }

        public poiDatabase.enderecosURLDataTable getUrlsByType (string type)
        {
            return enderecos.GetDataByType(type);

        }

        public poiDatabase.enderecosURLDataTable getUrlsById(int id)
        {
            return enderecos.GetDataById(id);
        }

        public void storeUrlsArray(string link, int typeId)
        {
            enderecos.InsertEnderecoUrl(link, typeId);
        }
    }
}
