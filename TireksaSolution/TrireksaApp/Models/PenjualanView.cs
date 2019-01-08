using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Models
{
   public delegate void RefreshCollies();
    public class PenjualanView:ModelsShared.Models.penjualan,ICloneable
    {

        public event RefreshCollies OnChangeColly;

        public PenjualanView()
        {
            if(this.Details==null)
            {
                this.Details = new List<ModelsShared.Models.colly>();
            }
        }
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;
                //if(Details!=null && !value)
                //{
                //    Details.Clear();
                //}
                OnChangeColly?.Invoke();
                OnPropertyChange("IsSelected");
            }
        }
       
       

        private int _pcsSim;

        public int PcsSim
        {
            get
            {

                if (this.Details!= null || this.Details.Count > 0)
                {
                    _pcsSim = Details.Count;
                }

                return _pcsSim;
            }
            set { Pcs = value; OnPropertyChange("PcsSim"); }
        }


        private double _weightSim;

        public double WeightSim
        {
            get
            {

                if (this.Details!= null || this.Details.Count > 0)
                {
                    _weightSim = Details.Sum<ModelsShared.Models.colly>(O => O.Weight);
                }

                return _weightSim;
            }
            set { _weightSim = value; OnPropertyChange("WeightSim"); }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public PenjualanView Cloning()
        {
            return (PenjualanView)this.MemberwiseClone();
        }

    }
}
