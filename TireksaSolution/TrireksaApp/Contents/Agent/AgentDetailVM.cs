using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Contents.Agent
{
    public class AgentDetailVM:ModelsShared.Models.agent
    {
        public AgentDetailVM(ModelsShared.Models.agent item)
        {
            this.Address = item.Address;
            this.ContactName = item.ContactName;
            this.Email = item.Email;
            this.Handphone = item.Handphone;
            this.Id = item.Id;
            this.Name = item.Name;
            this.Phone = item.Phone;
            this.CityID = item.CityID;
        }

    }
}
