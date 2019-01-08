using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared;
using ModelsShared.Models;
using ModelsShared.ReportModels;
using TrireksaApp.Common;
using TrireksaApp.Models;

namespace TrireksaApp.CollectionsBase
{
   
    public class PenjualanCollection:BaseCollection
    {

        public event RefreshComplete RefreshCompleted;
        private Client client = new Client("Penjualans");

        public ObservableCollection<penjualan> Source { get; set; }

        public PenjualanCollection()
        {
            Source = new ObservableCollection<penjualan>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        internal async Task<List<PenjualanReportModel>> GetPenjualanFromTo(DateTime start, DateTime end)
        {
            var url = string.Format("GetPenjualanFromTo?start={0}-{1}-{2}&ended={3}-{4}-{5}",start.Year,start.Month,start.Day,
                end.Year,end.Month,end.Day);
            var result = await client.GetAsync<List<PenjualanReportModel>>(url);
            return result;
        }

        private async void InitAsync()
        {
            Source.Clear();
            var url = string.Format("Get?startDate={0}-{1}-{2}&endDate={3}-{4}-{5}", StartDate.Year, StartDate.Month, StartDate.Day,
              EndDate.Year, EndDate.Month, EndDate.Day);
            var result = await client.GetAsync<List<penjualan>>(url);
            if (result != null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    await Task.Delay(10);
                    
                }
            }
            SourceView.Refresh();
            RefreshCompleted?.Invoke();
        }

        public Task Refresh()
        {
            InitAsync();
            return Task.FromResult(0);
        }


        public async Task<penjualan> GetItemBySTT(int id)
        {
            var result = this.Source.Where(O => O.STT == id).FirstOrDefault();
            if (result == null)
            {
                result = await client.GetAsync<penjualan>("Get", id);
                if (result != null)
                {

                    this.Source.Add(result);
                
                }
            }
            SourceView.Refresh();
            return result;
        }

        public async Task<penjualan> GetItemById(int id)
        {
            var result = this.Source.Where(O => O.Id == id).FirstOrDefault();
            if (result == null)
            {
                result = await client.GetAsync<penjualan>("GetById", id);
                if (result != null)
                {

                    this.Source.Add(result);

                }
            }
            SourceView.Refresh();
            return result;
        }


        public async Task<penjualan> Add(penjualan item)
        {
            var result = await client.PostAsync<penjualan>("Post", item);
            if(result!=null)
            {
                Source.Add(result);
                SourceView.Refresh();
            }
            return result;
        }

        public CollectionView SourceView { get; set; }

        internal async Task<colly> RemoveItem(colly selectedItem)
        {
            using (var client = new Client("Collies"))
            {
                return await client.Delete<colly>("Delete", selectedItem.Id);
            }
        }

        public penjualan SelectedItem { get; set; }

        internal Task<List<penjualan>> GetPenjualanNotPaid(int id)
        {
           return client.GetAsync<List<penjualan>>("GetPenjualanNotPaid", id);
        }

        internal async Task<bool> DeletePhoto(Photo selectedPhoto)
        {
            var clientPhoto = new Client("Photos");
            var res = await clientPhoto.Delete<bool>("DeletePhoto", selectedPhoto.Id);
            return res;
        }

        internal Task<List<penjualan>> GetByParameter(manifestoutgoing manifestoutgoing)
        {
            var uri = "GetByParameter?agentId=" + manifestoutgoing.AgentId + "&type=" + manifestoutgoing.PortType;
            return client.GetAsync<List<penjualan>>(uri);
        }

        internal Task<int> NewSTT()
        {
            return  client.GetAsync<int>("NewSTT");
        }

        internal async Task<List<Photo>> GetPhotoByPenjualanId(int id)
        {
            var clientPhoto = new Client("Photos");
            var res= await clientPhoto.GetAsync<List<Photo>>("GetPhotosByPenjualanId", id);
            return res;
        }

        internal Task<bool> IsSended(int id)
        {
            return client.GetAsync<bool>("IsSended");
        }

        internal async Task<bool> UpdateDeliveryStatus(deliverystatus deliveryStatus)
        {
            var res = await client.PutAsync<deliverystatus>("UpdateDeliveryStatus", deliveryStatus.Id, deliveryStatus);
            if (res != default(deliverystatus))
            {
                return true;
            }
            else
                return false;
         
        }

        internal async Task<Photo> AddNewPhoto(Photo ph)
        {
            var clientPhoto = new Client("Photos");
            var res = await clientPhoto.PostAsync<Photo>("AddNewPhoto", ph);
            
            return res;
        }

        internal async Task<bool> Update(int id, penjualan penj)
        {
            var item =await client.PutAsync<penjualan>("Put",id, penj);
            var data = Source.Where(O => O.Id == id).FirstOrDefault();
            if (item!=default(penjualan) && data!=null)
            {
                data.CustomerIsPay = item.CustomerIsPay;
                data.Details = item.Details;
                data.Etc = item.Etc;
                data.CityID = item.CityID;
                data.PackingCosts = item.PackingCosts;
                data.PayType = item.PayType;
                data.PortType = item.PortType;
                data.Price = item.Price;
                data.ReciverID = item.ReciverID;
                data.ShiperID = item.ShiperID;
                data.STT = item.STT;
                data.Tax = item.Tax;
                data.Total = item.Total;
                data.TypeOfWeight = item.TypeOfWeight;
                data.UserID = item.UserID;
                data.Content = item.Content;
                data.DoNumber = item.DoNumber;
                data.Id = item.Id;
                data.ChangeDate = item.ChangeDate;
                data.Note = item.Note;
                SourceView.Refresh();
                return true;
            }

            return false;
        }

        internal async Task<byte[]> GetPictureById(int id)
        {
            var clientPhoto = new Client("Photos");
            var res = await clientPhoto.GetAsync<byte[]>("GetPictureById", id);
            return res;
        }
    }
}
