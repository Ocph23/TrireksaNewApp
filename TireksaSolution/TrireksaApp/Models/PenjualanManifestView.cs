using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace TrireksaApp.Models
{
    public class PenjualanManifestView
    {
        private manifestoutgoing _manifest;

        public PenjualanManifestView()
        {
            Source = new ObservableCollection<ManifestView>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            MainVM = Common.ResourcesBase.GetMainWindowViewModel();
        }

        public async void SetManifest(manifestoutgoing manifest)
        {
            Source.Clear();
            this._manifest = manifest;
            var stt = _manifest.PackingList.GroupBy(O => O.PenjualanId).ToList();
            foreach (var item in stt)
            {
                var result = await MainVM.PenjualanCollection.GetItemById(item.Key);
                if(result!=null)
                {
                    var detail = from b in item.ToList()
                                 join c in result.Details on b.CollyNumber equals c.CollyNumber
                                 select c;

                    var newItem = new Models.ManifestView();
                    newItem.STT = string.Format("{0:D5}", result.STT);
                    newItem.Code = _manifest.Code;
                    newItem.PortType = manifest.PortType.ToString();
                    newItem.PayType = result.PayType;
                    newItem.Pcs = item.Count();
                    newItem.WeightView = detail.Sum(O => O.Weight);
                    newItem.ShiperName = MainVM.CustomerCollection.Source.Where(O => O.Id == result.ShiperID).FirstOrDefault().Name;
                    newItem.ReciverName = MainVM.CustomerCollection.Source.Where(O => O.Id == result.ReciverID).FirstOrDefault().Name;
                    newItem.AgentName = _manifest.Agent.Name;
                    newItem.CreatedDate = _manifest.CreatedDate;
                    Source.Add(newItem);
                }
            }
            SourceView.Refresh();
        }
        public MainWindowVM MainVM { get; private set; }
        public ObservableCollection<ManifestView> Source { get; set; }
        public CollectionView SourceView { get;  set; }
    }
}