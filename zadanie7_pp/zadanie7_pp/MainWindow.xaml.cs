using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace zadanie7_pp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Tagtoken> tagtokens;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".xml"; // Default file extension
            dlg.Filter = "xml documents (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                
                // clear 
                this.tagtokens = new List<Tagtoken>();
                
                
                // Open document
                XmlDocument doc = new XmlDocument();

                Console.WriteLine(dlg.FileName);
                
                 
                doc.Load(@dlg.FileName);
                
                foreach (XmlNode xn in doc.SelectNodes("//tagtoken"))
                {
                    Tagtoken tmp = new Tagtoken();
                    tmp.setXmlId(xn.Attributes["xml:id"].InnerText);
                    foreach (XmlNode tag in xn.SelectNodes("tags/tag"))
                        tmp.addTag(tag.Attributes["type"].InnerText, tag.InnerText);

                    this.tagtokens.Add(tmp);
                }

                Console.WriteLine(this.tagtokens.Count());
            }
        }
    }
}
