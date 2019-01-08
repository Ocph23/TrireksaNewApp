using FirstFloor.ModernUI.Presentation;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.CollectionsBase;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    public class SeaReferanceVM :ManifestInformation
    {
        private ship _ship;

        public SeaReferanceVM()
        {
            this.ShipsCollection = Common.ResourcesBase.GetMainWindowViewModel().ShipCollection;
        }
        public ShipCollection ShipsCollection  { get; private set; }

        public ModelsShared.Models.ship ShipSelectedItem {

            get
            {
                return _ship;
            }
            set
            {
                _ship = value;
                this.ArmadaName = value.Name;
                OnPropertyChange("ShipSelectedItem");
            }
        }
    }

   
}
