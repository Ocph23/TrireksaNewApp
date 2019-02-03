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
    public class CityCollection
    {
        public event RefreshComplete RefreshCompleted;
        private Client client = new Client("city");
        private SignalRClient signalRClient;

        public ObservableCollection<ModelsShared.Models.city> Source { get; set; }
        public CollectionView SourceView { get; set; }

        public ModelsShared.Models.city SelectedItem { get; set; }

        public CityCollection()
        {
            Source = new ObservableCollection<city>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            Refresh();

        }

        private async void InitAsync()
        {
            Source.Clear();
            var result = await client.GetAsync<List<city>>("Get");
            if (result != null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }
            signalRClient = ResourcesBase.GetSignalClient();
            signalRClient.OnAddCity += SignalRClient_OnAddCity;
            RefreshCompleted?.Invoke();
        }

        public Task Refresh()
        {
            InitAsync();
            return Task.FromResult(0);
        }

        private async void SignalRClient_OnAddCity(object sender)
        {
            await Task.Delay(2000);
            var result = JsonConvert.DeserializeObject<city>(sender.ToString());

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

        internal Task<city> Post(city city)
        {

          return client.PostAsync<city>("post",city);
            
        }

        internal async Task<bool> Update(int id, city city)
        {
            var res = await client.PutAsync<city>("put",id, city);
            if(res!=default(city))
            {
                var source = Source.Where(O => O.Id == city.Id).FirstOrDefault();
                if(source!=null)
                {
                    source.CityCode = res.CityCode;
                    source.CityName = res.CityName;
                    source.Province = res.Province;
                    source.Regency = res.Regency;
                    return true;
                }
            }

            return false;
        }

        internal async Task<bool> Delete(int id)
        {
            var item = Source.Where(O => O.Id == id).FirstOrDefault();
            var istrue= await client.Delete<bool>("Delete",id);
            if(istrue)
            {
                Source.Remove(item);
                SourceView.Refresh();
                return true;
            }

            return istrue;

        }
    }
}
