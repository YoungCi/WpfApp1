using DataGridDemo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Emp_order.xaml 的交互逻辑
    /// </summary>
    public partial class Emp_order : Page
    {
        Sql helper = new Sql();
        Frame frame;
        ObservableCollection<OrderObj> list = new ObservableCollection<OrderObj>();
        public Emp_order()
        {
            InitializeComponent();
        }
        public Emp_order(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            resetList();
            Order_DataGrid.ItemsSource = list;
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
        private void EmpOrder_find(object sender, RoutedEventArgs e)
        {
            var rows = helper.find_id("sale_item", "sale_no", find.Text.Trim());
            resetList(rows);
        }

        private void editOrder(object sender, RoutedEventArgs e)
        {
            var index = Order_DataGrid.SelectedIndex;
            Console.WriteLine(list[index]);
            MyDialog dialog = new MyDialog(list[index], false,"修改订单信息");
            dialog.ShowDialog();
            Console.WriteLine(dialog.DialogResult);
            if (dialog.DialogResult == true)
            {
                var item = dialog.getObjResult() as OrderObj;
                var key = list[index].订单号;
                if (item.订单号 != key)
                {
                    helper.ManOrder_alter(key, 8, item.订单号, 0);
                    key = item.订单号;
                }
                helper.ManOrder_alter(key, 1, item.销售日期, 0);
                helper.ManOrder_alter(key, 2, item.货物编号, 0);
                helper.ManOrder_alter(key, 3, item.客户编号, 0);
                helper.ManOrder_alter(key, 4, item.员工编号, 0);
                helper.ManOrder_alter(key, 5, item.货物数量.ToString().Trim(), 1);
                resetList();
            }
        }

        private void add_order(object sender, RoutedEventArgs e)
        {
            MyDialog dialog = new MyDialog(typeof(OrderObj),"添加订单信息");
            dialog.ShowDialog();
            ObservableCollection<OrderObj> newList = dialog.getObjectList();
            foreach (var item in newList)
            {
                int operatorCode = helper.Order_add(item.订单号.Trim(), item.销售日期.ToString().Trim(),item.货物编号.Trim(),item.客户编号.Trim(),item.员工编号.Trim(),item.货物数量.ToString().Trim());
                if (operatorCode == 0)
                {
                    resetList();
                    return;
                }
                else
                {
                    MessageBox.Show("您增加的数据中包含不符合规则的值！");
                }
            }
        }
        private void resetList(DataRow[] rows)
        {
            list.Clear();
            foreach (var r in rows)
            {
                list.Add(new OrderObj(r["sale_no"].ToString().Trim(), r["sale_date"].ToString().Trim(), r["car_no"].ToString().Trim(), r["cus_no"].ToString().Trim(), r["emp_no"].ToString().Trim(), Int32.Parse(r["qty"].ToString().Trim())));
            }
        }
        private void resetList()
        {
            DataRow[] rows = helper.find_all("sale_item");
            resetList(rows);
        }
    }

    }
