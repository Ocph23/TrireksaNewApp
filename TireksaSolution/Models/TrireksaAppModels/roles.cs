using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("roles")] 
     public class roles : BaseNotifyProperty, Iroles
    {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public string Id 
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

          private string  _id;
           private string  _name;
      }
}


