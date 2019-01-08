using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using OcphSMSLib;

namespace TestWithWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public delegate void DisplayMessage(string message,CollectionView source);
        private List<string> list;
        private Modem modem;
        public event DisplayMessage Display;

        public MainWindow()
        {
            InitializeComponent();
            this.list = new List<string>();
            this.Source = (CollectionView)CollectionViewSource.GetDefaultView(list);
            this.DataContext = this;
            list.Add("test");
            this.Display += MainWindow_Display1; ;
            this.modem = new OcphSMSLib.Modem("COM5");
            modem.OnErrorMessage += Modem_OnErrorMessage;
            modem.OnReciveSMS += Modem_OnReciveSMS;
            modem.OnRecive += Modem_OnRecive;
            var isconect = modem.Connect();
            CompleteConnected(isconect);
        }

     

        private void MainWindow_Display1(string message, CollectionView source)
        {
            list.Add(message);
            Source.Refresh();
        }

     

        private  void Modem_OnRecive(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            if(Display!=null)
            {
                MainWindow_Display1(sp.ReadExisting(), Source);
            }
         
        }

        private void Modem_OnReciveSMS(OcphSMSLib.Models.InMessage inbox, EventArgs args)
        {
        }

        private  async void CompleteConnected(Task<bool> isconect)
        {
            var iscon = await isconect;
            MainWindow_Display1(iscon.ToString(), Source);
        }

        private void Modem_OnErrorMessage(Exception ex)
        {
            MainWindow_Display1(ex.Message, Source);
        }

    

        public CollectionView Source { get; set; }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            modem.SetModeSMS(OcphSMSLib.SMSMOde.Text);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Source.Refresh();
        }
    }
}
