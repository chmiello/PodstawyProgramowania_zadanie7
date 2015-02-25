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

        private Dictionary<string, Tagtoken> tagtokens;
        private Dictionary<string, Dr> domain;
        private List<Cond> conds;

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
                this.tagtokens = new Dictionary<string, Tagtoken>();
                this.domain = new Dictionary<string, Dr>();
                this.conds = new List<Cond>();
                
                // Open document
                XmlDocument doc = new XmlDocument();

                Console.WriteLine(dlg.FileName);
                
                 
                doc.Load(@dlg.FileName);

                // tagtoken parse

                foreach (XmlNode xn in doc.SelectNodes("//tagtoken"))
                {
                    Tagtoken tmp = new Tagtoken();
                    tmp.setXmlId(xn.Attributes["xml:id"].InnerText);
                    foreach (XmlNode tag in xn.SelectNodes("tags/tag"))
                        tmp.addTag(tag.Attributes["type"].InnerText, tag.InnerText);
                    this.tagtokens.Add(xn.Attributes["xml:id"].InnerText, tmp);
                }


                // domain parse
                foreach (XmlNode xn in doc.SelectNodes("//merge/drs/domain/dr"))
                {
                    Dr tmp = new Dr(xn.Attributes["name"].InnerText, xn.Attributes["label"].InnerText);
                    this.domain.Add(xn.Attributes["name"].InnerText, tmp);
                }

                // conds parse

                foreach (XmlNode x in doc.SelectNodes("//merge/drs/conds/cond"))
                {
                    string nodeName = "";
                    if (x.SelectSingleNode("pred") != null)
                    {
                        nodeName = "pred";
                    }
                    else if (x.SelectSingleNode("rel") != null)
                    {
                        nodeName = "rel";
                    }
                    else if (x.SelectSingleNode("named") != null)
                    {
                        nodeName = "named";
                    }
                    else if (x.SelectSingleNode("timex") != null)
                    {
                        nodeName = "timex";
                    }
                    else if (x.SelectSingleNode("card") != null)
                    {
                        nodeName = "card";
                    }
                    else if (x.SelectSingleNode("eq") != null)
                    {
                        nodeName = "eq";
                    }
                    else if (x.SelectSingleNode("not") != null)
                    {
                        nodeName = "not";
                    }

                    if (nodeName.Equals("not"))
                    {
                        // ma wewnątrz dodatowe domain i conds
                        Cond tmp = new Cond();
                        XmlNode current = x.SelectSingleNode(nodeName);

                        tmp.setNodeName(nodeName);

                        foreach (XmlNode b in current.SelectNodes("indexlist/index"))
                        {
                            Index elementTmp = new Index();
                            elementTmp.setPos(b.Attributes["pos"].InnerText);
                            elementTmp.setValue(b.InnerText);
                            tmp.addIndexElement(elementTmp);
                        }

                        // parsowanie dra

                        Not notTmp = new Not();

                        // drki
                        foreach (XmlNode xn in current.SelectNodes("drs/domain/dr"))
                        {
                            Dr drTmp = new Dr(xn.Attributes["name"].InnerText, xn.Attributes["label"].InnerText);
                            notTmp.addDomain(xn.Attributes["name"].InnerText, drTmp);
                        }

                        foreach (XmlNode x2 in current.SelectNodes("drs/conds/cond"))
                        {
                            string nodeName2 = "";

                            if (x2.SelectSingleNode("pred") != null)
                            {
                                nodeName2 = "pred";
                            }
                            else if (x2.SelectSingleNode("rel") != null)
                            {
                                nodeName2 = "rel";
                            }
                            else if (x2.SelectSingleNode("named") != null)
                            {
                                nodeName2 = "named";
                            }
                            else if (x2.SelectSingleNode("timex") != null)
                            {
                                nodeName2 = "timex";
                            }
                            else if (x2.SelectSingleNode("card") != null)
                            {
                                nodeName2 = "card";
                            }
                            else if (x2.SelectSingleNode("eq") != null)
                            {
                                nodeName2 = "eq";
                            }
                           

                            Cond tmp2 = new Cond();
                            XmlNode current2 = x2.SelectSingleNode(nodeName2);
                            tmp2.setNodeName(nodeName2);

                            foreach (XmlAttribute a2 in current2.Attributes)
                            {
                                tmp2.addArg(a2.Name, a2.InnerText);
                            }

                            foreach (XmlNode b2 in current2.SelectNodes("indexlist/index"))
                            {
                                Index elementTmp2 = new Index();
                                elementTmp2.setPos(b2.Attributes["pos"].InnerText);
                                elementTmp2.setValue(b2.InnerText);
                                tmp2.addIndexElement(elementTmp2);
                            }

                            notTmp.addCond(tmp2);

                        }

                        tmp.addNot(notTmp);
                        this.conds.Add(tmp);

                    }
                    else
                    {
                        Cond tmp = new Cond();
                        XmlNode current = x.SelectSingleNode(nodeName);
                        tmp.setNodeName(nodeName);

                        foreach (XmlAttribute a in current.Attributes)
                        {
                            tmp.addArg(a.Name, a.InnerText);
                        }

                        foreach (XmlNode b in current.SelectNodes("indexlist/index"))
                        {
                            Index elementTmp = new Index();
                            elementTmp.setPos(b.Attributes["pos"].InnerText);
                            elementTmp.setValue(b.InnerText);
                            tmp.addIndexElement(elementTmp);
                        }

                        this.conds.Add(tmp);
                        
                    }
                }

                
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "Id";
                c1.Binding = new Binding("Id");
                c1.Width = 110;
                dataGrid1.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Tok";
                c2.Width = 110;
                c2.Binding = new Binding("Tok");
                dataGrid1.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Pos";
                c3.Width = 110;
                c3.Binding = new Binding("Pos");
                dataGrid1.Columns.Add(c3);
                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Header = "Lemma";
                c4.Width = 110;
                c4.Binding = new Binding("Lemma");
                dataGrid1.Columns.Add(c4);
                DataGridTextColumn c5 = new DataGridTextColumn();
                c5.Header = "Namex";
                c5.Width = 110;
                c5.Binding = new Binding("Namex");
                dataGrid1.Columns.Add(c5);


                foreach (KeyValuePair<string, Tagtoken> x in this.tagtokens) {
                    
                    dataGrid1.Items.Add(new tagtokenItem() { 
                        Id = x.Value.getXmlId() , 
                        Tok = x.Value.getTag("tok"), 
                        Pos = x.Value.getTag("pos"),
                        Lemma = x.Value.getTag("lemma"),
                        Namex = x.Value.getTag("namex")
                    });    
                
                }


                // test

                foreach (Cond x in this.conds) 
                {
                    Console.WriteLine(x.getAllArgs());
                    if (x.getNot().getConds().Count() > 0)
                    {
                        foreach (Cond c in x.getNot().getConds()) {
                            Console.WriteLine(" + " + c.getAllArgs());
                        }
                    }
                }

            }
        }
    }
}
