using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace Incomming 
{ 
     [TableName("customers")] 
     public class customers :BaseNotify, Icustomers  
   {
          [PrimaryKey("CustomerId")] 
          [DbColumn("CustomerId")] 
          public int CustomerId 
          { 
               get{return _customerid;} 
               set{ 

                    SetProperty(ref _customerid, value);
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

          [DbColumn("Telepon")] 
          public string Telepon 
          { 
               get{return _telepon;} 
               set{ 

                    SetProperty(ref _telepon, value);
                     }
          } 

          [DbColumn("City")] 
          public string City 
          { 
               get{return _city;} 
               set{ 

                    SetProperty(ref _city, value);
                     }
          } 

          private int  _customerid;
           private string  _name;
           private string  _address;
           private string  _telepon;
           private string  _city;
      }
}


