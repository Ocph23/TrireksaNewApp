using System;
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
    public class CustomerCollection
    {
        public ObservableCollection<customer> Source { get; set; }
        public CollectionView SourceView { get; set; }
        private Client client = new Client("customers");
        public customer SelectedItem { get; set; }
        private SignalRClient signalRClient { get; set; }

        public CustomerCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.customer>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            InitAsync();

        }

        private async void SignalRClient_OnUpdateCUstomer(object sender)
        {
            await Task.Delay(2000);
            var result = JsonConvert.DeserializeObject<customer>(sender.ToString());
          
            if(result!=null)
            {
                var data = Source.Where(O => O.Id == result.Id).FirstOrDefault();
                if(data==null)
                {
                    Source.Add(result);
                    SourceView.Refresh();
                }
                   
            }
        }

       
        private async void InitAsync()
        {
            var result = await client.GetAsync<List<customer>>("Get");
            if(result!=null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }

            signalRClient = ResourcesBase.GetSignalClient();
            signalRClient.OnAddCustomer+= SignalRClient_OnUpdateCUstomer;


        }

        public customer GetItemById(int Id)
        {
            return Source.Where(O => O.Id == Id).FirstOrDefault();
        }

        public async Task<bool> Add(customer item)
        {
            var res = await client.PostAsync<customer>("post",item);
            if (res != null)
            {
                this.Source.Add(res);
                SourceView.Refresh();
                return true;
            }
            else
                return false;
        }

        internal async Task<bool> RegisterCustomer(customer selectedItem)
        {
            var respon = await client.PostAsync<customer>("RegisterCustomer", selectedItem);
            return true;
        }

        internal async Task<bool> Update(int id, customer customer)
        {
            var newitem = await client.PutAsync<customer>("Put",id, customer);
            if (newitem!=default(customer))
            {
                var item = Source.Where(O => O.Id == id).FirstOrDefault();
                if (item != null)
                {
                    item.Address = newitem.Address;
                    item.ContactName = newitem.ContactName;
                    item.CustomerType = newitem.CustomerType;
                    item.Email = newitem.Email;
                    item.Handphone = newitem.Handphone;
                    item.Name = newitem.Name;
                    item.Phone1 = newitem.Phone1;
                    item.Phone2 = newitem.Phone2;
                    item.CityID = newitem.CityID;
                    return true;
                }
                SourceView.Refresh();
            }
            return false;
        }

        internal async Task<bool> Delete(int id)
        {
            bool result = await client.Delete<bool>("Delete",id);
            if (result)
            {
                Source.Remove(SelectedItem);
                SourceView.Refresh();
            }

            return result;
          
        }
    }
}
