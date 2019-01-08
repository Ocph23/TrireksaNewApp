using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Port
{
    public class PortListVM : NotifyPropertyChanged
    {
        private string _search;

        public CollectionsBase.PortCollection Collection { get; set; }
        public PortListVM()
        {
            Delete = new CommandHandler { CanExecuteAction = x => DeleteValidation(), ExecuteAction = x => DeleteAction() };
            Edit = new CommandHandler { CanExecuteAction = x => EditValidation(), ExecuteAction = x => EditAction() };

            MainVm = ResourcesBase.GetMainWindowViewModel();
            this.Collection = MainVm.PortCollection;
            Collection.SourceView.Filter = FilterItem;

        }

        private async void EditAction()
        {
            var vm = new Contents.Port.PortEditVM(Collection.SelectedItem);
            var cnt = new Contents.Port.Edit();
            cnt.DataContext = vm;
            var dlg = new ModernDialog
            {
                Title = "Edit",
                Content = cnt
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.DialogResult.HasValue)
            {
                ModelsShared.Models.port port = new ModelsShared.Models.port
                {
                    CityID = vm.CityID,
                    Code = vm.Code,
                    PortType = vm.PortType,
                    Id = vm.Id,
                    Name = vm.Name,
                    CityName =MainVm.CityCollection.Source.Where(O => O.Id == vm.CityID).FirstOrDefault().CityName
                };
                var isUpdated = await Collection.Update(port.Id, port);
                if (isUpdated!=default(ModelsShared.Models.port))
                {
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Updated !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }
            }

        }

        private async void DeleteAction()
        {
            var result = ModernDialog.ShowMessage("Are You Sure  Delete Data ?", "Message Dialog", System.Windows.MessageBoxButton.YesNo);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                var isDeleted = await Collection.Delete(Collection.SelectedItem.Id);
                if (isDeleted!=default(ModelsShared.Models.port))
                {
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Deleted !", "Message Dialog", System.Windows.MessageBoxButton.OK);
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
        public MainWindowVM MainVm { get; }

        private bool FilterItem(object x)
        {
            if (!string.IsNullOrEmpty(this.Search))
            {
                var scr = this.Search.ToUpper();
                var obj = (ModelsShared.Models.port)x;
                return (obj.Code.ToUpper().Contains(scr)
                    || obj.CityName.ToUpper().Contains(scr)
                    || obj.Name.ToUpper().Contains(scr));
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

        public string Search
        {

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