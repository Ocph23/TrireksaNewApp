﻿using System;
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

namespace TrireksaApp.Contents.Invoice
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : UserControl
    {
        private InvoiceListVM viewmodel;

        public List()
        {
            InitializeComponent();
            this.viewmodel = new InvoiceListVM();
            this.DataContext = viewmodel;
        }
    }
}
