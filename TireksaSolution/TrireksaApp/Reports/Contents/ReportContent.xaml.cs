using Microsoft.Reporting.WinForms;
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

namespace TrireksaApp.Reports.Contents
{
    /// <summary>
    /// Interaction logic for ReportContent.xaml
    /// </summary>
    public partial class ReportContent : UserControl
    {
        private LocalReport report;

        public ReportContent(ReportDataSource reportDataSource, string layout, ReportParameter[] parameters )
        {
            InitializeComponent();
            reportDataSource.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportViewer.LocalReport.ReportEmbeddedResource = layout;
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 120;
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            if(parameters!=null)
              reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.RefreshReport();
        }

        public ReportContent(List<ReportDataSource> reportDataSources, string layout, ReportParameter[] parameters)
        {
            InitializeComponent();
            reportViewer.LocalReport.ReportEmbeddedResource = layout;
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 120;
            foreach (var item in reportDataSources)
            {
                reportViewer.LocalReport.DataSources.Add(item);
            }
            if (parameters != null)
                reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.RefreshReport();
        }

    }
}
