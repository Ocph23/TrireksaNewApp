using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using Newtonsoft.Json;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class PortCollection
    {
        public ObservableCollection<ModelsShared.Models.port> Source { get; set; }
        public CollectionView SourceView { get; set; }
        private Client client = new Client("Ports");
        private SignalRClient signalRClient;

        public ModelsShared.Models.port SelectedItem { get; set; }
        public CollectionView SourceView1 { get; private set; }

        public PortCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.port>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            CompleteTask();
        }

        public async void CompleteTask()
        {
            var result = await client.GetAsync<List<port>>("Get");
            if(result!=default(List<port>))
            {
                foreach (var item in result)
                {
                    Source.Add(item);
               
                }

                SourceView.Refresh();
            }
           

            signalRClient = ResourcesBase.GetSignalClient();
            signalRClient.OnAddPort += SignalRClient_OnAddPort;

        }

        private async void SignalRClient_OnAddPort(object sender)
        {
            await Task.Delay(3000);
            var result = JsonConvert.DeserializeObject<port>(sender.ToString());
            if (result != null)
            {
                var data = Source.Where(O => O.Id == result.Id).FirstOrDefault();
                if (data == null)
                {
                    Source.Add(result);
                    SourceView.Refresh();
                }
            }
        }

        internal async Task<port> Add(port port)
        {
            var result = await client.PostAsync<port>("Post", port);
            if(result!=default(port))
            {
                Source.Add(result);
                SourceView.Refresh();
            }
            return result;
         
        }

        internal async Task<port> Update(int id, port port)
        {
            var data = Source.Where(O => O.Id == id).FirstOrDefault();
            if (data != null)
            {
                var item = await client.PutAsync<port>("Put", id, port);
                if (item != default(port))
                {
                    data.CityID = item.CityID;
                    data.CityName = item.CityName;
                    data.Code = item.Code;
                    data.Name = item.Name;
                    data.PortType = item.PortType;
                }
                return item;
            }
            else
                return null;
         
        }

        internal async Task<port> Delete(int id)
        {
            var data = Source.Where(O => O.Id == id).FirstOrDefault();
            if (data != null)
            {
                var result = await client.Delete<port>("Delete", id);
                if (result!=null)
                {
                    Source.Remove(data);
                    SourceView.Refresh();
                }
                return result;
            }

            return default(port);
               
        }
    }
}
