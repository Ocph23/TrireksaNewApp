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

namespace TrireksaApp.Templates
{
    /// <summary>
    /// Interaction logic for TextBoxInput.xaml
    /// </summary>
    public partial class TextBoxInput : UserControl
    {
        public TextBoxInput()
        {
            InitializeComponent();
        }

        public string LabelText {
            get { return lb.Content.ToString(); }
            set {
                lb.Content = value;
            } }

        public string Text
        {
            get
            {
                return tx.Text;
            }
            set
            {
                tx.Text = value;
            }
        }

        public double LabelWidth
        {
            get { return lb.Width; }
            set { lb.Width = value; }
        }

    }
}
