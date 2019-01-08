using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace Incomming 
{ 
     [TableName("details")] 
     public class details :BaseNotify, Idetails  
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

          [DbColumn("PenjualanId")] 
          public string PenjualanId 
          { 
               get{return _penjualanid;} 
               set{ 

                    SetProperty(ref _penjualanid, value);
                     }
          } 

          [DbColumn("STT")] 
          public string STT 
          { 
               get{return _stt;} 
               set{ 

                    SetProperty(ref _stt, value);
                     }
          } 

          [DbColumn("Pcs")] 
          public string Pcs 
          { 
               get{return _pcs;} 
               set{ 

                    SetProperty(ref _pcs, value);
                     }
          } 

          [DbColumn("TypeOfWeight")] 
          public string TypeOfWeight 
          { 
               get{return _typeofweight;} 
               set{ 

                    SetProperty(ref _typeofweight, value);
                     }
          } 

          [DbColumn("Bobot")] 
          public string Bobot 
          { 
               get{return _bobot;} 
               set{ 

                    SetProperty(ref _bobot, value);
                     }
          } 

          [DbColumn("ShiperId")] 
          public int ShiperId 
          { 
               get{return _shiperid;} 
               set{ 

                    SetProperty(ref _shiperid, value);
                     }
          } 

          [DbColumn("ReciverId")] 
          public int ReciverId 
          { 
               get{return _reciverid;} 
               set{ 

                    SetProperty(ref _reciverid, value);
                     }
          } 

          [DbColumn("ManifestId")] 
          public int ManifestId 
          { 
               get{return _manifestid;} 
               set{ 

                    SetProperty(ref _manifestid, value);
                     }
          } 

          [DbColumn("Width")] 
          public double Width 
          { 
               get{return _width;} 
               set{ 

                    SetProperty(ref _width, value);
                     }
          } 

          [DbColumn("Height")] 
          public double Height 
          { 
               get{return _height;} 
               set{ 

                    SetProperty(ref _height, value);
                     }
          } 

          [DbColumn("Longl")] 
          public double Longl 
          { 
               get{return _longl;} 
               set{ 

                    SetProperty(ref _longl, value);
                     }
          } 

          private int  _id;
           private string  _penjualanid;
           private string  _stt;
           private string  _pcs;
           private string  _typeofweight;
           private string  _bobot;
           private int  _shiperid;
           private int  _reciverid;
           private int  _manifestid;
           private double  _width;
           private double  _height;
           private double  _longl;
      }
}


