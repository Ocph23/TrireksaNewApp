using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.City
{
    public class CityListVM :Common.ViewModelBase, IDataErrorInfo
    {
        private string _search;

        public CollectionsBase.CityCollection Collection { get; set; }
        public CityListVM()
        {
            Delete = new CommandHandler { CanExecuteAction = x => DeleteValidation(), ExecuteAction = x => DeleteAction() };
            Edit = new CommandHandler { CanExecuteAction = x => EditValidation(), ExecuteAction = x => EditAction() };

            var mw = App.Current.Windows[0] as MainWindow;
            this.Collection = ((MainWindowVM)mw.DataContext).CityCollection;
            Collection.SourceView.Filter = FilterItem;
           
        }

        private async void EditAction()
        {
            var vm = new Contents.City.CityEditVM(Collection.SelectedItem);
            var cnt = new Contents.City.Edit();
            cnt.DataContext = vm;
            var dlg = new ModernDialog
            {
                Title = "Common dialog",
                Content = cnt
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if(dlg.DialogResult.Value)
            {
                ModelsShared.Models.city city = new ModelsShared.Models.city { Id=vm.Id, CityCode = vm.CityCode, CityName = vm.CityName, Province = vm.Province, Regency = vm.Regency };
                bool isUpdated = await MainVM.CityCollection.Update(Collection.SelectedItem.Id, city);
                if (isUpdated)
                {
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Updated !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }
            }

        }

        private async void DeleteAction()
        {
            var result = ModernDialog.ShowMessage("Are You Sure  Delete Data ?", "Message Dialog",  System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                var isDeleted = await  MainVM.CityCollection.Delete(Collection.SelectedItem.Id);
                if (isDeleted)
                {
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Deleted !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }
                else
                {
                    ModernDialog.ShowMessage("Data Can Not Deleted !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }

            }
            
        }

        private bool EditValidation()
        {
            if (Collection.SelectedItem != null)
                return true;
            else
                return false;
        }

        private bool DeleteValidation()
        {
            if (Collection.SelectedItem != null)
                return true;
            else
                return false;
        }

        public CommandHandler Delete { get; set; }
        public CommandHandler Edit { get; set; }

        private bool FilterItem(object x)
        {
            var scr = string.Empty;
           
            if (!String.IsNullOrEmpty(this.Search))
            {
                scr = this.Search.ToUpper();
                var obj = (ModelsShared.Models.city)x;
                return (obj.CityCode.ToUpper().Contains(scr)
                    || obj.CityName.ToUpper().Contains(scr)
                    || obj.Province.ToUpper().Contains(scr)
             || obj.Regency.ToUpper().Contains(scr));
            }
            else
                return true;
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Search {

            get { return _search; }
            set
            {
                _search = value;
                Collection.SourceView.Refresh();
                OnPropertyChanged("Search");
            }
        }
    }
}
