using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace TrireksaApp.Pages.Settings
{
    /// <summary>
    /// Interaction logic for Nota.xaml
    /// </summary>
    public partial class Nota : UserControl
    {
       
        public Nota()
        {
            InitializeComponent();
            this.DataContext = this;
            var main = ResourcesBase.GetMainWindowViewModel();
            Config = main.SystemConfiguration;
            ShiperCities = (CollectionView)CollectionViewSource.GetDefaultView(main.CityCollection.Source);
            ReciverCities = (CollectionView)CollectionViewSource.GetDefaultView(main.CityCollection.Source);
            this.Loaded += Nota_Loaded;
            
        }

        private void Nota_Loaded(object sender, RoutedEventArgs e)
        {
      
            ShiperCities.Refresh();
            ReciverCities.Refresh();
        }

        public AppConfiguration Config { get; set; }
        public CollectionView ShiperCities { get; set; }
        public CollectionView ReciverCities { get; set; }
    }
}
