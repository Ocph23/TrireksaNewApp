using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{


    public delegate void RefreshComplete();

    public  class BaseCollection: NotifyPropertyChanged
    {

        public BaseCollection()
        {
            var now = DateTime.Now;
            StartDate = new DateTime(now.Year, now.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);
        }


        private DateTime start;

        public DateTime StartDate
        {
            get { return start; }
            set {  start = value;
                OnPropertyChanged("StartDate");
            }
        }


        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value;
                OnPropertyChanged("EndDate");
            }
        }




    }
}
