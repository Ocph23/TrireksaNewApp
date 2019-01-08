using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;
namespace ModelsShared.Models 
{ 
     [TableName("userrole")] 
     public class userrole : BaseNotifyProperty
    {
          [PrimaryKey("RoleId")] 
          [DbColumn("RoleId")] 
          public int RoleId 
          { 
               get{return _roleid;} 
               set{ 
                      _roleid=value; 
                     OnPropertyChange("RoleId");
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

          private int  _roleid;
           private string  _userid;
      }
}


