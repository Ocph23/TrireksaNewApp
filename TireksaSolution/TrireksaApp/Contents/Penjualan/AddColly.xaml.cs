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
using TrireksaApp.Models;

namespace TrireksaApp.Contents.Penjualan
{
    /// <summary>
    /// Interaction logic for AddColly.xaml
    /// </summary>
    public partial class AddColly : ModernWindow
    {
        public AddColly()
        {
            InitializeComponent();
            this.Loaded += AddColly_Loaded;
        }

        private void AddColly_Loaded(object sender, RoutedEventArgs e)
        {
            dg.BeginEdit();
        }

        private void dg_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var vm = (AddCollyVM)this.DataContext;
            var newColly = new Models.newcolly { TypeOfWeight = vm.TypeOfWeightBase };
            if (vm.details.Count>0)
            {
                var last = vm.details.Last();
                if(last!=null)
                {
                    newColly.PenjualanId = last.PenjualanId;
                    newColly.TypeOfWeight = last.TypeOfWeight;
                }
            }

            e.NewItem = newColly;

        }

        private void dg_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            var vm = (AddCollyVM)this.DataContext;
            vm.TotalColly = string.Empty;
            vm.TotalWeight = string.Empty;
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            var vm = (AddCollyVM)this.DataContext;
            vm.TotalColly = string.Empty;
            vm.TotalWeight = string.Empty;
         
        }

        private void dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void dg_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
