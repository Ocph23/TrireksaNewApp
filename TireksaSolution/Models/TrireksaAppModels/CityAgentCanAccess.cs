using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
   
    [TableName("citiesagentcanacccess")]
    public class CityAgentCanAccess:BaseNotifyProperty
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

        [DbColumn("CityId")]
        public int CityId
        {
            get { return _cityid; }
            set
            {
                _cityid = value;
                OnPropertyChange("CityId");
            }
        }

        private int _id;
        private int _agentid;
        private int _cityid;

    }

}
