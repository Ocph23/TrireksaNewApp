using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Contents.Ship
{
   public  class ShipListVM:NotifyPropertyChanged
    {

        public MainWindowVM MainVM { get; private set; }
        public ShipListVM()
        {
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
        }

      
    }
}
