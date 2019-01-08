using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;
using ModelsShared.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Newtonsoft.Json;

namespace TrireksaApp.Contents.Invoice
{
    public class InvoiceCreateVM:invoice
    {
        public InvoiceCreateVM()
        {
            this.CreateDate = DateTime.Now;
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            this.Details = new List<invoicedetail>();
            this.SourceView = (CollectionView)CollectionViewSource.GetDefaultView(this.Details);
            this.DeadLine = DateTime.Now.AddMonths(1);
            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
            PreviewManifest= new CommandHandler { CanExecuteAction = x => PreviewManifestValidation(), ExecuteAction = x => PreviewManifestAction() };
            SaveAndPrint = new CommandHandler { CanExecuteAction = x => SaveAndPrintValidation(), ExecuteAction = x => SaveAndPrintAction() };
            MainVM.CustomerCollection.SourceView.Filter = ShiperFilter;
        }


        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set {
                searchText = value;
                OnPropertyChange("SearchText");
                MainVM.CustomerCollection.SourceView.Refresh();
            }
        }


        private bool ShiperFilter(object x)
        {
            var scr = string.Empty;
            var obj = (ModelsShared.Models.customer)x;

            if (obj != null && !string.IsNullOrEmpty(SearchText))
            {
                scr = SearchText.ToUpper().Trim();
                var data = obj.Name.ToUpper();
                return data.Contains(scr);
            }
            return true;
        }

        private bool SaveAndPrintValidation()
        {
            if (this.Number > 0)
                return false;
            return true;
        }

        private void SaveAndPrintAction()
        {
            throw new NotImplementedException();
        }

        private void PreviewManifestAction()
        {
            var data = this.GetInvoiceReportModel();
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

        private List<Reports.Models.InvoiceReportModel> GetInvoiceReportModel()
        {
           var result = from item in this.Details.Where(o => o.IsSelected)
                         select new Reports.Models.InvoiceReportModel { Id = item.Id, Pcs = item.Pcs, Price = item.Price, STT = item.STT, ChangeDate=item.ChangeDate,
                             Tujuan = item.Tujuan, DoNumber = item.DoNumber, Via = item.Via,  PortType=item.PortType,  
                          Reciver=item.Reciver, Shiper=item.Shiper,  PenjualanId=item.PenjualanId, Total=item.Total, Weight=item.Weight,
                           Etc=item.Etc, Tax=item.Tax, PackingCosts=item.PackingCosts, InvoiceId=item.InvoiceId,   Terbilang=GrandTotal.Terbilang(),
                         NumberView=this.NumberView, CustomerName=this.CustomerSelectedItem.Name, DeadLine=this.DeadLine,   CreateDate=item.ChangeDate};
            return result.ToList();
        }

        private bool PreviewManifestValidation()
        {
            return true;
        }

        private async void SaveAction()
        {
            var item = new ModelsShared.Models.invoice
            {
                CustomerId = this.CustomerId,
                CreateDate = this.CreateDate,
                CustomerName = this.CustomerName,
                DeadLine = this.DeadLine,
                DeliveryDate = this.DeliveryDate,
                Details = this.Details.Where(O=>O.IsSelected).ToList(), 
                Id = this.Id, 
                InvoicePayType = this.InvoicePayType,
                InvoiceStatus = this.InvoiceStatus,
                IsDelivery = this.IsDelivery,
                Number = this.Number,
                ReciveDate = this.ReciveDate,
                ReciverBy = this.ReciverBy,
                UserId = this.UserId
            };
           var result = await MainVM.InvoiceCollections.Add(item);
            if(result==true)
            {
                MainVM.InvoiceCollections.SourceView.Refresh();
                this.Number = MainVM.InvoiceCollections.SelectedItem.Number;
                ModernDialog.ShowMessage("Data Is Saved... !", "Information", MessageBoxButton.OK);
            }else
            {
                ModernDialog.ShowMessage("Data Not Saved... !", "Error", MessageBoxButton.OK);
            }
        }

        private bool SaveValidation()
        {
            if (this.Number > 0)
                return false;
            return true;
        }

        private bool AddItemValidated()
        {
            return true;
        }

        private customer _customer;
        private double _grand;
        private bool _isActive;

        public bool ProgressIsActive
        {
            get { return _isActive; }
            set
            {

                _isActive = value;
                OnPropertyChange("ProgressIsActive");
            }
        }

        public customer CustomerSelectedItem
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;

                OnPropertyChange("CustomerSelectedItem");
             
                LoadPenjualan(value);
            }
        }

        private async void LoadPenjualan(customer value)
        {
            if(value!=null)
            {
                ProgressIsActive = true;
                this.Details.Clear();
                SourceView.Refresh();
                var x = await MainVM.PenjualanCollection.GetPenjualanNotPaid(value.Id);

                if (x != null && x.Count > 0)
                {
                    var r = (from item in x
                             join c in MainVM.CityCollection.Source on item.Reciver.CityID equals c.Id
                             select new ModelsShared.Models.invoicedetail
                             {
                                 PenjualanId = item.Id,
                                 Etc = item.Etc,
                                 PackingCosts = item.PackingCosts,
                                 Tax = item.Tax,
                                 STT = item.STT,
                                 Via = item.PortType.ToString(),
                                 Reciver = item.Reciver.Name,
                                 Shiper = item.Shiper.Name,
                                 Pcs = item.Details.Count,
                                 Weight = item.Details.Sum(O => O.Weight),
                                 Price = item.Price,
                                 Total = item.Total,
                                 DoNumber = item.DoNumber, ChangeDate=item.ChangeDate, 
                                 Tujuan = c.CityName,
                                 PortType = item.PortType
                             }).ToList();

                    foreach (var item in r)
                    {

                        item.TotalAction += Item_TotalAction;
                        Details.Add(item);


                    }
                }
                SourceView.Refresh();
                ProgressIsActive = false;
            }
        }

        

        private async void Item_TotalAction()
        {
            await Task.Delay(200);
            GrandTotal = this.Details.Where(O=>O.IsSelected).Sum(O => O.Total);
        }
        

        public double GrandTotal
        {
            get {
                return _grand; }
            set { _grand = value;
                OnPropertyChange("GrandTotal");
            }
        }

     

        public MainWindowVM MainVM { get; private set; }
        public CollectionView SourceView { get; private set; }
        public CommandHandler AddItems { get; private set; }
        public CommandHandler PreviewManifest { get; private set; }
        public CommandHandler SaveAndPrint { get; private set; }
        public CommandHandler Save { get; private set; }
    }
}
