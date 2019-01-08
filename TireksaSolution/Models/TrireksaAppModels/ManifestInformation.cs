using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{

    [TableName("manifestinformation")]
    public class ManifestInformation:BaseNotifyProperty
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("ManifestType")]
        public ManifestType ManifestType
        {
            get { return _manifesttype; }
            set
            {
                _manifesttype = value;
                OnPropertyChange("ManifestType");
            }
        }

        [DbColumn("ManifestId")]
        public int ManifestId
        {
            get { return _manifestid; }
            set
            {
                _manifestid = value;
                OnPropertyChange("ManifestId");
            }
        }

        [DbColumn("PortType")]
        public PortType PortType
        {
            get { return _porttype; }
            set
            {
                _porttype = value;
                OnPropertyChange("PortType");
            }
        }

        [DbColumn("ArmadaName")]
        public string ArmadaName
        {
            get { return _armadaname; }
            set
            {
                _armadaname = value;
                OnPropertyChange("ArmadaName");
            }
        }

        [DbColumn("CrewName")]
        public string CrewName
        {
            get { return _crewname; }
            set
            {
                _crewname = value;
                OnPropertyChange("CrewName");
            }
        }

        [DbColumn("Contact")]
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                OnPropertyChange("Contact");
            }
        }

        [DbColumn("Address")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChange("Address");
            }
        }

        [DbColumn("ReferenceNumber")]
        public string ReferenceNumber
        {
            get { return _referencenumber; }
            set
            {
                _referencenumber = value;
                OnPropertyChange("ReferenceNumber");
            }
        }

        private int _id;
        private ManifestType _manifesttype;
        private int _manifestid;
        private PortType _porttype;
        private string _armadaname;
        private string _crewname;
        private string _contact;
        private string _address;
        private string _referencenumber;
    }

}
