using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace Incomming 
{ 
     [TableName("manifestsincomming")] 
     public class manifestsincomming :BaseNotify, Imanifestsincomming  
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

          [DbColumn("ManifestNumber")] 
          public string ManifestNumber 
          { 
               get{return _manifestnumber;} 
               set{ 

                    SetProperty(ref _manifestnumber, value);
                     }
          } 

          [DbColumn("AgentId")]
        public int AgentId
        {
            get { return _agentId; }
            set
            {

                SetProperty(ref _agentId, value);
            }
        }

          [DbColumn("Via")] 
          public string Via 
          { 
               get{return _via;} 
               set{ 

                    SetProperty(ref _via, value);
                     }
          } 

          [DbColumn("CreateDate")] 
          public DateTime CreateDate 
          { 
               get{return _createdate;} 
               set{ 

                    SetProperty(ref _createdate, value);
                     }
          } 

          [DbColumn("UserId")] 
          public string UserId 
          { 
               get{return _userid;} 
               set{ 

                    SetProperty(ref _userid, value);
                     }
          } 

          [DbColumn("UpdateDate")] 
          public string UpdateDate 
          { 
               get{return _updatedate;} 
               set{ 

                    SetProperty(ref _updatedate, value);
                     }
          }


        private int  _id;
           private string  _manifestnumber;
           private string  _via;
           private DateTime  _createdate;
           private string  _userid;
           private string  _updatedate;
        private int _agentId;
    }
}


