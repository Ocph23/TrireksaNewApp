using ModelsShared;
using ModelsShared.Models;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
   public class titipankapal:BaseNotifyProperty
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

        [DbColumn("Code")]
        public int Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChange("Code");
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

        [DbColumn("agentName")]
        public string AgentName
        {
            get { return _agent; }
            set
            {
                _agent = value;
                OnPropertyChange("AgentName");
            }
        }

        [DbColumn("PackNumber")]
        public int PackNumber
        {
            get { return _pack; }
            set
            {
                _pack = value;
                OnPropertyChange("PackNumber");
            }
        }

        [DbColumn("OriginPort")]
        public string Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                OnPropertyChange("OriginPort");
            }
        }

        [DbColumn("DestinationPort")]
        public string Destination
        {
            get { return _destionation; }
            set
            {
                _destionation = value;
                OnPropertyChange("DestinationPort");
            }
        }

        [DbColumn("OriginCode")]
        public string OriginCode
        {
            get { return _originCode; }
            set
            {
                _originCode = value;
                OnPropertyChange("OriginCode");
            }
        }

        [DbColumn("DestinationCode")]
        public string DestinationCode
        {
            get { return _destionationCode; }
            set
            {
                _destionationCode = value;
                OnPropertyChange("DestinationCode");
            }
        }

        [DbColumn("AgentContactName")]
        public string AgentContactName
        {
            get { return _AgentContactName; }
            set
            {
                _AgentContactName = value;
                OnPropertyChange("AgentContactName");
            }
        }
        [DbColumn("agentPhone")]
        public string AgentPhone
        {
            get { return _agentPhone; }
            set
            {
                _agentPhone = value;
                OnPropertyChange("AgentPhone");
            }
        }

        [DbColumn("AgentHandphone")]
        public string AgentHandphone
        {
            get { return _AgentHandphone; }
            set
            {
                _AgentHandphone = value;
                OnPropertyChange("AgentHandphone");
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


        [DbColumn("CrewContact")]
        public string CrewContact
        {
            get { return _CrewContact; }
            set
            {
                _CrewContact = value;
                OnPropertyChange("CrewContact");
            }
        }

        [DbColumn("CrewAddress")]
        public string CrewAddress
        {
            get { return _CrewAddress; }
            set
            {
                _CrewAddress = value;
                OnPropertyChange("CrewAddress");
            }
        }

        [DbColumn("ReferenceNumber")]
        public string ReferenceNumber
        {
            get { return _ReferenceNumber; }
            set
            {
                _ReferenceNumber = value;
                OnPropertyChange("ReferenceNumber");
            }
        }

        public int Jumlah
        {
            get { return _jumlah; }
            set
            {
                _jumlah = value;
                OnPropertyChange("Jumlah");
            }
        }



        private int _id;
        private PortType _porttype;
        private string _armadaname;
        private string _crewname;
        private int _code;
        private string _agent;
        private int _pack;
        private string _origin;
        private string _destionation;
        private string _originCode;
        private string _destionationCode;
        private string _AgentContactName;
        private string _agentPhone;
        private string _AgentHandphone;
        private string _CrewContact;
        private string _CrewAddress;
        private string _ReferenceNumber;
        private int _jumlah;
    }
}
