using FirstFloor.ModernUI.Windows.Controls;
using ModelsShared.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TrireksaApp.Common;
using ModelsShared;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.Reporting.WinForms;
using TrireksaApp.Reports.Models;

namespace TrireksaApp.Contents.Invoice
{
    public class InvoiceListVM:Common.ViewModelBase
    {
        public CommandHandler PreviewManifest { get; private set; }
        public CommandHandler PreviewReport { get; private set; }
        public CommandHandler UpdateDeliveryData { get; private set; }
        public List<InvoiceStatus> InvoiceStatusView { get; private set; }
        public CommandHandler UpdateInvoiceStatus { get; private set; }
        public List<InvoicePayType> InvoicePayTypeView { get; private set; }

        public InvoiceListVM()
        {
            MainVM.InvoiceCollections.SourceView.Filter = InvoiceFilter;
            this.InvoiceStatusView = Common.ResourcesBase.GetEnumCollection<InvoiceStatus>();
            InvoicePayTypeView = Common.ResourcesBase.GetEnumCollection<InvoicePayType>();
            PreviewManifest = new CommandHandler { CanExecuteAction = x => PreviewManifestValidation(), ExecuteAction = x => PreviewManifestAction() };
            UpdateDeliveryData  = new CommandHandler { CanExecuteAction = x => UpdateDeliveryDataValidation(), ExecuteAction = x => UpdateDeliveryDataAction() };
            UpdateInvoiceStatus  = new CommandHandler { CanExecuteAction = x => UpdateInvoiceStatusValidation(), ExecuteAction = x => UpdateInvoiceStatusAction() };
            PreviewReport = new CommandHandler { ExecuteAction = PrintpreviewReportAction };
            MainVM.InvoiceCollections.RefreshCompleted += InvoiceCollections_RefreshCompleted;
            RefreshAction(null);
        }

        private void PrintpreviewReportAction(object obj)
        {
            var data = MainVM.InvoiceCollections.SourceView.Cast<invoice>().ToList();
            if (data != null && data.Count>0)
            {
                StringBuilder sb = new StringBuilder();
                if (IsUnpaid)
                    AddText("Belum Dibayar",sb);
                if (IsPaid)
                    AddText("Terbayar", sb);
                if (IsPending)
                    AddText("Pending", sb);
                if (IsPaid)
                    AddText("Batal", sb);

                if (sb.Length <= 0)
                    sb.Append("Laporan Invoice");
                var p = new ReportParameter() { Name="Title" };
                p.Values.Add(sb.ToString());
                ReportParameter[] param = new ReportParameter[] { p };

                var list = new List<InvoiceReport>();
                foreach(var item in data)
                {
                    list.Add(new InvoiceReport(item));
                }


                var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value =list},
               "TrireksaApp.Reports.Layouts.InvoiceReportLayout.rdlc", param);
                var dlg = new FirstFloor.ModernUI.Windows.Controls.ModernWindow
                {
                    Content = content,
                    Title = "Daftar Invoice",
                    Style = (Style)App.Current.Resources["BlankWindow"],
                    ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                    WindowState = WindowState.Maximized,
                };

