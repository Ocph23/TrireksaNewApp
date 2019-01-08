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

namespace TrireksaApp.Contents.Port
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        private PortCreateVM viewmodel;

        public Create()
        {
            InitializeComponent();
            this.viewmodel = new Contents.Port.PortCreateVM();
            this.DataContext = this.viewmodel;
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            viewmodel.RefreshAction();
        }
    }
}
