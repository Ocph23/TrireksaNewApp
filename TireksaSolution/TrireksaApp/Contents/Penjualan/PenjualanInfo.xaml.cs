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

namespace TrireksaApp.Contents.Penjualan
{
    /// <summary>
    /// Interaction logic for PenjualanInfo.xaml
    /// </summary>
    public partial class PenjualanInfo : UserControl
    {
        public PenjualanInfo()
        {
            InitializeComponent();
           
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //CtsMenuShiper.IsOpen = true;
        }

        private void txtReciver_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CtsMenuReciver.IsOpen = true;
        }

     
    }
}
