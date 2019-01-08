using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ocph.DAL;

using System.Threading.Tasks;

namespace ModelsShared.Models 
{ 
     [TableName("collies")] 
     public class colly : BaseNotifyProperty,ICloneable
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

          [DbColumn("PenjualanId")] 
          public int PenjualanId
        { 
               get{return _penjualanId; } 
               set{ 
                      _penjualanId=value; 
                     OnPropertyChange("PenjualanId");
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

          [DbColumn("Weight")] 
          public virtual double Weight 
          { 
               get{

                if (this.TypeOfWeight == TypeOfWeight.Volume)
                {
                    _weight = (this.Long * this.Wide * this.Hight) / 1000000;
                    return _weight;
                }
                else if(this.TypeOfWeight == TypeOfWeight.WeightVolume)
                {
                    _weight = (this.Long * this.Wide * this.Hight) / 6000;
                    return _weight;
                }

                return _weight;


            } 
               set{ 
                      _weight=value; 
                     OnPropertyChange("Weight");
                     }
          } 

          [DbColumn("Long_")] 
          public virtual double Long 
          { 
               get{return _long;} 
               set{ 
                      _long=value; 
                     OnPropertyChange("Long");
                     }
          } 

          [DbColumn("Hight")] 
          public virtual double Hight 
          { 
               get{return _hight;} 
               set{ 
                      _hight=value; 
                     OnPropertyChange("Hight");
                     }
          } 

          [DbColumn("Wide")] 
          public virtual double Wide 
          { 
               get{return _wide;} 
               set{ 
                      _wide=value; 
                     OnPropertyChange("Wide");
                     }
          } 

          [DbColumn("TypeOfWeight")] 
          public virtual TypeOfWeight TypeOfWeight 
          { 
               get{return _typeofweight;} 
               set{ 
                      _typeofweight=value; 
                     OnPropertyChange("TypeOfWeight");
                     }
          } 

          [DbColumn("IsSended")] 
          public bool IsSended 
          { 
               get{return _issended;} 
               set{ 
                      _issended=value; 
                     OnPropertyChange("IsSended");
                     }
          }

     


        public Dimention Dimention
        {
            get
            {
                return new Dimention(this.Long, this.Wide, this.Hight);
            }
            set
            {
                this.Long = value.Long;
                this.Wide = value.Wide;
                this.Hight = value.Hight;
            }
        }

        public int STT { get; set; }

           private int  _collynumber;
           private double  _weight;
           private double  _long;
           private double  _hight;
           private double  _wide;
           private TypeOfWeight  _typeofweight;
           private bool _issended;
        private int _penjualanId;
        private int _id;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public colly GetClone()
        {
            return (colly)this.MemberwiseClone();
        }
    }
}


