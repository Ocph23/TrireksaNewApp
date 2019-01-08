using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Agent
{
    public class AgentListVM:Common.ViewModelBase
    {
        public AgentListVM()
        {
            this.Collection = MainVM.AgentCollection;
            this.Edit = new Common.CommandHandler { CanExecuteAction = x => EditValidation(), ExecuteAction = x => EditAction() };
            this.Delete = new Common.CommandHandler { CanExecuteAction = x => DeleteValidation(), ExecuteAction = x => DeleteAction() };
            this.ViewDetail = new Common.CommandHandler { CanExecuteAction = x => ViewDetailValidation(), ExecuteAction = x => ViewDetailAction() };
            this.CitiesAgentCanAccess = new Common.CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CitiesAgentAcanAccessAction() };
        }

        private void CitiesAgentAcanAccessAction()
        {
          
            var cnt = new Agent.CitiesAgentCanAccess();
            var vm = new Agent.CitiesAgentCanAccessVM(Collection.SelectedItem);
            cnt.DataContext = vm;
         
            var wnd = new ModernWindow
            {
                WindowStartupLocation= WindowStartupLocation.CenterOwner,
                Style = (Style)App.Current.Resources["BlankWindow"],
                Title = "Cities Agent Can Access",
                Content = cnt,
                Height = 480,
                ResizeMode = ResizeMode.CanResizeWithGrip, 

                  
            };
            vm.CloseWindow = wnd.Close;
            wnd.ShowDialog();

        }


        //Action
        private void ViewDetailAction()
        {
            var vm = new Agent.AgentDetailVM(Collection.SelectedItem);
            var cnt = new Agent.Details();
            cnt.DataContext = vm;
            var dlg = new ModernDialog
            {
                Title = "Agent Details",
                Content = cnt
            };
            dlg.Buttons = new Button[] { dlg.OkButton};
            dlg.ShowDialog();

        }

        private async void DeleteAction()
        {
            var result =await Collection.Delete(Collection.SelectedItem.Id);
            if (result)
            {
                Collection.SourceView.Refresh();
                ModernDialog.ShowMessage("Data Is Deleted !", "Info", System.Windows.MessageBoxButton.OK);
            }
            else
                ModernDialog.ShowMessage("Data Can't Delete!", "Error", System.Windows.MessageBoxButton.OK);
        }

        private async void EditAction()
        {
            var vm = new  Agent.AgentEditVM(Collection.SelectedItem);
            var cnt = new Agent.Edit();
            cnt.DataContext = vm;
            var dlg = new ModernDialog
            {
                Title = "Edit Agent",
                Content = cnt
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();

            if (dlg.MessageBoxResult== MessageBoxResult.OK)
            {
                var newitem = new ModelsShared.Models.agent
                {
                    Address = vm.Address,
                    ContactName = vm.ContactName,
                    Email = vm.Email,
                    Handphone = vm.Handphone,
                    Id = vm.Id,
                    Name = vm.Name,
                    CityID = vm.CityID,
                    NPWP = vm.NPWP,
                    Phone = vm.Phone
                };
                var isUpdate = await Collection.Update(Collection.SelectedItem.Id, newitem);
                if (isUpdate)
                {
                    var item = Collection.Source.Where(O => O.Id == Collection.SelectedItem.Id).FirstOrDefault();
                    if (item != null)
                    {
                        item.Address = newitem.Address;
                        item.ContactName = newitem.ContactName;
                        item.Email = newitem.Email;
                        item.Handphone = newitem.Handphone;
                        item.Name = newitem.Name;
                        item.Phone = newitem.Phone;
                        item.CityID = newitem.CityID;
                        item.Id = newitem.Id;
                        item.NPWP = newitem.NPWP;
                    }
                    Collection.SourceView.Refresh();
                    ModernDialog.ShowMessage("Data Is Updated !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }
            }
        }

      


        //validation
        private bool ViewDetailValidation()
        {
            if (this.Collection.SelectedItem != null)
                return true;
            else
                return false;
        }

        private bool EditValidation()
        {
            if (this.Collection.SelectedItem != null)
                return true;
            else
                return false;
        }

        private bool DeleteValidation()
        {
            if(this.Collection.SelectedItem!=null)
            {
                return true;
            }else
                return false;
        }


        //Command

        public Common.CommandHandler Save { get; set; }
        public Common.CommandHandler ViewDetail { get; set; }
        public CommandHandler Delete { get; private set; }
        public CommandHandler Edit { get; private set; }

        //Property

       

        public AgentCollection Collection { get; set; }
        public CommandHandler CitiesAgentCanAccess { get; set; }
    }
}
