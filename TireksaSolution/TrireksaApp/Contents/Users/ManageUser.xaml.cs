using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrireksaApp.Contents.Users
{
    /// <summary>
    /// Interaction logic for ManageUser.xaml
    /// </summary>
    public partial class ManageUser : UserControl
    {
        ManageUserViewModel vm = new ManageUserViewModel();
        public ManageUser()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            var items =(roles) menu.Items.CurrentItem;
            vm.AddNewRole.Execute(items);
        }
    }



    public enum MyVisibility
    {
        Visible, Hidden
    }
}
