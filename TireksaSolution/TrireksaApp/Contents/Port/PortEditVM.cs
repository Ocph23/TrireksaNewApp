using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Port
{
    public class PortEditVM:ModelsShared.Models.port,IDataErrorInfo
    {

        public ObservableCollection<ModelsShared.Models.city> Cities { get; set; }
        public List<ModelsShared.Models.PortType> PortTypes { get; set; }

        public PortEditVM(port selectedItem)
        {
            var vm = ResourcesBase.GetMainWindowViewModel();
            Cities = vm.CityCollection.Source;
            PortTypes = new List<ModelsShared.Models.PortType>();
            PortTypes.Add(ModelsShared.Models.PortType.None);
            PortTypes.Add(ModelsShared.Models.PortType.Sea);
            PortTypes.Add(ModelsShared.Models.PortType.Air);
            PortTypes.Add(ModelsShared.Models.PortType.Land);

            this.CityID = selectedItem.CityID;
            this.CityName = selectedItem.CityName;
            this.Code = selectedItem.Code;
            this.Id = selectedItem.Id;
            this.Name = selectedItem.Name;
            this.PortType = selectedItem.PortType;


            
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    return string.IsNullOrEmpty(this.Name) ? "Name Required Value" : null;
                }

                if (columnName == "Code")
                {
                    return string.IsNullOrEmpty(this.Code) ? "Code Required Value" : null;
                }

                if (columnName == "CityID")
                {
                    return (CityID <= 0) ? "Select a City" : null;
                }

                if (columnName == "PortType")
                {
                    return (this.PortType == ModelsShared.Models.PortType.None) ? "Select a Type" : null;
                }

                return null;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
    }
}
