using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Port
{
    public class PortCreateVM : ModelsShared.Models.port, IDataErrorInfo
    {
        public CollectionsBase.CityCollection CityCollection { get; set; }
        public List<ModelsShared.Models.PortType> PortTypes { get; set; }
        public PortCollection Collection { get; set; }

        public PortCreateVM()
        {
            var vm = ResourcesBase.GetMainWindowViewModel();
            Collection = vm.PortCollection;
            CityCollection = vm.CityCollection;
            PortTypes = ResourcesBase.GetEnumCollection<ModelsShared.Models.PortType>();

            Save = new CommandHandler { CanExecuteAction = x => SaveValidation(), ExecuteAction = x => SaveAction() };
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = c => CancelAction() };
        }

        public void RefreshAction()
        {
            CityCollection.SourceView.Refresh();
        }

        private void CancelAction()
        {
            this.CityID = 0;
            this.CityName = string.Empty;
            this.Code = string.Empty;
            this.Id = 0;
            this.Name = string.Empty;
            this.PortType = ModelsShared.Models.PortType.None;
        }

        private async void SaveAction()
        {
            ModelsShared.Models.port port = new ModelsShared.Models.port
            {

                CityID = this.CityID,
                Code = this.Code,
                PortType = this.PortType,
                Id = this.Id,
                Name = this.Name,
                CityName = CityCollection.SelectedItem.CityName
            };
            var res = await Collection.Add(port);
            if(res!=null)
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                var result = ModernDialog.ShowMessage("Data Saved?", "Message Dialog", btn);
                Cancel.Execute(null);
            }
         
        }

        private bool SaveValidation()
        {
            return IsValid;
        }

        public CommandHandler Save { get; set; }
        public CommandHandler Cancel { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    return string.IsNullOrEmpty(this.Name) ? "Name Required Value" : null;
                }

                if (columnName == "Code")
                {
                    return string.IsNullOrEmpty(this.Code) ? "Code Required Value" : null;
                }

                if (columnName == "CityID")
                {
                    return (CityID<=0 ) ? "Select a City" : null;
                }

                if (columnName == "PortType")
                {
                    return (this.PortType== ModelsShared.Models.PortType.None) ? "Select a Type" : null;
                }

                return null;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
    }
}
