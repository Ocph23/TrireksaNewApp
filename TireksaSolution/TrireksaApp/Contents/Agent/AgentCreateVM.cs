using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Agent
{
    public class AgentCreateVM : ModelsShared.Models.agent, IDataErrorInfo
    {
        private Common.ErrorHandler errors = new Common.ErrorHandler();
        private MainWindowVM main = Common.ResourcesBase.GetMainWindowViewModel();

        public AgentCreateVM()
        {
            this.CitySourceView = ResourcesBase.GetMainWindowViewModel().CityCollection.SourceView;
            this.AgentCollection = ResourcesBase.GetMainWindowViewModel().AgentCollection;

            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancelAction() };
        }

        //Command

        public CommandHandler Save { get; set; }
        public CommandHandler Cancel { get; set; }


        //validation

        private bool SaveValidation()
        {
            if (errors.ErrorCount <= 0 && this.IsValid)
            { return true; }
            else
                return false;
        }


        //action
        private async void SaveAction()
        {
          
          var item = new ModelsShared.Models.agent
          {
              Address = this.Address,
              CityID = this.CityID,
              ContactName = this.ContactName,
              Email = this.Email,
              Handphone = this.Handphone,
              Id = this.Id,
              Name = this.Name,
              NPWP = this.NPWP,
              Phone = this.Phone
          };


            var result = await main.AgentCollection.Add(item);
            if (result)
            {
                AgentCollection.SourceView.Refresh();
                ModernDialog.ShowMessage("Data Is Saved !", "Info", System.Windows.MessageBoxButton.OK);
                this.CancelAction();
            }
            else
            {
                ModernDialog.ShowMessage("Data Is Not Saved !", "Error", System.Windows.MessageBoxButton.OK);
            }

        }
        private void CancelAction()
        {
            this.Address = string.Empty;
            this.CityID = 0;
            this.ContactName = string.Empty;
            this.Email = string.Empty;
            this.Handphone = string.Empty;
            this.Id = 0;
            this.Name = string.Empty;
            this.NPWP = string.Empty;
            this.Phone = string.Empty;
        }


        //property
        public CollectionView CitySourceView
        { get; private set; }

        public AgentCollection AgentCollection { get; private set; }


        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(this.Name))
                    {
                        var err = "Name Is Requered !";
                        errors.AddError(columnName,err );
                        return err;
                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }
                }

                

                if (columnName == "ContactName")
                {
                    if (string.IsNullOrEmpty(this.ContactName))
                    {
                        var err = "Contact Name Is Requered";
                        errors.AddError(columnName, err);
                        return err;

                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }
                       
                }

                if (columnName == "Handphone")
                {
                    if (string.IsNullOrEmpty(this.Handphone))
                    {
                        var err = "Handphone Is Requered";
                        errors.AddError(columnName, err);
                        return err;

                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }

                }


                if (columnName=="CityID")
                {
                    if (CityID <= 0)
                    {
                        var err = "City Is Requred";
                        errors.AddError(columnName,err);
                        return err;
                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }
                    

                }

                if (columnName == "Address")
                {
                    if (string.IsNullOrEmpty(this.Address))
                    {
                        var err = "Address Is Requered !";
                        errors.AddError(columnName, err);
                        return err;
                    }
                    else
                    {
                        errors.DeleteError(columnName);
                        return null;
                    }
                }

                return null;
            }
        }

      

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
          
        }

    }
}
