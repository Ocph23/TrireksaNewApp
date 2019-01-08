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

namespace TrireksaApp.Contents.ManifestOutgoing
{
    /// <summary>
    /// Interaction logic for BrowseSTT.xaml
    /// </summary>
    public partial class BrowseSTT : UserControl
    {
        public BrowseSTT()
        {
            InitializeComponent();
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var res = ((DataGrid)sender).CurrentItem as PenjualanView;
            res.IsSelected= !res.IsSelected;
            var dc = (BrowseSTTVM)DataContext;
            dc.SimulationPack.SelectedItemDetailsView.Refresh();
            dc.SimulationPack.SelectedItem = null;
            dc.SimulationPack.SelectedItem = res;
        }
    }
}
