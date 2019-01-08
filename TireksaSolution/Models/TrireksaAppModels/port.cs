using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("ports")] 
     public class port : BaseNotifyProperty, IModelValidate
    {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value; 
                     OnPropertyChange("Name");
                     }
          } 

          [DbColumn("PortType")] 
          public PortType PortType 
          { 
               get{return _porttype;} 
               set{ 
                      _porttype=value; 
                     OnPropertyChange("PortType");
                     }
          }


        [DbColumn("Code")]
        public string Code
        {
            get { return _code; }
            set
            {
                _code= value;
                OnPropertyChange("Code");
            }
        }

        [DbColumn("CityID")]
        public int CityID
        {
            get { return _cityId; }
            set
            {
                _cityId= value;
                OnPropertyChange("CityID");
            }
        }


        // Tambahan

            public string CityName {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChange("CityName");
            }
        }

        public bool IsValid
        {
            get
            {
                return ValidatedAction();
            }
        }

        public bool ValidatedAction()
        {
            bool result = true;
            if(CityID<=0 || string.IsNullOrEmpty(this.Code) || 
                string.IsNullOrEmpty(this.Name) || this.PortType==PortType.None)
            {
                result = false;

            }

            return result;

        }

        private int  _id;
           private string  _name;
           private PortType  _porttype;
        private string _code;
        private int _cityId;
        private string _cityName;

       
    }
}


