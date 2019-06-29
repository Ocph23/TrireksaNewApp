using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp.Reports
{
   public class MySetting
    {

        private static string GetAddress()
        {
            var app = new AppConfiguration();
            return app.Address;
        }


        public string Address
        {
            get
            {
                return GetAddress();
            }
        }

    }
}
