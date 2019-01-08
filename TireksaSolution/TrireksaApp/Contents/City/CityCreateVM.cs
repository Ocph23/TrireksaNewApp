using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.City
{
    public class CityCreateVM :ModelsShared.Models.city, IDataErrorInfo
    {
        private bool _isActive;

        public CityCreateVM()
        {
            ProgressIsActive = false;
            Save = new CommandHandler { CanExecuteAction = x => SaveValidate(), ExecuteAction = x => SaveAction() };
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancelAction() };
        }

        private void CancelAction()
        {
            this.CityCode = string.Empty;
            this.CityName = string.Empty;
            this.Id = 0;
            this.Province = string.Empty;
            this.Regency = string.Empty;
            
        }

        private async void SaveAction()
        {
            ProgressIsActive = true;
            ModelsShared.Models.city city = new ModelsShared.Models.city { CityCode = this.CityCode, CityName = this.CityName, Province = this.Province, Regency = this.Regency };
            var res = await Common.ResourcesBase.GetMainWindowViewModel().CityCollection.Post(city);
            MessageBoxButton btn = MessageBoxButton.OK;
            if (res == null)
                ModernDialog.ShowMessage("Data Not Saved !", "Message Dialog", btn);
            else
            {
                var collection = ResourcesBase.GetMainWindowViewModel().CityCollection;
                collection.Source.Add(res);
                collection.SourceView.Refresh();
                var result = ModernDialog.ShowMessage("Data Saved?", "Message Dialog", btn);
                CancelAction();
              
            }
            ProgressIsActive = false;
        }

        private bool SaveValidate()
        {
            if (!string.IsNullOrEmpty(Province) && !string.IsNullOrEmpty(Regency)
                 && !string.IsNullOrEmpty(CityName) && !string.IsNullOrEmpty(CityCode))
                return true;
            else
                return false;
        }

        public CommandHandler Save { get; set; }
        public CommandHandler Cancel{ get; set; }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Province")
                {
                    return string.IsNullOrEmpty(this.Province) ? "Province Required Value" : null;
                }
                if (columnName == "Regency")
                {
                    return string.IsNullOrEmpty(this.Regency) ? "Regency Required value" : null;
                }
                if (columnName == "CityName")
                {
                    return string.IsNullOrEmpty(this.CityName) ? "City Name Required value" : null;
                }
                if (columnName == "CityCode")
                {
                    return string.IsNullOrEmpty(this.CityCode) ? "City Code Required value" : null;
                }
                return null;
            }
        }

        public bool ProgressIsActive { get { return _isActive; } set {

                _isActive = value;
                OnPropertyChange("ProgressIsActive");
            } }
    }
}