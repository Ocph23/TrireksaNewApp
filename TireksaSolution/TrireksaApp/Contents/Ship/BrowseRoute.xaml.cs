using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ModelsShared.Models;

namespace TrireksaApp.Contents.Ship
{
    /// <summary>
    /// Interaction logic for BrowseRoute.xaml
    /// </summary>
    public partial class BrowseRoute : UserControl
    {
        public BrowseRoute()
        {
            InitializeComponent();

            var ports = Common.ResourcesBase.GetMainWindowViewModel().PortCollection.Source;
            Ports = new ObservableCollection<ModelsShared.Models.port>();
            foreach (var item in ports)
            {
                if (item.PortType == PortType.Sea)
                {
                    Ports.Add(item);
                }

            }
            this.DataContext = this;
        }

        public ModelsShared.Models.port SelectedItem { get; set; }

        public ObservableCollection<port> Ports { get; private set; }
    }
}
