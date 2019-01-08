using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;   
using ModelsShared.Models;
using System.Windows.Data;
using TrireksaApp.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    public class ManifestOutGoingCreateVM : ModelsShared.Models.manifestoutgoing,IDataErrorInfo
    {
        public Models.PenjualanView STTSelectedItem { get; set; }
        private ManifestOutGoingCollection vm = Common.ResourcesBase.GetMainWindowViewModel().ManifestOutgoingCollection;

        public ObservableCollection<port> DestinationSource { get; set; }

        public ManifestOutGoingCreateVM()
        {
            this.PortTypes = Common.ResourcesBase.GetEnumCollection<ModelsShared.Models.PortType>();
            this.PortCollection = Common.ResourcesBase.GetMainWindowViewModel().PortCollection;
            this.OriginView = (CollectionView)CollectionViewSource.GetDefaultView(this.PortCollection.Source);
            DestinationSource = new ObservableCollection<port>();
            this.DestinationView = (CollectionView)CollectionViewSource.GetDefaultView(DestinationSource);
            this.OriginView.Filter = PortFilter;
            DestinationView.Filter = PortFilter;

            this.AgentCollection = Common.ResourcesBase.GetMainWindowViewModel().AgentCollection;
            BrowseManifestCommand = new CommandHandler { CanExecuteAction = x => BrowseManifestCommandValidate(), ExecuteAction = x => BrowseManifestCommandAction() };

            this.PenjualanTemporaty = new List<Models.PenjualanView>();
            this.PenjualanTemporaryView = (CollectionView)CollectionViewSource.GetDefaultView(this.PenjualanTemporaty);
            DeleteSTTFromList = new CommandHandler { CanExecuteAction = x => (STTSelectedItem != null), ExecuteAction = x => DeleteSTTFromListAction() };
            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
            Preview = new Common.CommandHandler { ExecuteAction = x => PreviewAction(), CanExecuteAction = x => PreviewValidation() };
            Cancel = new CommandHandler { ExecuteAction = CancelAction, CanExecuteAction = x => true };
        }

        private void CancelAction(object obj)
        {
            PackingListSimulation = null;
            PenjualanTemporaty.Clear();
            PenjualanTemporaryView.Refresh();
            Destination = 0;
            Origin = 0;
            Agent = null;
            PortType = PortType.None;
        }

        private bool BrowseManifestCommandValidate()
        {
           if (this.SourceFromDatabase != null && this.SourceFromDatabase.Count>0)
            {
                if (Agent!=null && PortType!= PortType.None)
                {
                    return true;
                }
            }
            return false;
        }

        private bool PreviewValidation()
        {
            if (this.PackingList != null)
                return true;
            else
                return false;
        }

        private void PreviewAction()
        {
            var man = new Models.PenjualanManifestView();

            man.SetManifest(this);
            var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = man.Source },
                "TrireksaApp.Reports.Layouts.ManifestOutgoingLayout.rdlc", null);
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Manifest Outgoing",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
        }

        private async void SaveAction()
        {
            var item = new ModelsShared.Models.manifestoutgoing
            {
                AgentId = this.AgentId,
                Code = this.Code,
                Destination = this.Destination,
                Id = this.Id,
                Information = this.Information,
                Origin = this.Origin,
                PackingList = this.PackingList,
                PortType = this.PortType,
                UserId = this.UserId
            };
          
            var result = await vm.Add(item);
            if (result != null)
            {
                this.Id = result.Id;
                this.Code = result.Code;
                this.CreatedDate = result.CreatedDate;
                this.Destination = result.Destination;
                this.Information = result.Information;
                this.OnDestinationPort = result.OnDestinationPort;
                this.OnOriginPort = result.OnDestinationPort;
                this.Origin = result.Origin;
                this.PackingList = result.PackingList;
                this.PortType = result.PortType;
                this.UpdateDate = result.UpdateDate;
                this.UserId = result.UserId;
                result.OriginPort = this.PortCollection.Source.Where(O => O.Id == Origin).FirstOrDefault();
                result.DestinationPort = this.PortCollection.Source.Where(O => O.Id == Destination).FirstOrDefault();
                result.Agent = this.Agent;
                result.PackingList = item.PackingList;

                ModernDialog.ShowMessage("Data Is Saved... !", "Information", MessageBoxButton.OK);
               
                vm.SourceView.Refresh();
            }
            else
            {
                ModernDialog.ShowMessage("Data Not Saved... !", "Error", MessageBoxButton.OK);
            }

        }

        private bool SaveValidation()
        {
            return true;
        }


        private void DeleteSTTFromListAction()
        {
            var item = SourceFromDatabase.Where(O => O.STT == STTSelectedItem.STT).FirstOrDefault();
            item.IsSelected = false;
            PenjualanTemporaty.Remove(STTSelectedItem);
            PenjualanTemporaryView.Refresh();
        }

        private void BrowseManifestCommandAction()
        {
            var view = new Contents.ManifestOutgoing.BrowseSTT();

            var dlg = new ModernWindow()
            {
                Title = "Simulasi Manifest Outgoing",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
                Content = view,
            };

            if (this.PackingListSimulation == null)
            {
                this.PackingListSimulation = new Models.PackingListSimulation(this.SourceFromDatabase);
            }


            var vm = new Contents.ManifestOutgoing.BrowseSTTVM(PackingListSimulation) { CloseWindow = dlg.Close };
            view.DataContext = vm;
            dlg.ShowDialog();

            if (PackingListSimulation.Packs.Count > 0)
            {

                this.PackingList = new List<packinglist>();
                PenjualanTemporaty.Clear();
                foreach (var item in SourceFromDatabase.Where(O => O.IsSelected == true))
                {
                    
                    var a = new PenjualanView { Reciver = item.Reciver, Shiper = item.Shiper, STT = item.STT };
                    foreach (var pack in PackingListSimulation.Packs)
                    {
                        foreach (var cly in pack.PackingLists)
                        {
                            if (item.STT == cly.STT)
                            {
                                PackingList.Add(new packinglist { PenjualanId=item.Id, CollyId=cly.Id, CollyNumber = cly.CollyNumber, PackNumber = pack.PackNumber, STT = item.STT, Weight = cly.Weight });
                                a.Details.Add(cly);
                                a.Pcs++;
                                a.Weight += cly.Weight;
                            }
                        }
                    }
                    if(a.Pcs>0)
                        PenjualanTemporaty.Add(a);

                }
                this.PenjualanTemporaryView.Refresh();
            }
            else
                this.PackingListSimulation = null;


        }

        private PortType _port;
        public PortType PortSelected
        {
            get { return _port; }
            set
            {
                _port = value;
                OriginView.Refresh();
                SetDestinationSource();
                OnPropertyChange("PortSelected");
            }
        }

        private void SetDestinationSource()
        {
            DestinationSource.Clear();
           foreach(var item in PortCollection.Source)
            {
                if(Agent!=null && Agent.CitiesCanAccess!=null)
                {
                    var CityCanAccess = Agent.CitiesCanAccess.Where(O => O.CityId == item.CityID).FirstOrDefault();
                    if (CityCanAccess != null)
                        DestinationSource.Add(item);
                }
              
            }

            DestinationView.Refresh();
        }

        private bool PortFilter(object obj)
        {
            ModelsShared.Models.port x = (ModelsShared.Models.port)obj;

            if(x!=null && Agent!=null)
            {
                return x.PortType == this.PortType;
            }
            
            return false;
        }
      

        internal void SetReferance(ManifestInformation vm)
        {
            this.Information = vm;
            if (vm.GetType().Name == "SeaReferanceVM")
            {

            }


        }

        public PortCollection PortCollection { get; private set; }
        public List<ModelsShared.Models.PortType> PortTypes { get; private set; }
        public AgentCollection AgentCollection { get; private set; }
        public CommandHandler BrowseManifestCommand { get; set; }

        internal List<Models.PenjualanView> PenjualanTemporaty { get; private set; }
        public CollectionView PenjualanTemporaryView { get; private set; }
        public List<PenjualanView> SourceFromDatabase { get; private set; }
        public CommandHandler DeleteSTTFromList { get; private set; }


        public CommandHandler Save { get; set; }
        public PackingListSimulation PackingListSimulation { get; private set; }
        public CommandHandler Preview { get; private set; }
        public CommandHandler Cancel { get; }
        public CollectionView OriginView { get; private set; }
        public CollectionView DestinationView { get; private set; }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public string this[string columnName]
        {
            get
            {
                if (columnName == "AgentId" && Agent!=null)
                {
                    return  (Agent.Id == 0) ? "Port Same" : null;
                }

             
                if (columnName=="Destination")
                {
                    return (Origin == Destination) ? "Port Same" : null;
                }
                if (columnName == "Origin")
                {
                    return (Origin == Destination) ? "Port Same" : null;
                }

                return null;

             
            }
        }

        internal async void GetPenjualan()
        {
            if (this.PortType != PortType.None)
            {
                this.PenjualanTemporaty.Clear();
                OriginView.Refresh();
                DestinationView.Refresh();
                this.PenjualanTemporaryView.Refresh();
                this.SourceFromDatabase = new List<Models.PenjualanView>();
                if (this.AgentId > 0 && this.PortType != ModelsShared.Models.PortType.None)
                {
                    var result = await Common.ResourcesBase.GetMainWindowViewModel().PenjualanCollection.GetByParameter(new manifestoutgoing { AgentId = this.AgentId, PortType = this.PortType });
                    var customers = Common.ResourcesBase.GetMainWindowViewModel().CustomerCollection.Source;
                    if (result != null && customers != null)
                    {
                        var a = from r in result
                                select new Models.PenjualanView { Id = r.Id, STT = r.STT, Details = r.Details, Shiper = r.Shiper, Reciver = r.Reciver };

                        this.SourceFromDatabase.Clear();
                        foreach (var item in a)
                        {
                            this.SourceFromDatabase.Add(item);
                        }
                    }


                }


            }
        }
    }
}
