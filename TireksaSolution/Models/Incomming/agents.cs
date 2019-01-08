using Ocph.DAL;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace Incomming 
{ 
     [TableName("agents")] 
     public class agents :BaseNotify, Iagents  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 

                    SetProperty(ref _id, value);
                     }
          } 

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 

                    SetProperty(ref _name, value);
                     }
          } 

          [DbColumn("Address")] 
          public string Address 
          { 
               get{return _address;} 
               set{ 

                    SetProperty(ref _address, value);
                     }
          } 

          [DbColumn("ContactPerson")] 
          public string ContactPerson 
          { 
               get{return _contactperson;} 
               set{ 

                    SetProperty(ref _contactperson, value);
                     }
          } 

          [DbColumn("Telepon")] 
          public string Telepon 
          { 
               get{return _telepon;} 
               set{ 

                    SetProperty(ref _telepon, value);
                     }
          } 

          private int  _id;
           private string  _name;
           private string  _address;
           private string  _contactperson;
           private string  _telepon;
      }
}


