using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace Incomming 
{ 
     [TableName("photos")] 
     public class photos :BaseNotify, Iphotos  
   {
          [DbColumn("DetailId")] 
          public int DetailId 
          { 
               get{return _detailid;} 
               set{ 

                    SetProperty(ref _detailid, value);
                     }
          } 

          [DbColumn("Location")] 
          public string Location 
          { 
               get{return _location;} 
               set{ 

                    SetProperty(ref _location, value);
                     }
          } 

          [DbColumn("FileName")] 
          public string FileName 
          { 
               get{return _filename;} 
               set{ 

                    SetProperty(ref _filename, value);
                     }
          } 

          [DbColumn("Ext")] 
          public string Ext 
          { 
               get{return _ext;} 
               set{ 

                    SetProperty(ref _ext, value);
                     }
          } 

          private int  _detailid;
           private string  _location;
           private string  _filename;
           private string  _ext;
      }
}


