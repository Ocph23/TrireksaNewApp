using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Common
{
    public static class Helper
    {
       private static string GetUrl<T> () where T : class
        {
            var type = typeof(T);
            switch(type.Name)
            {
                case "city":
                    return "api/City";
                case "customer":
                    return "api/Customers";
                case "penjualan":
                    return "api/Penjualans";
                case "port":
                    return "api/Ports";
                case "agent":
                    return "api/Agents";
                case "CityAgentCanAccess":
                    return "api/CitiesAgentCanAccess";
                case "manifestoutgoing":
                    return "api/manifestoutgoing";
                case "userprofile":
                    return "api/userprofile";
                case "ship":
                    return "api/ships";
                case "invoice":
                    return "api/Invoices";
                case "Prices":
                    return "api/Prices";
                default:
                    return null;
            }
          
        }

        public static string GetApiUrl<T>(string methode) where T:class
        {
            try
            {
                var x = Helper.GetUrl<T>() + string.Format("/{0}", methode);
                if(x==null)
                    throw new SystemException();
                return x;
            }
            catch (Exception )
            {

                throw new SystemException("Uri Not Found ... !");
            }
        
           
        }

        internal static string GetApiUrlWithId<T>(string methode,int id) where T:class
        {
            return GetUrl<T>() + string.Format("/{0}/{1}",methode, id);
        }
    }
}
