using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using ModelsShared.Models;
using TrireksaApp.Models;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Penjualan
{
    public class AddCollyVM :NotifyPropertyChanged, IDataErrorInfo, IDisposable
    {
        public List<ModelsShared.Models.colly> details;
        private string _tcolly;
        private string _tweight;
        public MainWindowVM MainVM { get; }
        public CommandHandler RemoveCommand { get; }

        public TypeOfWeight TypeOfWeightBase;
        private newcolly _SelectedItem;
      
        public ObservableCollection<newcolly> Source { get; set; }
        public CollectionView SourceView { get; set; }
        public AddCollyVM(ModelsShared.Models.TypeOfWeight value, List<ModelsShared.Models.colly> details)
        {
            MainVM= Common.ResourcesBase.GetMainWindowViewModel();
            RemoveCommand = new CommandHandler {CanExecuteAction=RemoveValidate,ExecuteAction=RemoveAction };

            this.TypeOfWeightBase = value;
            this.details = details;

            if (TypeOfWeights == null)
            {
                TypeOfWeights = new List<ModelsShared.Models.TypeOfWeight>();

            }

            TypeOfWeights.Clear();
            if (value == ModelsShared.Models.TypeOfWeight.Volume)
            {
                TypeOfWeights.Add(ModelsShared.Models.TypeOfWeight.Volume);

            }
            else
            {
                TypeOfWeights.Add(ModelsShared.Models.TypeOfWeight.Weight);
                TypeOfWeights.Add(ModelsShared.Models.TypeOfWeight.WeightVolume);

            }

            Source = new ObservableCollection<newcolly>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            if (details == null)
                details = new List<colly>();
            SetDetailsToNewCollie(details);


        }

        private void RemoveAction(object obj)
        {
            Source.Remove(SelectedItem);
        }

        private bool RemoveValidate(object obj)
        {
            if (SelectedItem != null)
                return true;
            return false;
        }

        public newcolly SelectedItem
        {
            get
            {
              
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged("SelectedItem");

            }
        }


        public string TotalColly
        {
            get
            {
                _tcolly = "Total Colly : "+ Source.Sum(O => O.Jumlah).ToString() + " Pcs";
                return _tcolly;
            }
            set
            {
                _tcolly = value;
                OnPropertyChanged("TotalColly");

            }
        }
        public string TotalWeight
        {
            get
            {
                if (TypeOfWeightBase == ModelsShared.Models.TypeOfWeight.Weight || TypeOfWeightBase== ModelsShared.Models.TypeOfWeight.WeightVolume)
                    _tweight ="Total Weight : "+ Source.Sum(O => O.TWeight).ToString() + " Kg";
                else
                    _tweight = "Total Weight : " + Source.Sum(O => O.TWeight).ToString() + " M3";
                return _tweight;
            }
            set
            {
                _tweight = value;
                OnPropertyChanged("TotalWeight");

            }
        }

       public void Refresh()
        {
            TotalColly = "";
            TotalWeight = "";
        }

    
        private void SetDetailsToNewCollie(List<ModelsShared.Models.colly> details)
        {
            Source.Clear();
           foreach (var item in details)
            {
                Source.Add(new newcolly()
                {
                    CollyNumber = item.CollyNumber,
                    Hight = item.Hight,
                    Id = item.Id,
                    IsSended = item.IsSended,
                    Long = item.Long,
                     PenjualanId= item.Id,
                    TypeOfWeight = item.TypeOfWeight,
                    Weight = item.Weight,
                    Wide = item.Wide, Jumlah=1
                });
            }

            SourceView.Refresh();
        }

        public string this[string columnName]
        {
            get
            {
                return null;
            }
        }

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public List<ModelsShared.Models.TypeOfWeight> TypeOfWeights { get; set; }

        public void Dispose()
        {
            details.Clear();
            int number = 1;

            foreach (var item in Source )
            {
                if(item.Jumlah>1 )
                {
                    for(var i = 0;i<item.Jumlah;i++)
                    {
                        var it = new ModelsShared.Models.colly
                        {
                            CollyNumber = number++,
                            Hight = item.Hight,
                            Id = item.Id,
                            IsSended = item.IsSended,
                            Long = item.Long,
                            PenjualanId = item.Id, STT=item.STT,
                            TypeOfWeight = item.TypeOfWeight,
                            Weight = item.Weight,
                            Wide = item.Wide
                        };
                        
                        details.Add(it);
                    }
                }else
                {
                    item.CollyNumber = number++;
                    details.Add(item);
                }
                 
            }
        }
    }






   
}
