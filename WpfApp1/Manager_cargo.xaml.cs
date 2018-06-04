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
    /// Manager_cargo.xaml 的交互逻辑
    /// </summary>
    public partial class Manager_cargo : Page
    {
        Sql helper = new Sql();
        Frame frame;
        public Manager_cargo(Frame frame)
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
        private void ManCargo_find(object sender, RoutedEventArgs e)
        {
            DataRow[] rows = null;
            switch (QuerySelectBox.SelectedIndex)
            {
                case 0:
                    rows = helper.ManCargo_sale_time(mon.SelectedIndex + 1, car_no.Text.Trim());
                    break;
                case 1:
                    rows = helper.ManCargo_sale_cust();
                    break;
                case 2:
                    rows = helper.ManCargo_sale_get();
                    break;
                case 3:
                    if (amount.Text.Trim().Length == 0) amount.Text = "0";
                    rows = helper.ManCargo_toadd(Int32.Parse(amount.Text.ToString().Trim()));
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
            if (query_0 != null)
                query_0.Visibility = Visibility.Hidden;
            if (query_1 != null)
                query_1.Visibility = Visibility.Hidden;
            switch (QuerySelectBox.SelectedIndex)
            {
                case 0:
                    if (query_0 != null)
                        query_0.Visibility = Visibility.Visible;
                    break;
                case 3:
                    if (query_1 != null)

                        query_1.Visibility = Visibility.Visible;
                    break;
            }
        }
    }

}
