using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Common
{
    public class ErrorHandler
    {
        private Dictionary<string, string> collection { get; set; }
        public ErrorHandler()
        {
            collection = new Dictionary<string, string>();
        }



        public void AddError(string propertyName, string value)
        {
            if (!collection.ContainsKey(propertyName))
            {
                collection.Add(propertyName, value);

            }
            
        }


        public int ErrorCount
        {
            get
            {
                return collection.Count;
            }
        }

        public void DeleteError(string propertyName)
        {
            if (collection.ContainsKey(propertyName))
            {
                collection.Remove(propertyName);
            }

        }


    }
}
