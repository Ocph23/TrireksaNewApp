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

namespace TrireksaApp.Pages
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }

        private void Frame_FragmentNavigation(object sender, FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {

        }

        private void Frame_Navigated(object sender, FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Frame_Navigating(object sender, FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }

        private void Frame_NavigationFailed(object sender, FirstFloor.ModernUI.Windows.Navigation.NavigationFailedEventArgs e)
        {

        }
    }
}
