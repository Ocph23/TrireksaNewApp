using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Customer
{
    public class CustomerCreateVM : ModelsShared.Models.customer, IDataErrorInfo
    {
        private bool _progress;

        public List<ModelsShared.Models.CustomerType> CustomersTypes { get; set; }
        public CustomerCreateVM()
        {
            MainVM = ResourcesBase.GetMainWindowViewModel();
            this.CustomersTypes = new List<ModelsShared.Models.CustomerType>();
            CustomersTypes.Add(ModelsShared.Models.CustomerType.Organization);
            CustomersTypes.Add(ModelsShared.Models.CustomerType.Person);
            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
        }

        private bool SaveValidation()
        {
            if (this.IsValid )
                return true;
            else
                return false;
        }

        private async void SaveAction()
        {
            this.ProgressIsActive = true;
            var item = new ModelsShared.Models.customer
            {
                Address = this.Address,
                CityID = this.CityID,
                ContactName = this.ContactName,
                CustomerType = this.CustomerType,
                Email = this.Email,
                Handphone = this.Handphone,
                Id = this.Id,
                Name = this.Name,
                Phone1 = this.Phone1,
                Phone2 = this.Phone2
            };

            bool isSaved = await MainVM.CustomerCollection.Add(item);
            if (isSaved)
            {
                MainVM.CustomerCollection.SourceView.Refresh();
                ModernDialog.ShowMessage("Data Is Saved !", "Information", System.Windows.MessageBoxButton.OK);
                this.Clear();
            }
            else
            {
                ModernDialog.ShowMessage("Data Not Saved !", "Error", System.Windows.MessageBoxButton.OK);
            }
            this.ProgressIsActive = false;
        }

        private void Clear()
        {
            this.Address = string.Empty;
            this.ContactName = string.Empty;
            this.CustomerType = ModelsShared.Models.CustomerType.None;
            this.Email = string.Empty;
            this.Handphone = string.Empty;
            this.Id = 0;
            this.CityID = 0;
            this.Name = string.Empty;
            this.Phone1 = string.Empty;
            this.Phone2 = string.Empty;
            
        }
        public bool ProgressIsActive
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChange("ProgressIsActive");
            }
        }
        public CommandHandler Save { get; set; }
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Address")
                {
                    return string.IsNullOrEmpty(this.Address) ? "Required Value" : null;
                }

                if (columnName == "CustomerType")
                {
                    return this.CustomerType== ModelsShared.Models.CustomerType.None ? "Select Type Of Customer" : null;
                }

                if (columnName == "Name")
                {
                    return string.IsNullOrEmpty(this.Name) ? "Select Type Of Customer" : null;
                }

                //if (columnName == "Phone1")
                //{
                //    return string.IsNullOrEmpty(this.Phone1) ? "Select Type Of Customer" : null;
                //}


                //if (columnName == "Handphone")
                //{
                //    return string.IsNullOrEmpty(this.Handphone) ? "Select Type Of Customer" : null;
                //}

                if (columnName == "CityID")
                {
                    return (this.CityID==0) ? "Select Type Of Customer" : null;
                }

                if (columnName == "ContactName")
                {
                    return string.IsNullOrEmpty(this.ContactName) ? "Select Type Of Customer" : null;
                }


                //if (columnName == "Email")
                //{
                //    return EmailValidation() ? string.Format("{0}","Email Error: (youremail@example.com)") : null;
                //}





                return null;
            }
        }

        //private bool EmailValidation()
        //{
        //    if (!string.IsNullOrEmpty(this.Email))
        //    {
        //        if (this.Email.Contains("@") && this.Email.Contains("."))
        //        {
        //            return false;
        //        }else
        //            return true;
        //    }else
        //        return true;
        //}

        public string Error
        {
            get
            {
                return null;
            }
        }

        public MainWindowVM MainVM { get; private set; }
    }
}
