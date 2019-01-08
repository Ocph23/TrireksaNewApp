using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Customer
{
    public class CustomerEditVM : ModelsShared.Models.customer, IDataErrorInfo
    {
        public CollectionView CustomersTypes { get; set; }
        public CustomerEditVM(ModelsShared.Models.customer item)
        {
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            var t = Common.ResourcesBase.GetEnumCollection<ModelsShared.Models.CustomerType>();
            CustomersTypes = (CollectionView)CollectionViewSource.GetDefaultView(t);
         
            Task.Factory.StartNew(() => SetItemToEdit(item));
        }

        private void SetItemToEdit(ModelsShared.Models.customer item)
        {
            this.Address = item.Address;
            this.ContactName = item.ContactName;
            this.CustomerType = item.CustomerType;
            this.Email = item.Email;
            this.Handphone = item.Handphone;
            this.Id = item.Id;
            this.Name = item.Name;
            this.Phone1 = item.Phone1;
            this.Phone2 = item.Phone2;
            this.CityID = item.CityID;
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
                    return this.CustomerType == ModelsShared.Models.CustomerType.None? "Select Type Of Customer" : null;
                }

                if (columnName == "Name")
                {
                    return string.IsNullOrEmpty(this.Name) ? "Select Type Of Customer" : null;
                }

                if (columnName == "Email" && !string.IsNullOrEmpty(this.Email))
                {
                    return EmailValidation() ? string.Format("{0}", "Email Error: (youremail@example.com)") : null;
                }
                return null;
            }
        }

        private bool EmailValidation()
        {
            if (!string.IsNullOrEmpty(this.Email))
            {
                if (!this.Email.Contains("@") || !this.Email.Contains("."))
                {
                    return true;
                }



                return false;
            }
            else
                return false;
        }


        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public MainWindowVM MainVM { get; private set; }
    }
}
