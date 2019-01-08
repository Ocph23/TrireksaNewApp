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
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Penjualan
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        private PenjualanCreateVM viewmodel;

        public Create()
        {
            InitializeComponent();
            this.viewmodel=new  Contents.Penjualan.PenjualanCreateVM();
            this.DataContext = viewmodel;
           
        }
        

        private void TypeOfWeightCgange(object sender, SelectionChangedEventArgs e)
        {
            var vm = (Contents.Penjualan.PenjualanCreateVM)this.DataContext;
            var cmb = (ComboBox)sender;
            vm.SetWeight(cmb.SelectedItem);
        }

        private void AddShiper(object sender, KeyEventArgs e)
        {
        
            if(e.Key== Key.Enter && viewmodel.ShiperSelected==null)
            {
                ComboBox cmb = (ComboBox)sender;
                var form = new Contents.Customer.Create();
                var vm = form.DataContext as Contents.Customer.CustomerCreateVM;
                vm.Name = cmb.Text;
              var dlg = new ModernDialog { MinWidth=800, MinHeight=500, Content = form, Title="Add Shiper" };
                dlg.ShowDialog();
               viewmodel.LoadShiper();
            }
       
        }

        private void AddReciver(object sender, KeyEventArgs e)
        {
     
            if (e.Key == Key.Enter && viewmodel.ReciverSelected==null)
            {
                ComboBox cmb = (ComboBox)sender;
                var form = new Contents.Customer.Create();
                var vm = form.DataContext as Contents.Customer.CustomerCreateVM;
                vm.Name = cmb.Text;
                var dlg = new ModernDialog { MinWidth = 800, MinHeight = 500, Content = form, Title = "Add Reciver" };
                dlg.ShowDialog();
                viewmodel.LoadReciver();
            }

      
        }

        private void SearchPress(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                TextBox tb = (TextBox)sender;
                string spb = tb.Text;
                viewmodel.Search.Execute(tb.Text);
                tb.Text = spb;
                
            }
                   
        }
    }
}
