using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ocph.DAL;namespace ModelsShared.Models
{
    [TableName("manifestoutgoing")]
    public class manifestoutgoing : BaseNotifyProperty
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

        [DbColumn("AgentId")]
        public int AgentId
        {
            get { return _agentid; }
            set
            {
                _agentid = value;
                OnPropertyChange("AgentId");
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

        [DbColumn("ReferenceID")]
        public int ReferenceID
        {
            get { return _referenceid; }
            set
            {
                _referenceid = value;
                OnPropertyChange("ReferenceID");
            }
        }

        [DbColumn("Origin")]
        public int Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                OnPropertyChange("Origin");
            }
        }

        [DbColumn("Destination")]
        public int Destination
        {
            get { return _destionation; }
            set
            {
                _destionation = value;
                OnPropertyChange("Destination");
            }
        }

        [DbColumn("OnOriginPort")]
        public DateTime? OnOriginPort
        {
            get { return _onoriginport; }
            set
            {
                _onoriginport = value;
                OnPropertyChange("OnOriginPort");
            }
        }

        [DbColumn("OnDestinationPort")]
        public DateTime? OnDestinationPort
        {
            get { return _ondestinationport; }
            set
            {
                _ondestinationport = value;
                OnPropertyChange("OnDestinationPort");
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

        [DbColumn("UpdateDate")]
        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set
            {
                _updatedate = value;
                OnPropertyChange("UpdateDate");
            }
        }

        [DbColumn("UserId")]
        public int UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserId");
            }
        }


        public string ManifestCode
        {
            get
            {
                if (this.Code > 0)
                    return ModelHelpers.GenerateManifestOutGoingCode(this.Code, this.CreatedDate);
                else
                    return string.Empty;
            }
        }

        public ModelsShared.Models.ManifestInformation Information { get;  set; }
        public List<ModelsShared.Models.packinglist> PackingList { get; set; }
        public agent Agent {
            get
            {
                return _agent;
            }
            set
            {
                _agent = value;
                OnPropertyChange("Agent");
            }
        }
        public port OriginPort {
            get { return _originPort; }
            set { _originPort = value; OnPropertyChange("OriginPort"); }
        }
        public port DestinationPort {
            get { return _destionationPort; }
            set
            {
                _destionationPort = value;
                OnPropertyChange("DestionationPort");
            }
        }

        private int _id;
        private int _code;
        private PortType _porttype;
        private int _referenceid;
        private int _origin;
        private int _destionation;
        private DateTime? _onoriginport;
        private DateTime? _ondestinationport;
        private DateTime _createddate;
        private DateTime _updatedate;
        private int _userid;
        private int _agentid;
        private agent _agent;
        private port _originPort;
        private port _destionationPort;
    }

}
