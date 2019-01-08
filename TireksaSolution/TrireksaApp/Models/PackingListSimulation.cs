using FirstFloor.ModernUI.Presentation;
using ModelsShared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TrireksaApp.Models
{
    public class PackingListSimulation:NotifyPropertyChanged
    {
        public PackingListSimulation(List<PenjualanView> penjualanFromdb)
        {
            if (Source == null)
            {
                this.penjualanFromDatabase = penjualanFromdb;
         
                this.Packs = new List<Pack>();
             //   this.PackingLists = new List<ModelsShared.Models.colly>();
                this.Collies = new List<ModelsShared.Models.colly>();
                var pack = new Pack { PackingLists = new List<ModelsShared.Models.colly>(), PackNumber = 1 };
                this.Packs.Add(pack);
              
                this.Source = (CollectionView)CollectionViewSource.GetDefaultView(penjualanFromdb);
                this.SelectedItemDetailsView = (CollectionView)CollectionViewSource.GetDefaultView(this.Collies);
                this.PacksView = (CollectionView)CollectionViewSource.GetDefaultView(this.Packs);

                DetailPackSource = new List<colly>();
                
                this.PackDetailsView = new CollectionView(this.DetailPackSource);

                if(penjualanFromDatabase!=null&&penjualanFromDatabase.Count>0)
                {
                    var penjualanview = penjualanFromDatabase[0];
                    penjualanview.OnChangeColly += Penjualanview_OnChangeColly;
                }


            }
        }

        private void Penjualanview_OnChangeColly()
        {
            SelectedItemDetailsView.Refresh();
        }

        public List<Models.Pack> Packs { get; set; }
        public ModelsShared.Models.colly PackDetailsSelectedItem { get; set; }
        public CollectionView PacksView { get; set; }
        public CollectionView PackDetailsView { get; set; }
        public CollectionView Source { get; set; }
        public CollectionView SelectedItemDetailsView { get; set; }
     //   public List<ModelsShared.Models.colly> PackingLists { get; set; }
  
        public List<colly> Collies { get; set; }



        public ModelsShared.Models.colly STTDetailSelected { get; set; }
        public PenjualanView SelectedItem
        {

            get { return _selected; }
            set
            {
                _selected = value;
                if(value!=null)
                {

                    value.OnChangeColly += Penjualanview_OnChangeColly;

                    if (value.IsSelected)
                    {

                        Collies.Clear();
                        foreach (var item in value.Details)
                        {
                            if (item.STT == 0)
                            {
                                item.STT = value.STT;
                                item.PenjualanId = value.Id;
                            }

                            Collies.Add(item);
                        }
                        //      this.SelectedItemDetailsView.Refresh();
                    }
                    else
                    {
                        //  _selected = null;
                        if (Collies != null && Collies.Count > 0)
                        {

                            foreach (var item in Packs)
                            {
                                if (item.PackingLists != null && item.PackingLists.Count>0)
                                {
                                    foreach (var c in item.PackingLists)
                                    {
                                        if (c.STT == value.STT)
                                            item.PackingLists.Remove(c);
                                    }
                                }
                            }
                            if (PackDetailsView != null)
                                PackDetailsView.Refresh();
                            Collies.Clear();
                        }

                    }


                }


                this.SelectedItemDetailsView.Refresh();
                OnPropertyChanged("SelectedItem");
            }
        }

       

        public Pack PackSelectedItem
        {

            get { return _packSelectedItem; }
            set
            {
                _packSelectedItem = value;
                if(value!=null)
                {
                    if (value.PackingLists == null)
                        _packSelectedItem.PackingLists = new List<colly>();
                    else
                    {
                        DetailPackSource.Clear();
                        foreach (var item in _packSelectedItem.PackingLists)
                        {
                            DetailPackSource.Add(item);
                        }
                    }
                    PackDetailsView.Refresh();
                }
    
                OnPropertyChanged("PackSelectedItem");
            }
        }

        public List<PenjualanView> penjualanFromDatabase { get; set; }
        public IList DetailPackSource { get; set; }

        private PenjualanView _selected;
        private Pack _packSelectedItem;
    }
}
