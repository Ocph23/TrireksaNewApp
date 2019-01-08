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
    public class PenjualanCreateVM : ModelsShared.Models.penjualan, IDataErrorInfo
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
            ReciversSource= new ObservableCollection<customer>(MainVM.CustomerCollection.Source);
            WillPaySource = new ObservableCollection<customer>(MainVM.CustomerCollection.Source);

            this.Shipers =(CollectionView)CollectionViewSource.GetDefaultView(ShipersSource);
            this.Recivers = (CollectionView)CollectionViewSource.GetDefaultView(ReciversSource);
             this.WillPays = (CollectionView)CollectionViewSource.GetDefaultView(WillPaySource);
            Shipers.Filter = ShiperFilter;
            Recivers.Filter = ReciverFilter;
            WillPays.Filter = WillPayFilter;
            Recivers.Refresh();
            Shipers.Refresh();
            WillPays.Refresh();


            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = SaveAction };
            Print = new CommandHandler { CanExecuteAction = x =>PrintSelected!=null, ExecuteAction = x => PrintAction() };
            PrintWithForm = new CommandHandler { CanExecuteAction = x => PrintSelected != null, ExecuteAction = x => PrintFormAction() };
            SaveAndPrint = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAndPrintAction() };
            Cancel= new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancalAction() };
            Search = new CommandHandler { CanExecuteAction = x =>(this.STT>0), ExecuteAction =SearchAction };
            ChangeWeight= new CommandHandler { CanExecuteAction = x => (this.TypeOfWeight!= TypeOfWeight.None), ExecuteAction = x => ChangeWeightAction() };
            PriceUpdate = new CommandHandler { CanExecuteAction = PriceUpdateValidation, ExecuteAction = PriceUpdateAction };
            CreateNewSTT();
           
            
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
            if (obj.PortType != PortType.None)
            {

                return (obj.PortType.Equals(this.PortType));
            }
            else
                return true;
        }
        private bool ShiperFilter(object x)
        {
            var scr = string.Empty;
            var obj = (ModelsShared.Models.customer)x;
            if (obj.CityID != AppConfig.EliminateCityId)
                return false;

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
            var obj = (ModelsShared.Models.customer)x;


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
            Helper.PrintNotaWithFormAction(PrintSelected);
        }


        private void CancalAction()
        {
            this.ClearForms();
            this.CreateNewSTT();


        }

        private async void SearchAction(object obj)
        {
            this.IsSearch = false;
            ClearForms();
            var stt = Convert.ToInt32(obj);
            var result = await MainVM.PenjualanCollection.GetItemBySTT(stt);
            SetSTT(result);
            await Task.Delay(2000);
            PrintSelected = result;
            PrintSelected.Reciver = ReciverSelected;
            PrintSelected.Shiper = ShiperSelected;

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
            bool print = false;
            if (param != null)
                print = (bool)param;

            penjualan item = new penjualan
            {
                ChangeDate = this.ChangeDate,
                CustomerIdIsPay = this.CustomerIdIsPay,
                CustomerIsPay = this.CustomerIsPay,
                Details = this.Details,
                Etc = this.Etc,
                Note = this.Note,
                CityID = this.ReciverSelected.CityID,
                PackingCosts = this.PackingCosts,
                PayType = this.PayType,
                PortType = this.PortType,
                Price = this.Price,
                ReciverID = this.ReciverID,
                ShiperID = this.ShiperID,
                STT = this.STT,
                Tax = this.Tax,
                Total = this.Total,
                TypeOfWeight = this.TypeOfWeight,
                UserID = this.UserID,
                Content = this.Content,
                DoNumber = this.DoNumber,
                Id = this.Id,
                Actived = this.Actived,
                UpdateDate = DateTime.Now
            };

            try
            {
                if (!IsSearch)
                {
                    item.Actived = true;
                    var result = await MainVM.PenjualanCollection.Add(item);
                    if (result != null)
                    {
                        result.Shiper = ShiperSelected;
                        result.Reciver = ReciverSelected;
                        ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);
                        Common.ResourcesBase.GetSMSClient().SendSMSPenjualan(item, ShiperSelected, ReciverSelected);
                        CancalAction();
                    }
                    else
                    {
                        throw new SystemException("Data Not Saved !");
                    }
                }
                else
                {
                    if (item != null && item.Details != null && item.Details.Count > 0)
                    {
                        foreach (var dataItem in item.Details)
                        {
                            dataItem.PenjualanId = item.Id;
                        }
                    }

                    var result = await MainVM.PenjualanCollection.Update(item.Id, item);
                    if (result)
                    {
                        item.Shiper = ShiperSelected;
                        item.Reciver = ReciverSelected;

                        MainVM.PenjualanCollection.SourceView.Refresh();
                        ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);
                        if (print)
                        {
                            PrintSelected = item;
                            Helper.PrintNotaAction(PrintSelected);
                        }
                        CancalAction();
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

        private Tuple<customer,customer> GetPriceObject()
        {
            var value = this.CustomerIsPay;
            customer shiper = ShiperSelected;
            customer reciever = ReciverSelected;
            if (value == CustomerIsPay.Reciver)
            {
                shiper = ReciverSelected;
                reciever = ShiperSelected;
            }
            else if (value == CustomerIsPay.Other)
            {
                shiper = WillPaySelected;
                reciever = ReciverSelected;
            }

            return new Tuple<customer, customer>(shiper, reciever);
        }


        private void PriceUpdateAction(object obj)
        {
           
            try
            {
                this.PriceIsSync = true;
                var data = GetPriceObject();
                var context = new PricesContext(data.Item1, data.Item2, PortType, PayType);
                context.UpdatePrice(Price);
            }
            catch (Exception ex)
            {

                ModernDialog.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK);
            }
            this.PriceIsSync = false;
        }

        private void ChangeWeightAction()
        {
            if (Details == null)
                Details = new List<colly>();
            using (var vm = new Contents.Penjualan.AddCollyVM(this.TypeOfWeight, this.Details))
            {
                if (this.TypeOfWeight != TypeOfWeight.None)
                {
                    var form = new Contents.Penjualan.AddColly();
                    form.DataContext = vm;
                    form.ShowDialog();

                }
            }
            // this.Details = vm.Source;

            if (this.Details != null && this.Details.Count > 0)
            {
                this.SetTotal();
                DetailsIsEmpty = true;
            }
        }

        #endregion

        #region Validate

        private bool PriceUpdateValidation(object obj)
        {
            if (ShiperSelected != null && ReciverSelected != null && Price > 0 && PortType != PortType.None &&
                 this.CustomerIsPay != CustomerIsPay.None && PayType != PayType.None)
            {
                return true;
            }
            return false;
        }

      
        private bool SetWeightValidation()
        {
            if (this.ShiperID > 0 && this.ReciverID > 0)
            {
                return true;
            }
            else
                return false;
        }

        private bool SaveValidation()
        {

            if (this.CustomerIsPay == CustomerIsPay.None)
                return false;
            if (this.CustomerIsPay == CustomerIsPay.Other && WillPaySelected == null)
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


      

        #endregion



        #region Fields
        private customer _shiperSelected;
        private customer _reciverSelected;
        private bool _detailsIsempty;
        private bool _priceIsSync;
        private string _shiperToolTip;
        private string reciverToolTip;
        private string _shiperSearch;
        private string _reciverSearch;
        private string _CustomerWillPaySearch;
        private customer _WillPaySelected;
        private Visibility _otherVisible;
        #endregion

        #region Properties
        public CommandHandler Save { get; set; }
        public CommandHandler SaveAndPrint { get; set; }
        public CommandHandler Print { get; set; }
        public CommandHandler PrintWithForm { get; }
        public CommandHandler Cancel { get; set; }
        public CommandHandler Search { get; set; }
        public string ShiperToolTip
        {
            get
            {
                return _shiperToolTip;
            }
            set
            {
                _shiperToolTip = value;
                OnPropertyChange("ShiperToolTip");
            }
        }
        public string ReciverToolTip
        {
            get
            {
                return reciverToolTip;
            }
            set
            {
                reciverToolTip = value;
                OnPropertyChange("ReciverToolTip");
            }
        }
        public CollectionView OriginPorts { get; set; }
        public CollectionView DestinationPorts { get; set; }
        public CollectionView Recivers { get; private set; }
        public CollectionView Shipers { get; private set; }
        public CollectionView WillPays { get; private set; }
        public CommandHandler ChangeWeight { get; private set; }
        public CommandHandler PriceUpdate { get; private set; }

        public penjualan PrintSelected { get; private set; }
        public Visibility OtherVisible
        {
            get { return _otherVisible; }
            set
            {
                _otherVisible = value;
                OnPropertyChange("OtherVisible");
            }

        }
        public customer ShiperSelected
        {
            get { return _shiperSelected; }
            set
            {
                _shiperSelected = value;
                if (value != null)
                {
                    ShiperToolTip = string.Format("{0}\r{1}", value.Address, MainVM.CityCollection.Source.Where(O => O.Id == value.CityID).FirstOrDefault().CityName);
                    this.GetPrices(this.CustomerIsPay);
                }
                else
                {
                    ShiperToolTip = string.Empty;
                }
                OnPropertyChange("ShiperSelected");
            }
        }
        public customer ReciverSelected
        {
            get { return _reciverSelected; }
            set
            {
                _reciverSelected = value;
                if (value != null)
                {
                    this.CityID = value.CityID;
                    ReciverToolTip = string.Format("{0}\r{1}", value.Address, MainVM.CityCollection.Source.Where(O => O.Id == value.CityID).FirstOrDefault().CityName);


                    this.GetPrices(this.CustomerIsPay);
                }
                else
                {

                    ReciverToolTip = string.Empty;
                }

                OnPropertyChange("ReciverSelected");
            }
        }


        public customer WillPaySelected
        {
            get { return _WillPaySelected; }
            set
            {
                _WillPaySelected = value;
                if (value != null)
                {
                    this.CustomerIdIsPay = value.Id;
                    this.GetPrices(this.CustomerIsPay);
                }
                else
                {

                    ReciverToolTip = string.Empty;
                }

                OnPropertyChange("WillPaySelected");
            }
        }


        public override CustomerIsPay CustomerIsPay
        {
            get
            {
                return base.CustomerIsPay;
            }

            set
            {

                base.CustomerIsPay = value;
                if(value!= CustomerIsPay.None)
                GetPrices(value);

                if (value == CustomerIsPay.Other)
                {
                    this.OtherVisible = Visibility.Visible;
                }
                else
                {
                    this.OtherVisible = Visibility.Collapsed;
                    this.CustomerWillPaySearch = string.Empty;
                }
                OnPropertyChange("CustomerIsPay");

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

        private async void CreateNewSTT()
        {
            IsSearch = false;
           STT= await MainVM.PenjualanCollection.NewSTT();
            this.ChangeDate = DateTime.Now;
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

            if (this.Details == null)
                this.Details = new List<colly>();

            using (var vm = new Contents.Penjualan.AddCollyVM(value, Details))
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
            this.SetTotal();
            // this.Details = vm.Source;

            if (this.Details != null && this.Details.Count > 0)
            {
                base.TypeOfWeight = value;
                DetailsIsEmpty = true;
            }
            base.TypeOfWeight = value;
        }

        public bool DetailsIsEmpty
        {
            get
            {
                if (this.TypeOfWeight == TypeOfWeight.None)
                {
                   _detailsIsempty=false;
                }

                return _detailsIsempty;
            }
            set
            {
                _detailsIsempty = value;
                OnPropertyChange("DetailsIsEmpty");
            }
        }


        private async void SetSTT(penjualan result)
        {
            await Task.Delay(50);
            if (result != null)
            {
                this.Actived = result.Actived; 
                this.IsSearch = true;
                STT = result.STT;
                ShiperID = result.ShiperID;
                ReciverID = result.ReciverID;
                Shiper = result.Shiper;
    
                PortTypeView = result.PortType;
                TypeOfWeight = result.TypeOfWeight;
                ChangeDate = result.ChangeDate;
                CustomerIsPay = result.CustomerIsPay;
                Details = result.Details;
                Etc = result.Etc;
                CityID = result.CityID;
                PackingCosts = result.PackingCosts;
                PayTypeView = result.PayType;
                
                Tax = result.Tax;
                CustomerIdIsPay = result.CustomerIdIsPay;
                UpdateDate = result.UpdateDate;
                UserID = result.UserID;
    
                DoNumber = result.DoNumber;
                Content = result.Content;
                Note = result.Note;
                Id = result.Id;
        
                DeliveryStatus = result.DeliveryStatus;
                Price = result.Price;
            }else if(IsSearch)
            {
           
                CancalAction();
            }
            else
            {
                ModernDialog.ShowMessage("Data Tidak Diitemukan", "Error", System.Windows.MessageBoxButton.OK);
            }

        }

        private void ClearForms()
        {
            this.Id = 0;
            this.CustomerIsPay = CustomerIsPay.None;
            this.Details = null;
            this.Etc = 0;
            this.CityID = 0;
            this.PackingCosts = 0;
            this.PayTypeView = PayType.None;
            this.PortTypeView = PortType.None;
            this.Price = 0;
            this.ReciverID = 0;
            this.ShiperID = 0;
            this.Weight = 0;
            this.Pcs = 0;
            this.STT = 0;
            this.Tax = 0;
            this.Total = 0;
            this.TypeOfWeight = TypeOfWeight.None;
            this.IsSearch = false;
            this.DoNumber = string.Empty;
            this.Note = string.Empty;
            this.Content = string.Empty;
            this.Shiper = null;
            this.Reciver = null;
            this.Actived = true;
            ShiperSearch = string.Empty;
            ReciverSearch= string.Empty;
            CustomerWillPaySearch = string.Empty;
            DetailsIsEmpty = false;
            OtherVisible = Visibility.Collapsed;
        }

        private  void GetPrices(CustomerIsPay value)
        {

            var obj = GetPriceObject();

            var pricesContext = new PricesContext(obj.Item1, obj.Item2, PortType,PayType);

              pricesContext.GetPrices().ContinueWith(GetPricesAsyncHandler); ;
           
        }

        private async void GetPricesAsyncHandler(Task<double> obj)
        {
            var price = await obj;
            if (price != 0 && price > 0)
            {
                this.Price = price;
            }
            else
            {
                this.Price = 0;
            }

        }


        public  PayType PayTypeView
        {
            get
            {
                return base.PayType;
            }

            set
            {
               
                base.PayType = value;
                OnPropertyChange("PayTypeView");
                if (this.PayType == PayType.COD)
                {
                    this.CustomerIsPay = CustomerIsPay.Reciver;
                }
                else
                {
                    this.CustomerIsPay = CustomerIsPay.Shiper;
                }

                this.GetPrices(this.CustomerIsPay);

            }
        }

        public  PortType PortTypeView
        {
            get
            {
                return base.PortType;
            }

            set
            {
               
                base.PortType = value;
                OnPropertyChange("PortTypeView");
                this.GetPrices(this.CustomerIsPay);

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

                    return  null;
                }

                if (columnName == "Price")
                    return (Price <= 0) ? "Price Requerid" : null;

                if (columnName == "TypeOfWeight")
                {
                    return (TypeOfWeight== TypeOfWeight.None) ? "Select Type" : null;
                }


                if (columnName == "PayType")
                    return (PayType== PayType.None) ? "Select a Payment Type" : null;


                if (columnName == "PortType")
                    return (PortType== PortType.None) ? "Select Port Type" : null;


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
            get;set;
        }



        //ToolTip
       
    }



    public class PricesContext
    {
        public PricesContext(customer shiper,customer reciever, PortType portType, PayType payType)
        {
            this.Shiper = shiper;
            this.Reciever = reciever;
            this.PortType = portType;
            this.PayType = payType;
        }

        public bool PricesParamaterValid
        {
            get
            {
                if (Shiper!= null && Reciever!= null && PayType != PayType.None && PortType != PortType.None)
                    return true;
                else
                    return false;
            }


        }


        public Task<double> GetPrices()
        {
            if(PricesParamaterValid)
            {
                var prices = GetModel();
                var context = ResourcesBase.GetMainWindowViewModel().PricesCollection;
                return context.GetPricesByCustomer(prices);
            }
            return Task.FromResult((double)0);
        }

        private Prices GetModel()
        {
           var prices= new Prices();
            prices.ShiperId = Shiper.Id;
            prices.ReciverId = Reciever.Id;
            prices.PortType = PortType;
            prices.From = Shiper.CityID;
            prices.To = Reciever.CityID;
            prices.PayType = PayType;
            return prices;
        }

        internal async void UpdatePrice(double price)
        {
            if (PricesParamaterValid)
            {
                var prices = GetModel();
                prices.Price = price;
                var context = ResourcesBase.GetMainWindowViewModel().PricesCollection;
                await context.SetPrices(prices);
            }
        }

        public customer Shiper { get; }
        public customer Reciever { get; }
        public PayType PayType { get; }
        public PortType PortType { get; }
    }

}
