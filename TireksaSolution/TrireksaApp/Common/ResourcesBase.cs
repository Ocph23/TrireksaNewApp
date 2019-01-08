using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using TrireksaApp.Pages;

namespace TrireksaApp.Common
{
    public static class ResourcesBase
    {

        public static ModelsShared.Models.userprofile UserIsLogin { get; set; }

        public static AuthenticationToken Token { get; set; }

        public static HomeMainViewModel HomeVM { get; set; }

        internal static MainWindowVM GetMainWindowViewModel()
        {
            try
            {
                return (App.Current.Windows[0] as MainWindow).DataContext as MainWindowVM;
            }
            catch (Exception)
            {
                try
                {
                    var res = App.Current;
                    return  res.MainWindow.DataContext as MainWindowVM;
                }
                catch (Exception)
                {

                    return null;
                }
    
            }
        }

        internal static SMSClient GetSMSClient()
        {
            var vm= (App.Current.Windows[0] as MainWindow).DataContext as MainWindowVM;
            return vm.SMS;
        }


        internal static SignalRClient GetSignalClient()
        {
            var vm = (App.Current.Windows[0] as MainWindow).DataContext as MainWindowVM;
            return vm.signalClient;
        }


        public static List<T> GetEnumCollection<T>() 
        {
            var enums = Enum.GetValues(typeof(T));
            List<T> list = new List<T>();
            foreach(T item in enums)
            {
                list.Add(item);
            }
            return list;
        }


        internal static void ShowMessage(string message)
        {
            var form = App.Current.Windows[0] as MainWindow;
            form.ShowMessage(message);
        }
        internal static void ShowMessageError(string message)
        {
            var form = App.Current.Windows[0] as MainWindow;
            form.ShowMessageError(message);
        }

        internal static bool MessageAsk(string message)
        {
            var form = App.Current.Windows[0] as MainWindow;
            if (form.MessageAsk(message) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
                return false;
        }

        internal static OpenFileDialog ShowOpenFileDialog()
        {
            OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.ShowDialog();
            return dialog;
        }

        internal static void PrintPreview(string Title,string Layout, ReportDataSource source,ReportParameter[] parameters)
        {
            var content = new Reports.Contents.ReportContent(source,Layout, parameters);
            var dlg = new ModernWindow
            {
                Content = content,
                Title =Title,
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
            /*
             Example :
            ResourcesBase.PrintPreview("Print Photo", "TrireksaApp.Reports.Layouts.PrintImageLayout.rdlc",
              new Microsoft.Reporting.WinForms.ReportDataSource { Value = new List<Photo> { SelectedPhoto } },
            null);
            */

        }
    }




}
