using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models 
{ 
     [TableName("packinglist")] 
     public class packinglist : BaseNotifyProperty
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

          [DbColumn("ManifestID")] 
          public int ManifestID 
          { 
               get{return _manifestid;} 
               set{ 
                      _manifestid=value; 
                     OnPropertyChange("ManifestID");
                     }
          } 

        
          public int STT 
          { 
               get{return _stt;} 
               set{ 
                      _stt=value; 
                     OnPropertyChange("STT");
                     }
          }

        [DbColumn("PenjualanId")]
        public int PenjualanId
        {
            get { return _penjualanId; }
            set
            {
                _penjualanId = value;
                OnPropertyChange("PenjualanId");
            }
        }

        [DbColumn("PackNumber")] 
          public int PackNumber 
          { 
               get{return _packnumber;} 
               set{ 
                      _packnumber=value; 
                     OnPropertyChange("PackNumber");
                     }
          } 

          [DbColumn("CollyNumber")] 
          public int CollyNumber 
          { 
               get{return _collynumber;} 
               set{ 
                      _collynumber=value; 
                     OnPropertyChange("CollyNumber");
                     }
          }

        [DbColumn("CollyId")]
        public int CollyId
        {
            get { return _collyId; }
            set
            {
                _collyId = value;
                OnPropertyChange("CollyId");
            }
        }


        private double _Wight;

        public double Weight
        {
            get
            {
                return _Wight;
            }
            set
            {
                _Wight = value;
                OnPropertyChange("Weight");
            }
        }

        private int  _id;
           private int  _manifestid;
           private int  _stt;
           private int  _packnumber;
           private int  _collynumber;
        private int _penjualanId;
        private int _collyId;
    }
}


