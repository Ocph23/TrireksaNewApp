using Microsoft.AspNet.SignalR.Client;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp
{


    public delegate void OnReciveData(object sender);


    public class SignalRClient
    {
        private HubConnection connection;
        private IHubProxy hubProxy;

        public event OnReciveData OnAddCustomer;
        public event OnReciveData OnAddCity;
        public event OnReciveData OnAddAgent;
        public event OnReciveData OnAddPort;



        public async void ConnectAsync()
        {
            string BaseUri = ConfigurationManager.AppSettings.Get("host");
            connection = new HubConnection(BaseUri);
            hubProxy = connection.CreateHubProxy("TrireksaAppHub");
          //  hubProxy.On<dynamic>("broadcastCustomer", (u) => OnUpdateCUstomer?.Invoke(u));
            hubProxy.On<dynamic>("onAddCustomer", (u) => OnAddCustomer?.Invoke(u));
            hubProxy.On<dynamic>("onAddCity", (u) => OnAddCity?.Invoke(u));
            hubProxy.On<dynamic>("onAddAgent", (u) => OnAddAgent?.Invoke(u));
            hubProxy.On<dynamic>("onAddPort", (u) => OnAddPort?.Invoke(u));

            connection.Reconnecting += Reconnecting;
            connection.Reconnected += Reconnected;
            connection.Closed += Disconnected;

            ServicePointManager.DefaultConnectionLimit = 10;
            try
            {
                await connection.Start();
            }
            catch (Exception ex)
            {
                if (ResourcesBase.HomeVM != null)
                    ResourcesBase.HomeVM.BarMessage = ex.Message;
                else

                    throw new SystemException(ex.Message);
            }
         
        }

        private void Disconnected()
        {
            Reconnecting();
        }

        private void Reconnected()
        {
            ConnectAsync();
        }

        private void Reconnecting()
        {
            ConnectAsync();
        }
    }
}
