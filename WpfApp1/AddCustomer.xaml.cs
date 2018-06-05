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
    /// AddCustomer.xaml 的交互逻辑
    /// </summary>
    public partial class AddCustomer : Page
    {
        Sql helper = new Sql();
        Frame frame;
        ObservableCollection<CustObj> list = new ObservableCollection<CustObj>();
        public AddCustomer(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            Customer_DataGrid.ItemsSource = list;
            resetList();
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
        private void add_cust(object sender, RoutedEventArgs e)
        {
            MyDialog dialog = new MyDialog(typeof(CustObj), "添加客户");
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            ObservableCollection<CustObj> newList = dialog.getObjectList();
            List<String> errorList = new List<string>();
            foreach (var item in newList)
            {
                int operatorCode = helper.Cust_add(item.客户号.Trim(), item.姓名.Trim(), item.电话.Trim(), item.地址.Trim());
                if (operatorCode == 0)
                {
                    resetList();
                    return;
                }
                else
                {
                    errorList.Add(item.客户号 + " : " + Sql.ErrorCodeToString(operatorCode));
                }
            }
            if (errorList.Count != 0)
            {
                MessageBox.Show(MainWindow.MakeErrorString(errorList), "添加客户出错");
            }
        }
        private void resetList(DataRow[] rows)
        {
            list.Clear();
            foreach (var r in rows)
            {
                list.Add(new CustObj(r["cus_no"].ToString().Trim(),
                                     r["cus_name"].ToString().Trim(),
                                      r["tel"].ToString().Trim(),
                                       r["addr"].ToString().Trim()));
            }
        }
        private void resetList()
        {
            var rows = helper.find_all("customer");
            resetList(rows);
        }

    }
}
