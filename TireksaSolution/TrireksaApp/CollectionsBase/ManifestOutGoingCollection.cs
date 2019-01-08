using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class ManifestOutGoingCollection:BaseCollection
    {
        public event RefreshComplete RefreshCompleted;
        private Client client = new Client("ManifestOutgoing");
        public ObservableCollection<ModelsShared.Models.manifestoutgoing> Source { get; set; }
        public CollectionView SourceView { get; set; }


        public virtual manifestoutgoing SelectedItem { get; set; }

        public ManifestOutGoingCollection()
        {
            Source = new ObservableCollection<ModelsShared.Models.manifestoutgoing>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        public Task Refresh()
        {
            InitAsync();
            return Task.FromResult(0);
        }

        public async void InitAsync()
        {
            Source.Clear();
            var result = await client.GetAsync<List<manifestoutgoing>>("Get");
            if (result != null)
            {
                var results = result.Where(O => O.CreatedDate >= StartDate && O.CreatedDate <= EndDate).ToList();
                foreach (var item in results)
                {
                    Source.Add(item);
                   
                }
            }
            SourceView.Refresh();
            RefreshCompleted?.Invoke();
        }

        public Task<manifestoutgoing> GetItemById(int Id)
        {
            return client.GetAsync<manifestoutgoing>("Get", Id);
        }

        public async Task<manifestoutgoing> Add(manifestoutgoing item)
        {
            var result = await client.PostAsync<manifestoutgoing>("Post",item);
            if (result != null)
                Source.Add(result);
            return result;
        }

        internal async Task<bool> UpdateInformation(ManifestInformation information)
        {
            var result = await client.PostAsync<ManifestInformation>("UpdateInformation",information);
            if (result != default(ManifestInformation))
                return true;
            else
                return false;
        }

        internal async Task<bool> UpdateOrigin(manifestoutgoing selectedItem)
        {
            var result = await client.PutAsync<manifestoutgoing>("UpdateOrigin", selectedItem.Id, selectedItem);
            if (result != default(manifestoutgoing))
                return true;
            else
                return false;
        }

        internal async Task< bool> UpdateDestination(manifestoutgoing selectedItem)
        {
            var result = await client.PutAsync<manifestoutgoing>("UpdateDestination", selectedItem.Id, selectedItem);
            if (result != default(manifestoutgoing))
                return true;
            else
                return false;
        }

        internal async Task<List<manifestoutgoing>> ManifestsByPenjualanId(int id)
        {
            var res= await client.GetAsync<List<manifestoutgoing>>("ManifestsByPenjualanId", id);
            return res;
        }

        internal  async  Task<titipankapal> GetTitipanKapal(int manifestId)
        {
            return await client.GetAsync<titipankapal>("GetTitipanKapal",manifestId);
        }

        internal Task<List<PackingListPrintModel>> GetPackingList(int manifestId)
        {
            return client.GetAsync<List<PackingListPrintModel>>("GetPackingList", manifestId);

        }
    }
}
