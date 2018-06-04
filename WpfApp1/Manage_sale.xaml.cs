using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    /// <summary>
    /// Manage_sale.xaml 的交互逻辑
    /// </summary>
    public partial class Manage_sale : Page
    {
        Sql helper = new Sql();
        Frame frame;
        public Manage_sale(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }
        private void OnDataGridAutoGeneratingColumn(
           object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
            if (e.PropertyName == "Check")
            {
                e.Cancel = true;
            }
        }
        private void Query(object sender, RoutedEventArgs e)
        {
            DataRow[] rows = null;
            switch (QuerySelectBox.SelectedIndex)
            {
                case 0:
                    rows = helper.ManOrder_max();
                    break;
                case 1:
                    rows = helper.ManOrder_get(mon.SelectedIndex+1);
                    break;
                case 2:
                    try {
                        float du = float.Parse(debu_text.Text.Trim());
                        rows = helper.ManOrder_empsal(emp_id_text.Text.Trim(),mon.SelectedIndex+1,du);
                    }catch(Exception exc) { Console.WriteLine(exc.Message); }
                    break;
                default:
                    return;
            }

            resetList(rows);
        }
        private void resetList(DataRow[] rows)
        {
            if (rows.Count() > 0)
            {
                DataTable dt = rows[0].Table;
                data.ItemsSource = dt.DefaultView;
            }
            else
                data.ItemsSource = null;

        }
        private void QuerySelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if(queryInfo!=null)
            {
                queryInfo.Visibility = Visibility.Hidden;
                int childrenCount = VisualTreeHelper.GetChildrenCount(queryInfo);
                for(int i=0;i<childrenCount;i++)
                {
                    var child = VisualTreeHelper.GetChild(queryInfo, i) as FrameworkElement;
                    child.Visibility = Visibility.Hidden;
                }
            }
            switch(QuerySelectBox.SelectedIndex)
            {
                case 2:
                    queryInfo.Visibility = Visibility.Visible;

                    int childrenCount = VisualTreeHelper.GetChildrenCount(queryInfo);
                    for (int i = 0; i < childrenCount; i++)
                    {
                        var child = VisualTreeHelper.GetChild(queryInfo, i) as FrameworkElement;
                        child.Visibility = Visibility.Visible;
                    }
                    break;
                case 1:
                    queryInfo.Visibility = Visibility.Visible;
                    mon.Visibility = mon_label.Visibility = Visibility.Visible;
                    break;
                
            }
        }
    }
}
