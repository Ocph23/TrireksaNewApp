using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class InvoiceCollection :BaseCollection
    {
        private Client client = new Client("Invoices");
        private invoice _selected;
        public event RefreshComplete RefreshCompleted;


        public invoice SelectedItem
        {
            get { return _selected; }
            set { _selected = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<invoice> Source { get; set; }
        public CollectionView SourceView { get; set; }

        public InvoiceCollection()
        {
            Source = new ObservableCollection<invoice>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            SourceView.SortDescriptions.Add(new System.ComponentModel.SortDescription { PropertyName = "Number", Direction = System.ComponentModel.ListSortDirection.Descending });
        }

        private async void InitAsync()
        {
            Source.Clear();
            var url = string.Format("Get?start={0}-{1}-{2}&end={3}-{4}-{5}", StartDate.Date.Year,StartDate.Date.Month,StartDate.Day,
                EndDate.Year, EndDate.Month, EndDate.Day) ;
            var result = await client.GetAsync<List<invoice>>(url);
            if(result!=null)
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }
            RefreshCompleted?.Invoke();
        }

        public async Task<bool> Add(invoice item)
        {
            var x = await client.PostAsync<invoice>("Post",item);
            if (x != null)
            {
                this.Source.Add(x);
                SourceView.Refresh();
                this.SelectedItem = x;
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<invoice> GetItemById(int Id)
        {
            var result = await client.GetAsync<invoice>("Get", Id);
            return result;
        }

        internal async Task<bool> UpdateInvoiceStatusAction(int id, invoice item)
        {
            var res= await client.PutAsync<invoice>("UpdateInvoiceStatusAction",id, item);
            if (res != default(invoice))
                return true;
            else
                return false;
        }

        internal async Task<bool> UpdateDeliveryDataAction(int id, invoice item)
        {
            var res=await client.PutAsync<invoice>("UpdateDeliveryDataAction", id, item);
            if (res != default(invoice))
                return true;
            else
                return false;
        }

        internal async Task<invoice> GetInvoiceForPenjualanInfo(int id)
        {
            var res= await client.GetAsync<invoice>("GetInvoiceForPenjualanInfo", id);
            return res;
        }

        public Task RefreshAction(object obj)
        {
            InitAsync();
            return Task.FromResult(0);
        }
    }
}
