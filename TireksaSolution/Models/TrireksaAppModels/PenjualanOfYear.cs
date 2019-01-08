using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    public class PenjualanOfYear
    {
        private int _tahun;
        [PrimaryKey("tahun")]
        [DbColumn("tahun")]
        public int Tahun
        {
            get { return _tahun; }
            set { _tahun = value; }
        }

        private double _weight;
        [DbColumn("Weight")]
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private double _total;
        [DbColumn("Total")]
        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }


        private double _cod;
        [DbColumn("COD")]
        public double COD
        {
            get { return _cod; }
            set { _cod = value; }
        }

        private double _credit;
        [DbColumn("Credit")]
        public double Credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

        private double _chash;
        [DbColumn("Chash")]
        public double Chash
        {
            get { return _chash; }
            set { _chash = value; }
        }




        public List<PenjualanOfMonth> Months { get; set; }


    }
}
