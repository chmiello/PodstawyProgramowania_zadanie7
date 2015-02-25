using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace zadanie7_pp
{
    /// <summary>
    /// Interaction logic for condInformation.xaml
    /// </summary>
    public partial class condInformation : Window
    {
        public condInformation(String item)
        {
            InitializeComponent();
            WriteInfoLabel.Content = item;
        }
    }
}
