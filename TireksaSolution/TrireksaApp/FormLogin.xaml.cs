using FirstFloor.ModernUI.Windows.Controls;
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

namespace TrireksaApp
{
    /// <summary>
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin :ModernWindow
    {
        private FormLoginVM viewmodel;

        public FormLogin()
        {
            InitializeComponent();
            Style = (Style)App.Current.Resources["BlankWindow"];
           this.viewmodel = new FormLoginVM() { CloseWindow = Close };
            this.DataContext = viewmodel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var obj = (PasswordBox)sender;
            viewmodel.Password = obj.Password;

        }
        
    }
}
