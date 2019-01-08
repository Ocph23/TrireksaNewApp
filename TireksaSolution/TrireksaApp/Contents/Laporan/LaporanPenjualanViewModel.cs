using Microsoft.Reporting.WinForms;
using ModelsShared;
using ModelsShared.Models;
using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Laporan
{
  public  class LaporanPenjualanViewModel:BaseNotifyProperty
    {
        private customer _shiperSelected;

        public customer ShiperSelected
        {
            get
            {
                return _shiperSelected;
            }
            set
            {
                _shiperSelected = value;
                OnPropertyChange("ShiperSelected");
            }
        }
    }
}
