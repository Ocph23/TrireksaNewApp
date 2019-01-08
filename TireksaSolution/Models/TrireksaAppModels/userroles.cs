using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("userroles")] 
     public class userroles : BaseNotifyProperty
    {
          [PrimaryKey("UserId")] 
          [DbColumn("UserId")] 
          public string UserId 
          { 
               get{return _userid;} 
               set{ 
                      _userid=value; 
                     OnPropertyChange("UserId");
                     }
          } 

          [PrimaryKey("RoleId")] 
          [DbColumn("RoleId")] 
          public string RoleId 
          { 
               get{return _roleid;} 
               set{ 
                      _roleid=value; 
                     OnPropertyChange("RoleId");
                     }
          } 

          private string  _userid;
           private string  _roleid;
      }
}


