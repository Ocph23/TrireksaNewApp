﻿using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
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

namespace TrireksaApp.Contents.Customer
{
    /// <summary>
    /// Interaction logic for list.xaml
    /// </summary>
    public partial class list : UserControl
    {
        public list()
        {
            InitializeComponent();
            this.DataContext = new Contents.Customer.CustomerListVM();
        }

        
    }
}
