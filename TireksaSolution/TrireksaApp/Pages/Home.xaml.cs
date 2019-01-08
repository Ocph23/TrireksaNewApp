using ModelsShared;
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

namespace TrireksaApp.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private HomeMainViewModel vm;

        public Home()
        {
            InitializeComponent();
            vm= new HomeMainViewModel();
            this.DataContext = vm;
            ResourcesBase.HomeVM = vm;
        }
    }



    public class HomeMainViewModel:BaseNotifyProperty
    {
        public HomeMainViewModel()
        {
            Inittial();
        }

        private async void Inittial()
        {
            var clien = new Client("Test");
            var res =  await clien.GetAsync<string>("GetLocalIpAddress");
            IPAdress = res;
        }

        
        private string ip;
        private string _barMessage;

        public string IPAdress
        {
            get { return ip; }
            set {
                ip = value;
                OnPropertyChange("IPAdress");
            }
        }

        public string BarMessage
        {
            get { return _barMessage; }
            set
            {
                _barMessage = value;
                OnPropertyChange("BarMessage");
            }
        }

    }



}
