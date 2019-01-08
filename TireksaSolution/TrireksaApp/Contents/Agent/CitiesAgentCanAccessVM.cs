using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Agent
{
    public class CitiesAgentCanAccessVM
    {
        private agent selectedItem;
        public string AgentName { get; set; }
        public List<Models.CityAgentCanAccessView> CitiesView { get; private set; }
        public Action CloseWindow { get;  set; }
        public CommandHandler CloseWindowCommand { get; private set; }

        public CitiesAgentCanAccessVM(agent selectedItem)
        {
            CloseWindowCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = CloseWindowAction };
            this.selectedItem = selectedItem;
            this.AgentName = selectedItem.Name;
           
           if(selectedItem.CitiesCanAccess==null)
                selectedItem.CitiesCanAccess= new List<CityAgentCanAccess>();
            this.CitiesView = new List<Models.CityAgentCanAccessView>();
            GetCitiesAgentCanAccess();
        }

        private void CloseWindowAction(object obj)
        {
            CloseWindow();
        }

        private void GetCitiesAgentCanAccess()
        {
            var cities = Common.ResourcesBase.GetMainWindowViewModel().CityCollection.Source;
            foreach(var item in cities)
            {
                CitiesView.Add(new Models.CityAgentCanAccessView(selectedItem.CitiesCanAccess) {AgentID=selectedItem.Id, CityCode = item.CityCode, CityName = item.CityName, Province = item.Province,
                    Regency = item.Regency, Id = item.Id, AccessID = 0 });
            }


            foreach(var item in selectedItem.CitiesCanAccess)
            {
               var a = CitiesView.Where(O => O.Id == item.CityId).FirstOrDefault();
                if (a != null)
                    a.IsTrue = true;
            }

          
        }
    }
}
