using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace Incomming 
{ 
     [TableName("shipinginformation")] 
     public class shipinginformation :BaseNotify, Ishipinginformation  
   {
          [DbColumn("ManifestId")] 
          public int ManifestId 
          { 
               get{return _manifestid;} 
               set{ 

                    SetProperty(ref _manifestid, value);
                     }
          } 

          [DbColumn("ArmadaName")] 
          public string ArmadaName 
          { 
               get{return _armadaname;} 
               set{ 

                    SetProperty(ref _armadaname, value);
                     }
          } 

          [DbColumn("ArmadaNumber")] 
          public string ArmadaNumber 
          { 
               get{return _armadanumber;} 
               set{ 

                    SetProperty(ref _armadanumber, value);
                     }
          } 

          [DbColumn("Package")] 
          public int Package 
          { 
               get{return _package;} 
               set{ 

                    SetProperty(ref _package, value);
                     }
          } 

          [DbColumn("OnOriginPort")] 
          public DateTime OnOriginPort 
          { 
               get{return _onoriginport;} 
               set{ 

                    SetProperty(ref _onoriginport, value);
                     }
          } 

          [DbColumn("OnDestinationPort")] 
          public DateTime OnDestinationPort 
          { 
               get{return _ondestinationport;} 
               set{ 

                    SetProperty(ref _ondestinationport, value);
                     }
          } 

          [DbColumn("OriginId")] 
          public int OriginId 
          { 
               get{return _originid;} 
               set{ 

                    SetProperty(ref _originid, value);
                     }
          } 

          [DbColumn("DestinationId")] 
          public int DestinationId 
          { 
               get{return _destinationid;} 
               set{ 

                    SetProperty(ref _destinationid, value);
                     }
          } 

          private int  _manifestid;
           private string  _armadaname;
           private string  _armadanumber;
           private int  _package;
           private DateTime  _onoriginport;
           private DateTime  _ondestinationport;
           private int  _originid;
           private int  _destinationid;
      }
}


