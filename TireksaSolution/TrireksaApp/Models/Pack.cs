using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Models
{
   public class Pack:NotifyPropertyChanged
    {

        private int _packNumber;

        public int PackNumber
        {
            get { return _packNumber; }
            set { _packNumber = value;
                OnPropertyChanged("PackNumber");
            }
        }

        private int _tColly;

        public int TotalColly
        {
            get {

                if (PackingLists != null)
                    _tColly = PackingLists.Count;

                return _tColly; }
            set { _tColly = value;
                OnPropertyChanged("TotalColly");
            }
        }


        private double _tWight;

        public double TotalWeight
        {
            get {
                if (PackingLists != null)
                    _tWight = PackingLists.Sum(O => O.Weight);
                    return _tWight; }
            set { _tWight = value;
                OnPropertyChanged("TotalWeight");
            }
        }


        private List<ModelsShared.Models.colly> _packinglist;

        public List<ModelsShared.Models.colly> PackingLists
        {
            get { return _packinglist; }
            set { _packinglist = value; }
        }





    }
}
