using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("userclaims")] 
     public class userclaims : BaseNotifyProperty
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

          [DbColumn("UserId")] 
          public string UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
                     }
          } 

          [DbColumn("ClaimType")] 
          public string ClaimType 
          { 
               get{return _claimtype;} 
               set{ 
                      _claimtype=value; 
                     OnPropertyChange("ClaimType");
                     }
          } 

          [DbColumn("ClaimValue")] 
          public string ClaimValue 
          { 
               get{return _claimvalue;} 
               set{ 
                      _claimvalue=value; 
                     OnPropertyChange("ClaimValue");
                     }
          } 

          private int  _id;
           private string  _userid;
           private string  _claimtype;
           private string  _claimvalue;
      }
}


