using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Users
{
    public class ManageUserViewModel : BaseNotifyProperty
    {
        private bool _isShowData;
        private userprofile _selected;
        private roles _RoleSelected;
        private roles _AddRoleSelected;

        public ObservableCollection<roles> RoleSource { get; set; }

        public ManageUserViewModel()
        {
            AddNewRole = new CommandHandler { CanExecuteAction = AddNewRoleValidate, ExecuteAction = AddNewRoleAction };
            RemoveRole = new CommandHandler { CanExecuteAction = x => RoleSelected != null && SelectedItem!=null, ExecuteAction= RemoveRoleActio };
            this.MainVM= Common.ResourcesBase.GetMainWindowViewModel();
            RoleSource = new ObservableCollection<roles>();
            InitAsync();
            RolesView = (CollectionView)CollectionViewSource.GetDefaultView(RoleSource);
            
        }

        private async void RemoveRoleActio(object obj)
        {
          roles role = await MainVM.UserProfileCollections.RemoveRole(SelectedItem, RoleSelected);
            if(role!=null)
            {
                SelectedItem.Roles.Remove(RoleSelected);
                RoleSource.Remove(RoleSelected);
                RolesView.Refresh();
                Roles.Refresh();
            }
        }

        private async void AddNewRoleAction(object obj)
        {
            var role = (roles)obj;
            if(role!=null)
            {
                var roleSaved = await MainVM.UserProfileCollections.AddNewUserRole(SelectedItem, role);
                if(roleSaved!=null)
                {
                    SelectedItem.Roles.Add(role);
                    RoleSource.Add(role);
                    RolesView.Refresh();
                    Roles.Refresh();
                }
               
            }
             
        }

        private bool AddNewRoleValidate(object obj)
        {
            if (SelectedItem != null)
                return true;
            return false;
        }

        public object UserLogin { get;  set; }
        public CommandHandler AddNewRole { get; }
        public CommandHandler RemoveRole { get; }
        public MainWindowVM MainVM { get; }

        public bool IsShowData
        {
            get
            {
                return _isShowData;
            }
            set
            {
                _isShowData = value;
                OnPropertyChange("IsShowData");
            }
        }

        public CollectionView Roles { get;  set; }
        public CollectionView RolesView { get; }

        private async void InitAsync()
        {
            IsShowData = MainVM.UserProfileCollections.Source.Count > 0;
            var result= await MainVM.UserProfileCollections.GetRoles();
            if(result!=null)
            {
                Roles = (CollectionView)CollectionViewSource.GetDefaultView(result);
                Roles.Filter = RolesFilter;
                Roles.Refresh();
            }

        }

        private bool RolesFilter(object obj)
        {
            roles role = (roles)obj;
            if(role!=null && SelectedItem!=null && SelectedItem.Roles!=null)
            {
                foreach(var item in SelectedItem.Roles)
                {
                    if (role.Id == item.Id)
                        return false;
                }
            }

            return true;
        }

        public userprofile SelectedItem
        {
            get { return _selected; }
            set
            {
                _selected = value;
                ChangeRoleViews(_selected);
                OnPropertyChange("SelectedItem");
            }
        }

        public roles AddRoleSelected
        {
            get
            {
                return _AddRoleSelected;
            }
            set
            {
                _AddRoleSelected = value;
                if (_AddRoleSelected != null)
                    AddNewRole.Execute(_AddRoleSelected);
                OnPropertyChange("AddRoleSelected");
            }
        }

        public roles RoleSelected
        {
            get { return _RoleSelected; }
            set
            {
                _RoleSelected = value;
                OnPropertyChange("RoleSelected");
            }
        }

        private void ChangeRoleViews(userprofile selected)
        {
           if(selected!=null && selected.Roles!=null)
            {
                RoleSource.Clear();
                foreach(var item in selected.Roles)
                {
                    RoleSource.Add(item);
                }
                RolesView.Refresh();
                Roles.Refresh();
            }
        }
    }
}
