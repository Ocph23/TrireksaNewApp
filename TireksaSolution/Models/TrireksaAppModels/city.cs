using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace ModelsShared.Models 
{ 
     [TableName("city")] 
     public class city :BaseNotifyProperty
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

          [DbColumn("Province")] 
          public string Province 
          { 
               get{return _province;} 
               set{ 
                      _province=value; 
                     OnPropertyChange("Province");
                     }
          } 

          [DbColumn("Regency")] 
          public string Regency 
          { 
               get{return _regency;} 
               set{ 
                      _regency=value; 
                     OnPropertyChange("Regency");
                     }
          } 

          [DbColumn("CityName")] 
          public string CityName 
          { 
               get{return _cityname;} 
               set{ 
                      _cityname=value; 
                     OnPropertyChange("CityName");
                     }
          } 

          [DbColumn("CityCode")] 
          public string CityCode 
          { 
               get{return _citycode;} 
               set{ 
                      _citycode=value; 
                     OnPropertyChange("CityCode");
                     }
          } 

          private int  _id;
           private string  _province;
           private string  _regency;
           private string  _cityname;
           private string  _citycode;
      }
}


