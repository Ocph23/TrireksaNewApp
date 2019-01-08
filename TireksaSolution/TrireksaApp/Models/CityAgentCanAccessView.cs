using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using TrireksaApp.Common;
using Newtonsoft.Json;

namespace TrireksaApp.Models
{
    public class CityAgentCanAccessView:ModelsShared.Models.city
    {
        private MainWindowVM mainVM;
        public CityAgentCanAccessView()
        {
            mainVM = ResourcesBase.GetMainWindowViewModel();
        }

        public CityAgentCanAccessView(List<CityAgentCanAccess> citiesAgents)
        {
            this.citiesAgents = citiesAgents;
            mainVM = ResourcesBase.GetMainWindowViewModel();
        }

        private bool _isTrue;
        private List<CityAgentCanAccess> citiesAgents;

        public int AccessID { get; internal set; }
        public bool IsTrue
        {

            get { return _isTrue; }
            set
            {
                _isTrue = value;
                OnIsTrueChange(value);
                OnPropertyChange("IsTrue");
            }

        }


        private int _agentId;

        public int AgentID
        {
            get { return _agentId; }
            set { _agentId = value; }
        }

        private async void OnIsTrueChange(bool IsTrue)
        {
            var acc = citiesAgents.Where(O => O.CityId == this.Id).FirstOrDefault();
            var item = new ModelsShared.Models.CityAgentCanAccess { AgentId = this.AgentID, CityId = this.Id, Id = 0 };

            if (acc == null && IsTrue)
            {
               var data = await mainVM.AgentCollection.AgenCanAccess.OnChangeItemTrue(item);
                if(data!=default(CityAgentCanAccess))
                     citiesAgents.Add(data);

            }
            else if (!IsTrue && acc != null)
            {
                var data = await mainVM.AgentCollection.AgenCanAccess.OnChangeItemFalse(acc);
                if (data != default(CityAgentCanAccess))
                {
                    citiesAgents.Remove(item);
                }

            }
        }
        

    }
}
