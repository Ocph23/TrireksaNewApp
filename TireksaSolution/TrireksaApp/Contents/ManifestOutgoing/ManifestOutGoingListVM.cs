using FirstFloor.ModernUI.Presentation;
using System.Threading.Tasks;
using TrireksaApp.CollectionsBase;
using ModelsShared.Models;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using TrireksaApp.Models;
using TrireksaApp.Common;
using Newtonsoft.Json;
using OcphSMSLib.Models;
using System;
using System.Collections.Generic;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    public class ManifestOutGoingListVM : ViewModelBase
    {
        private manifestoutgoing _selected;
        private bool _originISync;
        private bool _destinationAsyn;
        private bool _updateAsync;
        public Common.CommandHandler Preview { get; set; }
        public ManifestOutGoingCollection Collection { get; set; }


        public ManifestOutGoingListVM()
        {
            Manifest = new Models.PenjualanManifestView();
            Preview = new CommandHandler { ExecuteAction = x => PreviewAction(), CanExecuteAction = x => PreviewValidation() };
            UpdateDeparture = new CommandHandler { CanExecuteAction = x => UpdateDepartureValidate(), ExecuteAction = UpdateDepartureAction };
            UpdateOrigin = new CommandHandler { CanExecuteAction = x => UpdateOriginValidate(), ExecuteAction = UpdateOriginAction };
            UpdateManifestInfo = new CommandHandler { CanExecuteAction = UpdateManifestInfoValidation, ExecuteAction = UpdateManifestInfoActionAsync };
            PrintTitipanKapal = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = PrintTitipanKapalAction };
            PrintPackingList = new CommandHandler { CanExecuteAction = x => SelectedItem != null, ExecuteAction = PrintPackingListAction };
            MainVM.ManifestOutgoingCollection.RefreshCompleted += ManifestOutgoingCollection_RefreshCompleted;
            RefreshAction(null);
        }


        protected override void RefreshAction(object obj)
        {
            ProgressIsActive = true;
            MainVM.ManifestOutgoingCollection.Refresh();
        }


        private void ManifestOutgoingCollection_RefreshCompleted()
        {
            if (ProgressIsActive)
                ProgressIsActive = false;
        }

        private async void PrintPackingListAction(object obj)
        {
            var list = await MainVM.ManifestOutgoingCollection.GetPackingList(SelectedItem.Id);
            var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value =list },
                   "TrireksaApp.Reports.Layouts.PackingListLayout.rdlc", null);
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Bukti Titipan Kapal",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();

        }

        private async void PrintTitipanKapalAction(object obj)
        {
            var item = await MainVM.ManifestOutgoingCollection.GetTitipanKapal(SelectedItem.Id);
            if(item!=null)
            {
                var list = new List<titipankapal>();
                list.Add(item);
                var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = list },
                  "TrireksaApp.Reports.Layouts.TitipanKapalLayout.rdlc", null);
                var dlg = new ModernWindow
                {
                    Content = content,
                    Title = "Bukti Titipan Kapal",
                    Style = (Style)App.Current.Resources["BlankWindow"],
                    ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                    WindowState = WindowState.Maximized,
                };

                dlg.ShowDialog();
            }else
            {
                ModernDialog.ShowMessage("Data Tidak Ditemukan","Info", MessageBoxButton.OK);
            }

        }

        private async void UpdateManifestInfoActionAsync(object obj)
        {
            UpdateInformationIsAsync = true;
            if (SelectedItem.Information.ManifestId == 0)
                SelectedItem.Information.ManifestId = SelectedItem.Id;
            var task = await MainVM.ManifestOutgoingCollection.UpdateInformation(SelectedItem.Information);

            if (task)
            {
                var message = new OutMessage
                {
                    Destination = SelectedItem.Agent.Handphone,
                    MessageText = string.Format("Mohon Dimonitor/t Via:{0}/t {1} Cly/t ABK={2}/t Posisi:{3}",
                   SelectedItem.Information.ArmadaName, SelectedItem.PackingList.Count,
                    SelectedItem.Information.CrewName, SelectedItem.Information.Address)
                };

                try
                {
                    Common.ResourcesBase.GetMainWindowViewModel().SMS.SendMessage(message);
                }
                catch (System.Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
            UpdateInformationIsAsync = false;



        }


        private bool UpdateManifestInfoValidation(object obj)
        {
            if (SelectedItem != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool OnOriginPortIsSync
        {
            get
            {
                return _originISync;

            }
            set
            {
                _originISync = value;
                OnPropertyChanged("OnOriginPortIsSync");

            }
        }
        private bool UpdateOriginValidate()
        {
            if (SelectedItem != null && SelectedItem.OnOriginPort!=null && OnOriginPortIsSync == false)
            {
                return true;
            }
            else
                return false;
        }


       
        private async void UpdateOriginAction(object obj)
        {
            OnOriginPortIsSync = true;
            var res = await MainVM.ManifestOutgoingCollection.UpdateOrigin(SelectedItem);
            if (res)
            {
                OnOriginPortIsSync = false;
            }
            OnOriginPortIsSync = false;
        }

      
        private bool UpdateDepartureValidate()
        {
            if (SelectedItem != null && SelectedItem.OnDestinationPort!=null && OnDestinationPortIsSync== false)
            {
                return true;
            }
            else
                return false;
        }
        private async void UpdateDepartureAction(object obj)
        {
            OnDestinationPortIsSync = true;
            bool res = await MainVM.ManifestOutgoingCollection.UpdateDestination(SelectedItem);
            if(res)
                OnDestinationPortIsSync = false;

        }

        

        private bool PreviewValidation()
        {
            return true;
        }

        private void PreviewAction()
        {
            var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = Manifest.Source },
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

       
        public manifestoutgoing SelectedItem
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if(_selected!=null)
                {
                    if (_selected.Information == null)
                    {
                        _selected.Information = new ManifestInformation { ManifestType = ManifestType.Outgoing, ManifestId = _selected.Id, PortType = _selected.PortType };
                    }
                    Manifest.SetManifest(_selected);
                }
                OnPropertyChanged("SelectedItem");
            }

        }

        public PenjualanManifestView Manifest { get; set; }
        public CommandHandler UpdateDeparture { get; private set; }
        public CommandHandler UpdateOrigin { get; private set; }
        public bool OnDestinationPortIsSync
        {
            get { return _destinationAsyn; }
            set
            {
                _destinationAsyn = value;
                OnPropertyChanged("OnDestinationPortIsSync");
            }
        }

        public CommandHandler UpdateManifestInfo { get; private set; }
        public CommandHandler PrintTitipanKapal { get; }
        public CommandHandler PrintPackingList { get; }

        public bool UpdateInformationIsAsync
        {
            get { return _updateAsync; }
            set
            {
                _updateAsync = value;
                OnPropertyChanged("UpdateInformationIsAsync");
            }
        }
    }
}