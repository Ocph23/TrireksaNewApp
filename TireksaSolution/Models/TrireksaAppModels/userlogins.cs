using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("userlogins")] 
     public class userlogins : BaseNotifyProperty
    {
          [PrimaryKey("LoginProvider")] 
          [DbColumn("LoginProvider")] 
          public string LoginProvider 
          { 
               get{return _loginprovider;} 
               set{ 
                      _loginprovider=value; 
                     OnPropertyChange("LoginProvider");
                     }
          } 

          [PrimaryKey("ProviderKey")] 
          [DbColumn("ProviderKey")] 
          public string ProviderKey 
          { 
               get{return _providerkey;} 
               set{ 
                      _providerkey=value; 
                     OnPropertyChange("ProviderKey");
                     }
          } 

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

          private string  _loginprovider;
           private string  _providerkey;
           private string  _userid;
      }
}