                dlg.ShowDialog();
            }
        }

        private void AddText(string v, StringBuilder sb)
        {
            if (sb.Length > 0)
                sb.Append(", " + v);
            else
                sb.Append(v);
        }

        private void InvoiceCollections_RefreshCompleted()
        {
            if (ProgressIsActive)
                ProgressIsActive = false;
        }

        protected override void RefreshAction(object obj)
        {
            ProgressIsActive = true;
            MainVM.InvoiceCollections.RefreshAction(obj);
        }
        private bool InvoiceFilter(object obj)
        {
           
            var item= (ModelsShared.Models.invoice)obj;
            if (this.IsUnpaid)
            {
                if(IsDelivery)
                    return (item.InvoiceStatus == InvoiceStatus.Unpaid)&&item.IsDelivery;
                return item.InvoiceStatus == InvoiceStatus.Unpaid;
               
            }
            if (this.IsPaid)
            {
                if (IsDelivery)
                    return (item.InvoiceStatus == InvoiceStatus.Paid) && item.IsDelivery;
                return item.InvoiceStatus == InvoiceStatus.Paid;
            }
            if (this.IsCancel)
            {
                if (IsDelivery)
                    return (item.InvoiceStatus == InvoiceStatus.Cancel) && item.IsDelivery;
                return item.InvoiceStatus == InvoiceStatus.Cancel;
            }
            if (this.IsPending)
            {
                if (IsDelivery)
                    return (item.InvoiceStatus == InvoiceStatus.Pending) && item.IsDelivery;
                return item.InvoiceStatus == InvoiceStatus.Pending;
            }

            if(this.IsDelivery)
            {
                return item.IsDelivery;
            }
                return true;
        }

        private async void UpdateInvoiceStatusAction()
        {
            var selected = MainVM.InvoiceCollections.SelectedItem;
            var item = new ModelsShared.Models.invoice
            {
                Id = selected.Id,
                InvoiceStatus = selected.InvoiceStatus,
                InvoicePayType = selected.InvoicePayType, PaidDate=selected.PaidDate

            };



            var IsUpdate = false;
            if (await MainVM.InvoiceCollections.UpdateInvoiceStatusAction(selected.Id, item)) 
            {
                IsUpdate = true;
            }

            if (IsUpdate == true)
            {
                ModernDialog.ShowMessage("Status Invoice Is Updated ...!", "Infomration", MessageBoxButton.OK);
            }
            else
            {
                ModernDialog.ShowMessage("Status Invoice Can Not  Update ...!", "Error", MessageBoxButton.OK);
            }
        }

        private bool UpdateInvoiceStatusValidation()
        {
            if (MainVM.InvoiceCollections.SelectedItem != null)
            {
                if (MainVM.InvoiceCollections.SelectedItem.InvoiceStatus == InvoiceStatus.Paid && MainVM.InvoiceCollections.SelectedItem.InvoicePayType == InvoicePayType.None)
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        private async void UpdateDeliveryDataAction()
        {
            var selected = MainVM.InvoiceCollections.SelectedItem;
            var item = new ModelsShared.Models.invoice
            {
                Id = selected.Id,
                DeliveryDate = selected.DeliveryDate,
                IsDelivery = true,
                ReciveDate = selected.ReciveDate,
                ReciverBy = selected.ReciverBy
            };

            var IsUpdate = false;
            if ( await MainVM.InvoiceCollections.UpdateDeliveryDataAction(selected.Id, item))
            {
                IsUpdate = true;
            }

            if (IsUpdate == true)
            {
                ModernDialog.ShowMessage("Delivery Invoice Is Updated ...!", "Infomration", MessageBoxButton.OK);
            }
            else
            {
                ModernDialog.ShowMessage("Delivery Invoice Can Not  Update ...!", "Error", MessageBoxButton.OK);
            }
        }

        private bool UpdateDeliveryDataValidation()
        {
            if (MainVM.InvoiceCollections.SelectedItem != null)
            {
               
                    return true;
            }
               
            return false;
        }

        private async void PreviewManifestAction()
        {
            var data = await this.GetInvoiceReportModel();
           if(data!=null)
            {
                var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = data.OrderBy(O => O.STT) },
               "TrireksaApp.Reports.Layouts.InvoiceLayout.rdlc", null);
                var dlg = new FirstFloor.ModernUI.Windows.Controls.ModernWindow
                {
                    Content = content,
                    Title = "Invoice",
                    Style = (Style)App.Current.Resources["BlankWindow"],
                    ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                    WindowState = WindowState.Maximized,
                };

                dlg.ShowDialog();
            }
        }

        private async Task<List<Reports.Models.InvoiceReportModel>> GetInvoiceReportModel()
        {

            var selected = await MainVM.InvoiceCollections.GetItemById(MainVM.InvoiceCollections.SelectedItem.Id);
            if(selected!=null)
            {
                var result = from item in selected.Details
                             select new Reports.Models.InvoiceReportModel
                             {
                                 Id = item.Id,
                                 Pcs = item.Pcs, PortType=item.PortType,
                                 Price = item.Price,
                                 Reciver = item.Reciver,
                                 Shiper = item.Shiper,
                                 PenjualanId = item.PenjualanId,
                                 Total = item.Total,
                                 DoNumber = item.DoNumber,
                                 Tujuan = item.Tujuan,
                                 Via = item.Via,
                                 Weight = item.Weight,
                                 Etc = item.Etc,
                                 InvoiceId = item.InvoiceId,
                                 PackingCosts = item.PackingCosts,
                                 Tax = item.Tax,
                                 NumberView = MainVM.InvoiceCollections.SelectedItem.NumberView,
                                 CustomerName = MainVM.InvoiceCollections.SelectedItem.CustomerName,
                                 DeadLine = MainVM.InvoiceCollections.SelectedItem.DeadLine,
                                  CreateDate=item.ChangeDate,
                                 STT = item.STT,
                                 Terbilang = selected.Details.Sum(O=>O.Total).Terbilang()
                             };
                return result.ToList();
            }
            return null;
        }

        private bool PreviewManifestValidation()
        {
            return true;
        }

        // view
        private bool _isUnpaid;

        public bool IsUnpaid
        {
            get { return _isUnpaid; }
            set { _isUnpaid = value; SetOtherStatusFalse(InvoiceStatus.Unpaid, value);
                OnPropertyChanged("IsUnpaid"); }
        }

        private bool _ispaid;

        public bool IsPaid
        {
            get { return _ispaid; }
            set { _ispaid = value;
                SetOtherStatusFalse(InvoiceStatus.Paid, value);
                OnPropertyChanged("IsPaid");
            }
        }

        private bool _isPending;

        public bool IsPending
        {
            get { return _isPending; }
            set { _isPending = value;
                SetOtherStatusFalse(InvoiceStatus.Pending, value);
                OnPropertyChanged("IsPending"); }
        }
        private bool _isCancel;

        public bool IsCancel
        {
            get { return _isCancel; }
            set { _isCancel = value;

                SetOtherStatusFalse(InvoiceStatus.Cancel, value);

                OnPropertyChanged("IsCancel"); }
        }


        private bool _isDelivery;

        public bool IsDelivery
        {
            get { return _isDelivery; }
            set
            {
                _isDelivery = value;
                MainVM.InvoiceCollections.SourceView.Refresh();
                OnPropertyChanged("IsDelivery");
            }
        }


        public void SetOtherStatusFalse(InvoiceStatus status,bool value)
        {
            if (value)
            {
                switch (status)
                {
                    case InvoiceStatus.Unpaid:
                        IsCancel = false;
                        IsPaid = false;
                        IsPending = false;
                        break;
                    case InvoiceStatus.Pending:
                        IsCancel = false;
                        IsPaid = false;
                        IsUnpaid = false;
                        break;
                    case InvoiceStatus.Paid:
                        IsCancel = false;
                        IsPending = false;
                        IsUnpaid = false;
                        break;
                    case InvoiceStatus.Cancel:
                        IsPaid = false;
                        IsPending = false;
                        IsUnpaid = false;
                        break;
                        
                    default:
                        break;
                }
               
            }
            MainVM.InvoiceCollections.SourceView.Refresh();
        }

    }
}
