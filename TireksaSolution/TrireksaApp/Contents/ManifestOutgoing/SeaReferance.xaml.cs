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
using TrireksaApp.CollectionsBase;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    /// <summary>
    /// Interaction logic for SeaReferance.xaml
    /// </summary>
    public partial class SeaReferance : UserControl
    {
        public SeaReferance()
        {
            InitializeComponent();
            this.DataContext = new Contents.ManifestOutgoing.SeaReferanceVM();
         
        }

       
    }
}
