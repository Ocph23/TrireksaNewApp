using System;
using ModelsShared.Models;
using TrireksaApp.Common;
using System.ComponentModel;

namespace TrireksaApp.Contents.Users
{
    public  class ChangeProfileVM:ModelsShared.Models.userprofile,IDataErrorInfo
    {
        public  ChangeProfileVM()
        {
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            Save = new CommandHandler { CanExecuteAction = x => SaveValidate(), ExecuteAction = x => SaveAction() };
            GetProfile();

            
        }

        private async void GetProfile()
        {

            userprofile result = await MainVM.UserProfileCollections.GetProfile();
            if (result != null)
            {

                if (result == null)
                {
                    this.IsNew = true;
                }
                else
                {

                    this.UserId = result.UserId;
                    this.FirstName = result.FirstName;
                    this.LastName = result.LastName;
                    this.Email = result.Email;
                    this.UserId = result.UserId;
                    this.PhoneNumber = result.PhoneNumber;
                    this.Address = result.Address;
                    this.Photo = result.Photo;
                }
            }




        }

        private async void SaveAction()
        {
            var item = new ModelsShared.Models.userprofile
            { 
                Address = this.Address,
                FirstName = this.FirstName,
                LastName = this.LastName,
                UserId = this.UserId,
                Photo = this.Photo,
                UserName = this.UserName
            };

            bool result = false;
            if (IsNew)
            {
               result = await MainVM.UserProfileCollections.Add(item);
            }
            else
            {
                result =await MainVM.UserProfileCollections.Update(this.UserId, item);
            }

            if(result)
            {
                ResourcesBase.ShowMessage("Data Berhasil Disimpan");
            }else
                ResourcesBase.ShowMessageError("Data Tidak Tersimpan");

        }

        private bool SaveValidate()
        {
            return true;
        }

        public CommandHandler Save { get; set; }

        public bool IsNew { get; private set; }
        public MainWindowVM MainVM { get; private set; }

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
                return null;
            }
        }
    }


}
