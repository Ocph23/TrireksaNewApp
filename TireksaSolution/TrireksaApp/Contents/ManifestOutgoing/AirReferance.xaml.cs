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
using ModelsShared.Models;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    /// <summary>
    /// Interaction logic for AirReferance.xaml
    /// </summary>
    public partial class AirReferance : UserControl
    {
        public AirReferance()
        {
            InitializeComponent();
        }

        public PortType PortType { get; internal set; }
    }
}
