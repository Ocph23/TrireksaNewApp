using FirstFloor.ModernUI.Windows.Controls;
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

namespace TrireksaApp.Contents.ManifestOutgoing
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : UserControl
    {
        private ManifestOutGoingCreateVM viewmodel;

        public Create()
        {
            InitializeComponent();
           this.viewmodel= new Contents.ManifestOutgoing.ManifestOutGoingCreateVM();
            this.DataContext = viewmodel;
        }

        private void Frame_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewmodel.PortCollection.SourceView.Refresh();
            var cmb = (ComboBox)sender;
            var t = cmb.SelectedItem.ToString();
          

            viewmodel.GetPenjualan();
           
        }

        private void Frame_FragmentNavigation(object sender, FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {

        }

        private void Frame_FragmentNavigation_1(object sender, FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {

        }

        private void Frame_Navigated(object sender, FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            /*
            ModernFrame control = (ModernFrame)sender;
            var type = control.Content.GetType();
            if(type.Name=="SeaReferance")
            {
                var content = (SeaReferance)control.Content;
                var vm = (SeaReferanceVM)content.DataContext;
                vm.PortType = ModelsShared.Models.PortType.Sea;
                viewmodel.SetReferance(vm);
            }
            if (type.Name == "AirReferance")
            {
                var content = (AirReferance)control.Content;
                content.PortType = ModelsShared.Models.PortType.Sea;
            }
            */

        }

        private void Frame_Navigating(object sender, FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }

        private void Frame_NavigationFailed(object sender, FirstFloor.ModernUI.Windows.Navigation.NavigationFailedEventArgs e)
        {

        }

        private void AgentSelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
