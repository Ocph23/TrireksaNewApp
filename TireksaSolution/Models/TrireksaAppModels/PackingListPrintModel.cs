using ModelsShared.Models;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    public class PackingListPrintModel:BaseNotifyProperty
    {
       
        [DbColumn("ManifestID")]
        public int ManifestID
        {
            get { return _manifestid; }
            set
            {
                _manifestid = value;
                OnPropertyChange("ManifestID");
            }
        }



        [DbColumn("PackNumber")]
        public int PackNumber
        {
            get { return _packnumber; }
            set
            {
                _packnumber = value;
                OnPropertyChange("PackNumber");
            }
        }

        [DbColumn("CollyNumber")]
        public int CollyNumber
        {
            get { return _collynumber; }
            set
            {
                _collynumber = value;
                OnPropertyChange("CollyNumber");
            }
        }

        [DbColumn("Code")]
        public int Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                OnPropertyChange("Code");
            }
        }
       


        private double _Wight;
        [DbColumn("Weight")]
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



        [DbColumn("STT")]
        public int STT
        {
            get { return _STT; }
            set
            {
                _STT = value;
                OnPropertyChange("STT");
            }
        }



        [DbColumn("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange("Name");
            }
        }
        
        [DbColumn("Phone")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChange("Phone");
            }
        }

        [DbColumn("Handphone")]
        public string Handphone
        {
            get { return _handphone; }
            set
            {
                _handphone = value;
                OnPropertyChange("Handphone");
            }
        }

        [DbColumn("Shiper")]
        public string Shiper
        {
            get { return _Shiper; }
            set
            {
                _Shiper = value;
                OnPropertyChange("Shiper");
            }
        }

        [DbColumn("Reciver")]
        public string Reciver
        {
            get { return _Reciver; }
            set
            {
                _Reciver = value;
                OnPropertyChange("Reciver");
            }
        }

        public string ManifestCode
        {
            get
            {
                if (this.Code > 0)
                    return ModelHelpers.GenerateManifestOutGoingCode(Code, this.CreatedDate);
                else
                    return string.Empty;
            }
        }

        public string STTCode
        {
            get
            {
                return string.Format("{0:D5}", STT);
            }
        }

        [DbColumn("CreatedDate")]
        public DateTime CreatedDate
        {
            get { return _createddate; }
            set
            {
                _createddate = value;
                OnPropertyChange("CreatedDate");
            }
        }


        private string _name;
        private string _phone;
        private string _handphone;
        private string _Reciver;
        private string _Shiper;
        private int _manifestid;
        private int _packnumber;
        private int _collynumber;
        private int _STT;
        private int _Code;
        private DateTime _createddate;
    }
}
