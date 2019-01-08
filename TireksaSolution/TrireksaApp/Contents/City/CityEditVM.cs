using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.ComponentModel;

namespace TrireksaApp.Contents.City
{
    public class CityEditVM:ModelsShared.Models.city, IDataErrorInfo
    {

        public CityEditVM(city selectedItem)
        {
            this.Id = selectedItem.Id;
            this.CityCode = selectedItem.CityCode;
            this.CityName = selectedItem.CityName;
            this.Province = selectedItem.Province;
            this.Regency= selectedItem.Regency;
            
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Province")
                {
                    return string.IsNullOrEmpty(this.Province) ? "Province Required Value" : null;
                }
                if (columnName == "Regency")
                {
                    return string.IsNullOrEmpty(this.Regency) ? "Regency Required value" : null;
                }
                if (columnName == "CityName")
                {
                    return string.IsNullOrEmpty(this.CityName) ? "City Name Required value" : null;
                }
                if (columnName == "CityCode")
                {
                    return string.IsNullOrEmpty(this.CityCode) ? "City Code Required value" : null;
                }
                return null;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

       
    }
}
