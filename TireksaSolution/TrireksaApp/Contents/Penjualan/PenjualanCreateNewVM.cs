using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ModelsShared.Models;
using FirstFloor.ModernUI.Windows.Controls;
using TrireksaApp.Common;
using System.Windows.Data;
using ModelsShared;
using System.Collections.ObjectModel;
using System.Windows;

namespace TrireksaApp.Contents.Penjualan
{
    public class PenjualanCreateVM : BaseNotifyProperty
    {
        public MainWindowVM MainVM { get; private set; }
        public AppConfiguration AppConfig { get; }
        public List<ModelsShared.Models.TypeOfWeight> TypeOfWeights { get; set; }
        public List<ModelsShared.Models.PortType> PortTypes { get; set; }
        public List<ModelsShared.Models.PayType> PayTypes { get; set; }
        public bool IsSearch { get; set; }

        //collection
        public ObservableCollection<customer> ShipersSource { get; set; }
        public ObservableCollection<customer> ReciversSource { get; set; }
        public ObservableCollection<customer> WillPaySource { get; set; }

        public PenjualanCreateVM()
        {
            this.MainVM = ResourcesBase.GetMainWindowViewModel();
            this.AppConfig = MainVM.SystemConfiguration;
            this.TypeOfWeights = ResourcesBase.GetEnumCollection<TypeOfWeight>();
            this.PortTypes = ResourcesBase.GetEnumCollection<PortType>();
            this.PayTypes = ResourcesBase.GetEnumCollection<PayType>();

            ShipersSource = new ObservableCollection<customer>(MainVM.CustomerCollection.Source);
            ReciversSource = new ObservableCollection<customer>(MainVM.CustomerCollection.Source);
            WillPaySource = new ObservableCollection<customer>(MainVM.CustomerCollection.Source);

            this.Shipers = (CollectionView)CollectionViewSource.GetDefaultView(ShipersSource);
            this.Recivers = (CollectionView)CollectionViewSource.GetDefaultView(ReciversSource);
            this.WillPays = (CollectionView)CollectionViewSource.GetDefaultView(WillPaySource);
            Shipers.Filter = ShiperFilter;
            Recivers.Filter = ReciverFilter;
            WillPays.Filter = WillPayFilter;
            Recivers.Refresh();
            Shipers.Refresh();
            WillPays.Refresh();

            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = SaveAction };
            Print = new CommandHandler { CanExecuteAction = x => PrintSelected != null, ExecuteAction = x => PrintAction() };
            PrintWithForm = new CommandHandler { CanExecuteAction = x => PrintSelected != null, ExecuteAction = x => PrintFormAction() };
            SaveAndPrint = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAndPrintAction() };
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancalAction() };
            Search = new CommandHandler { ExecuteAction = SearchAction };
            ChangeWeight = new CommandHandler { ExecuteAction = x => ChangeWeightAction() };
            PriceUpdate = new CommandHandler { CanExecuteAction = PriceUpdateValidation, ExecuteAction = PriceUpdateAction };
            CreateNewSTT();
        }

        private STTCreateModel stt;

        public STTCreateModel STTModel
        {
            get { return stt; }
            set
            {
                stt = value;
                OnPropertyChange("STTModel");
            }
        }


        private async void CreateNewSTT()
        {
            var STTnumber = await MainVM.PenjualanCollection.NewSTT();
            STTModel = new STTCreateModel(STTnumber);
            STTModel.ChangeDate = DateTime.Now;
            STTModel.OnChangePrice += STTModel_OnChangePrice;
        }

        private void STTModel_OnChangePrice(bool isOld)
        {
            if(!isOld)
                this.GetPrices(STTModel.CustomerIsPay);
        }
               

        #region Searchs
        public string ShiperSearch
        {
            get { return _shiperSearch; }
            set
            {
                _shiperSearch = value;
                Shipers.Refresh();
                OnPropertyChange("ShiperSearch");
            }
        }

        public string ReciverSearch
        {
            get { return _reciverSearch; }
            set
            {
                _reciverSearch = value;
                Recivers.Refresh();
                OnPropertyChange("ReciverSearch");
            }
        }

        public string CustomerWillPaySearch
        {
            get { return _CustomerWillPaySearch; }
            set
            {
                _CustomerWillPaySearch = value;
                if (value != string.Empty)
                    WillPays.Refresh();
                OnPropertyChange("CustomerWillPaySearch");
            }
        }
        #endregion

        #region Filers
        private bool CityFileter(object x)
        {
            var obj = (ModelsShared.Models.port)x;
            if (obj.PortType != PortType.None && STTModel != null)
            {
                return (obj.PortType.Equals(STTModel.PortType));
            }
            else
                return true;
        }
        private bool ShiperFilter(object x)
        {
            var scr = string.Empty;
            if (string.IsNullOrEmpty(ShiperSearch))
            {
                //ShiperSelected = null;
            }
            var obj = (customer)x;


            if (obj != null && !string.IsNullOrEmpty(ShiperSearch))
            {
                scr = ShiperSearch.ToUpper().Trim();
                var data = obj.Name.ToUpper();
                return data.Contains(scr);
            }
            return true;
        }
        private bool WillPayFilter(object x)
        {
            var scr = string.Empty;
            var obj = (customer)x;
            if (string.IsNullOrEmpty(CustomerWillPaySearch))
            {
                WillPaySelected = null;
            }

            if (obj != null && !string.IsNullOrEmpty(CustomerWillPaySearch))
            {
                scr = CustomerWillPaySearch.ToUpper();
                return obj.Name.ToUpper().Contains(scr);
            }
            return true;
        }

        private bool ReciverFilter(object x)
        {
            var scr = string.Empty;
            if (string.IsNullOrEmpty(ReciverSearch))
            {
                //ReciverSelected = null;
            }

            var obj = (ModelsShared.Models.customer)x;
            if (obj.CityID == AppConfig.WitoutCityId)
                return false;

            if (obj != null && !string.IsNullOrEmpty(ReciverSearch))
            {
                scr = ReciverSearch.ToUpper();
                return obj.Name.ToUpper().Contains(scr);
            }
            return true;
        }

        #endregion

        #region action

        private void PrintFormAction()
        {
            Helper.PrintPreviewNotaAction(PrintSelected);
        }


        private void CancalAction()
        {
            this.ClearForms();
            this.CreateNewSTT();


        }

        private async void SearchAction(object obj)
        {
            try
            {
                //ClearForms();
                var stt = Convert.ToInt32(obj);
                var result = await MainVM.PenjualanCollection.GetItemBySTT(stt);
                if (result != null)
                {
                    var newmodel = new STTCreateModel(result);
                    STTModel = newmodel;
                    STTModel.OnChangePrice += STTModel_OnChangePrice;
                }
                   
                else
                {
                    STTModel = new STTCreateModel(stt);
                    throw new SystemException("Data Tidak Ditemukan !");
                }
                  
                await Task.Delay(1000);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }

        }

       

        private void SaveAndPrintAction()
        {
            try
            {
                if (Save.CanExecute(null))
                    Save.Execute(true);


            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }

        private void PrintAction()
        {
            Helper.PrintNotaAction(PrintSelected);
        }

        private async void SaveAction(object param)
        {
            try
            {
                await STTModel.Save();
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
      

        private void PriceUpdateAction(object obj)
        {

            try
            {
                this.PriceIsSync = true;
               // var data = GetPriceObject();
                var context = new PricesContext(STTModel);
                context.UpdatePrice(STTModel.Price);
            }
            catch (Exception ex)
            {

                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }
            this.PriceIsSync = false;
        }

        private void ChangeWeightAction()
        {
            if (STTModel.Details == null)
                STTModel.Details = new List<colly>();
            using (var vm = new Contents.Penjualan.AddCollyVM(STTModel.TypeOfWeight, STTModel.Details))
            {
                if (STTModel.TypeOfWeight != TypeOfWeight.None)
                {
                    var form = new Contents.Penjualan.AddColly();
                    form.DataContext = vm;
                    form.ShowDialog();

                }
            }
            // this.Details = vm.Source;

            if (STTModel.Details != null && STTModel.Details.Count > 0)
            {
                STTModel.SetTotal();
                DetailsIsEmpty = true;
            }
        }

        #endregion

        #region Validate

        private bool PriceUpdateValidation(object obj)
        {
            if (STTModel!=null && STTModel.Shiper != null && STTModel.Reciver != null && STTModel.Price > 0 && STTModel.PortType != PortType.None &&
                 STTModel.CustomerIsPay != CustomerIsPay.None && STTModel.PayType != PayType.None)
            {
                return true;
            }
            return false;
        }


        private bool SetWeightValidation()
        {
            if (STTModel.ShiperID > 0 && STTModel.ReciverID > 0)
            {
                return true;
            }
            else
                return false;
        }

        private bool SaveValidation()
        {
            if (STTModel != null)
                return STTModel.IsValid;
            return false;

        }
        #endregion

        #region Fields
        private bool _detailsIsempty;
        private bool _priceIsSync;
        private string _shiperSearch;
        private string _reciverSearch;
        private string _CustomerWillPaySearch;
        private customer _WillPaySelected;
        #endregion

        #region Properties
        public CommandHandler Save { get; set; }
        public CommandHandler SaveAndPrint { get; set; }
        public CommandHandler Print { get; set; }
        public CommandHandler PrintWithForm { get; }
        public CommandHandler Cancel { get; set; }
        public CommandHandler Search { get; set; }
        public CollectionView OriginPorts { get; set; }
        public CollectionView DestinationPorts { get; set; }
        public CollectionView Recivers { get; private set; }
        public CollectionView Shipers { get; private set; }
        public CollectionView WillPays { get; private set; }
        public CommandHandler ChangeWeight { get; private set; }
        public CommandHandler PriceUpdate { get; private set; }

        public penjualan PrintSelected { get; private set; }
       
        public customer WillPaySelected
        {
            get { return _WillPaySelected; }
            set
            {
                _WillPaySelected = value;
                if (value != null)
                {
                    STTModel.CustomerIdIsPay = value.Id;
                }
                OnPropertyChange("WillPaySelected");
            }
        }

       


        #endregion


        internal void LoadShiper()
        {
            ShipersSource.Clear();
            foreach (var item in MainVM.CustomerCollection.Source)
            {
                ShipersSource.Add(item);
            }
            Shipers.Refresh();
        }

        internal void LoadReciver()
        {
            ReciversSource.Clear();
            foreach (var item in MainVM.CustomerCollection.Source)
            {
                ReciversSource.Add(item);
            }
            Recivers.Refresh();
        }



        public bool PriceIsSync
        {
            get
            {
                return _priceIsSync;
            }
            set
            {
                _priceIsSync = value;
                OnPropertyChange("PriceIsSync");
            }
        }



        internal void SetWeight(object sender)
        {
            var value = (TypeOfWeight)sender;

            if (STTModel.Details == null)
                STTModel.Details = new List<colly>();

            using (var vm = new Contents.Penjualan.AddCollyVM(value, STTModel.Details))
            {

                if (value != TypeOfWeight.None)
                {
                    var form = new Contents.Penjualan.AddColly();
                    form.DataContext = vm;

                    var dlg = new ModernDialog
                    {
                        Title = "Add Item",
                        Content = form
                    };
                    dlg.ShowDialog();

                }
            }
            STTModel.SetTotal();
            // this.Details = vm.Source;

            if (STTModel.Details != null && STTModel.Details.Count > 0)
            {
                STTModel.TypeOfWeight = value;
                DetailsIsEmpty = true;
            }
            STTModel.TypeOfWeight = value;
        }

        public bool DetailsIsEmpty
        {
            get
            {
                if (STTModel.TypeOfWeight == TypeOfWeight.None)
                {
                    _detailsIsempty = false;
                }

                return _detailsIsempty;
            }
            set
            {
                _detailsIsempty = value;
                OnPropertyChange("DetailsIsEmpty");
            }
        }



        private void ClearForms()
        {
            CustomerWillPaySearch = string.Empty;
            ShiperSearch = string.Empty;
            ReciverSearch = string.Empty;
            DetailsIsEmpty = false;
        }

        private void GetPrices(CustomerIsPay value)
        {
            if (STTModel != null)
            {
                var pricesContext = new PricesContext(STTModel);
                pricesContext.GetPrices().ContinueWith(GetPricesAsyncHandler);
            }
        }

        private async void GetPricesAsyncHandler(Task<double> obj)
        {
            var price = await obj;
            if ( price > 0)
            {
                STTModel.Price = price;
            }
            else
            {
                STTModel.Price = 0;
            }

        }
    }


    public delegate void ChangePrice (bool isOld);
    public class STTCreateModel : penjualan, IDataErrorInfo
    {
        public event ChangePrice OnChangePrice;
        public STTCreateModel(int sTTnumber)
        {
            STT = sTTnumber;

        }
        private bool IsOld { get; set; }

        public STTCreateModel(penjualan result)
        {
            IsOld = true;
            if (result != null)
            {
                
                Actived = result.Actived;
                ChangeDate = result.ChangeDate;
                CityID = result.CityID;
                Content = result.Content;
                CustomerIdIsPay = result.CustomerIdIsPay;
                CustomerIsPay = result.CustomerIsPay;
                DeliveryStatus = result.DeliveryStatus;
                Details = result.Details;
                DoNumber = result.DoNumber;
                Etc = result.Etc;
                Id = result.Id;
                Note = result.Note;
                PackingCosts = result.PackingCosts;
                PayType = result.PayType;
                Pcs = result.Pcs;
                PortType = result.PortType;
                Price = result.Price;
                ShiperID = result.ShiperID;
                ReciverID = result.ReciverID;
                Shiper = result.Shiper;
                Reciver = result.Reciver;
                STT = result.STT;
                TypeOfWeight = result.TypeOfWeight;
                Tax = result.Tax;
                UpdateDate = result.UpdateDate;
                UserID = result.UserID;
            }
        }

        internal async Task Save(bool print = false)
        {
            var MainVM = ResourcesBase.GetMainWindowViewModel();
            try
            {
                this.UpdateDate = DateTime.Now;
                if (this.Id <= 0)
                {
                    this.Actived = true;
                    var result = await MainVM.PenjualanCollection.Add(this);
                    if (result != null)
                    {
                        ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);

                    }
                    else
                    {
                        throw new SystemException("Data Not Saved !");
                    }
                }
                else
                {
                    if (Id > 0 && Details != null && Details.Count > 0)
                    {
                        foreach (var dataItem in Details)
                        {
                            dataItem.PenjualanId = Id;
                        }
                    }

                    var result = await MainVM.PenjualanCollection.Update(Id, this);
                    if (result)
                    {

                        MainVM.PenjualanCollection.SourceView.Refresh();
                        ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);
                    }
                    else
                    {
                        throw new SystemException("Data Not Saved !");
                    }
                }

            }
            catch (Exception ex)
            {

                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }

        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ShiperID")
                {
                    if (ShiperID <= 0)
                        return "Select Shiper";
                    else if (ShiperID == ReciverID)
                        return "Shiper & Reciver Same";

                    return null;
                }

                if (columnName == "ReciverID")
                {
                    if (ReciverID <= 0)
                        return "Select Reciver";
                    else if (ShiperID == ReciverID)
                        return "Shiper & Reciver Same";

                    return null;
                }


                if (columnName == "CustomerIdIsPay")
                {
                    if (CustomerIdIsPay <= 0)
                        return "Select Customer";
                    return null;
                }

                if (columnName == "Price")
                    return (Price <= 0) ? "Price Requerid" : null;

                if (columnName == "TypeOfWeight")
                {
                    return (TypeOfWeight == TypeOfWeight.None) ? "Select Type" : null;
                }


                if (columnName == "PayType")
                    return (PayType == PayType.None) ? "Select a Payment Type" : null;


                if (columnName == "PortType")
                    return (PortType == PortType.None) ? "Select Port Type" : null;


                if (columnName == "CityID")
                    return (CityID <= 0) ? "Select Origin Port" : null;


                if (columnName == "CustomerIsPay")
                    return (CustomerIsPay == CustomerIsPay.None) ? "Who Will Pay ?" : null;


                if (columnName == "Content")
                    return string.IsNullOrEmpty(Content) ? "Content Required ?" : null;






                return null;
            }
        }

        public string Error
        {
            get; set;
        }
        public bool IsValid
        {
            get
            {

                if (this.CustomerIsPay == CustomerIsPay.None)
                    return false;

                if (this.CustomerIsPay == CustomerIsPay.Other && (CustomerIdIsPay == ShiperID || CustomerIdIsPay == ReciverID))
                    return false;

                if (this.CityID == 0 && this.Price == 0)
                    return false;
                if (this.Details == null || this.Details.Count == 0)
                    return false;

                if (PortType == PortType.None)
                    return false;
                if (this.ReciverID == 0 && this.ShiperID == 0)
                    return false;
                if (this.TypeOfWeight == TypeOfWeight.None)
                    return false;
                if (this.PayType == PayType.None)
                    return false;
                if (string.IsNullOrEmpty(Content))
                    return false;
                if (this.Price <= 0)
                    return false;


                return true;



            }
        }

        public override CustomerIsPay CustomerIsPay {
            get => base.CustomerIsPay;
            set {
                base.CustomerIsPay = value;
                if (OnChangePrice != null)
                    OnChangePrice(IsOld);
                ChangeOldStatus();
            } 
        }

        private async void ChangeOldStatus()
        {
            await Task.Delay(2000);
            if (IsOld)
                IsOld = false;
        }

        public override PayType PayType { get => base.PayType;
            set { base.PayType = value;
                if (OnChangePrice != null)
                    OnChangePrice(IsOld);
                ChangeOldStatus();
            }
        }

        public override PortType PortType { get => base.PortType;
            set
            {
                base.PortType= value;
                if (OnChangePrice != null)
                    OnChangePrice(IsOld);
                ChangeOldStatus();
            }
        }
    }


    public class PricesContext
    {
        private STTCreateModel sTTModel;

        public PricesContext(STTCreateModel sTTModel)
        {
            this.sTTModel = sTTModel;
        }

        public bool PricesParamaterValid
        {
            get
            {
                if (sTTModel.Reciver!=null && sTTModel.CustomerIdIsPay>0 && 
                    sTTModel.PayType != PayType.None && sTTModel.PortType != PortType.None && sTTModel.Reciver.CityID>0)
                    return true;
                else
                    return false;
            }


        }

        public Task<double> GetPrices()
        {
            if (PricesParamaterValid)
            {
                var prices = GetModel();
                var context = ResourcesBase.GetMainWindowViewModel().PricesCollection;
                return context.GetPricesByCustomer(prices);
            }
            return Task.FromResult((double)0);
        }

        private Prices GetModel()
        {
            var prices = new Prices {  From= sTTModel.Shiper.CityID, PayType= sTTModel.PayType, PortType= sTTModel .PortType,
             Price=sTTModel.Price, ShiperId= sTTModel.CustomerIdIsPay, To= sTTModel.ReciverID};
            return prices;
        }

        public async void UpdatePrice(double price)
        {
            if (PricesParamaterValid)
            {
                var prices = GetModel();
                prices.Price = price;
                var context = ResourcesBase.GetMainWindowViewModel().PricesCollection;
                await context.SetPrices(prices);
            }
        }
    }
}