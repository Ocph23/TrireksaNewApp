using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace TrireksaAppContext
{

    public interface ITrireksaAppHub
    {
        void OnAddAgent(object t);
    }


    public class TrireksaAppHub:Hub<ITrireksaAppHub>
    {
        private CustomersContext customerinstance;
        private CityContext cityInstance;
        private AgentsContext agentInstance;
        private PortsContext portInstance;

     

        public TrireksaAppHub()
        {
            customerinstance = CustomersContext.Instance;
            cityInstance = CityContext.Instance;
            agentInstance = AgentsContext.Instance;
            portInstance = PortsContext.Instance;
            
        }

       public void SaveAddAgent(object t)
        {
            Clients.Others.OnAddAgent(t);
        }


        public override Task OnConnected()
        {
            
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}