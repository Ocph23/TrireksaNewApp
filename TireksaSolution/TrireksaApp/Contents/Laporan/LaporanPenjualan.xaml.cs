using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Reporting.WinForms;
using ModelsShared.Models;
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
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Laporan
{
    /// <summary>
    /// Interaction logic for LaporanPenjualan.xaml
    /// </summary>
    public partial class LaporanPenjualan : UserControl
    {
        ReportDataSource reportDataSource = new ReportDataSource();
       private LaporanPenjualanViewModel vm= new LaporanPenjualanViewModel();
        public LaporanPenjualan()
        {
            InitializeComponent();
            this.DataContext = vm;
            this.Loaded += LaporanPenjualan_Loaded;
        }

        private void LaporanPenjualan_Loaded(object sender, RoutedEventArgs e)
        {
            var now = DateTime.Now;
            this.StartDate = new DateTime(now.Year, now.Month, 1);
            this.EndDate = StartDate.AddMonths(1).AddDays(-1);
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportDataSource.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 120;
            reportViewer.LocalReport.ReportEmbeddedResource = "TrireksaApp.Reports.Layouts.PenjualanFromToLayout.rdlc";
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            customers = ResourcesBase.GetMainWindowViewModel().CustomerCollection;
            shiper.ItemsSource = customers.Source;
        }

        private PenjualanCollection context;
        private CustomerCollection customers;

      

        private DateTime _start;

        public DateTime StartDate
        {
            get { return _start; }
            set
            {
                _start = value;
               
            }
        }

        private DateTime _endDate;
    //    private customer _shiperSelected;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {

                _endDate = value;
            }
        }


        public async void GetDataPenjualanAsync(DateTime start, DateTime end)
        {
            this.context = ResourcesBase.GetMainWindowViewModel().PenjualanCollection;
            if (StartDate != null && EndDate != null && StartDate <= EndDate)
            {
                var result = await context.GetPenjualanFromTo(start, end);

                if(result!=null)
                {

                    if (vm.ShiperSelected != null)
                    {
                        reportDataSource.Value = result.Where(O => O.Shiper == vm.ShiperSelected.Name).ToList();
                    }
                    else if (result != null)
                    {
                        reportDataSource.Value = result;
                    }
                    reportViewer.RefreshReport();
                }
                else
                {
                    ModernDialog.ShowMessage("Data Tidak Ada", "Not Found", MessageBoxButton.OK);
                }
               
            }
      
        }

        private void EndDateAction(object sender, SelectionChangedEventArgs e)
        {
            var dp = ((DatePicker)sender);
            if (dp != null && dp.SelectedDate != null)
            {
                EndDate =dp.SelectedDate.Value;
            }
        }

        private void StartDateAction(object sender, SelectionChangedEventArgs e)
        {
            var dp = ((DatePicker)sender);
            if (dp!=null && dp.SelectedDate!=null)
            {
                StartDate =dp.SelectedDate.Value;
            }
        }

        private void cari_Click(object sender, RoutedEventArgs e)
        {
            GetDataPenjualanAsync(StartDate, EndDate);
        }
    }
}
