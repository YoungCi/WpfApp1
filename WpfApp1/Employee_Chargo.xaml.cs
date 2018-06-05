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
    /// Employee_Chargo.xaml 的交互逻辑
    /// </summary>
    public partial class Employee_Chargo : Page
    {
        Sql helper = new Sql();
        Frame frame;
        ObservableCollection<CarObj> list = new ObservableCollection<CarObj>();
        public Employee_Chargo(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            Chargo_DataGrid.ItemsSource = list;
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
        
        private void find_chargo(object sender, RoutedEventArgs e)
        {
            if (QuerySelectBox.SelectedIndex == 0)
            {
                var rows = helper.find_id("cargo", "car_no", find.Text.Trim());
                resetList(rows);
            }
            else if (QuerySelectBox.SelectedIndex == 1)
            {
                var rows = helper.find_name("cargo", "car_name", find.Text.Trim());
                resetList(rows);
            }
        }
        private void Del_Selected(object sender, RoutedEventArgs e)
        {
            List<CarObj> delList = new List<CarObj>();
            foreach (var item in list)
            {
                if (item.Check == true)
                {
                    helper.del_no("cargo", "car_no", item.货物号);
                }
            }
            resetList();
        }
        private void resetList(DataRow[] rows)
        {
            list.Clear();
            foreach (var r in rows)
            {
                list.Add(new CarObj(r["car_no"].ToString().Trim(),
                                     r["car_name"].ToString().Trim(),
                                     Double.Parse(r["pur_price"].ToString().Trim()),
                                     Double.Parse(r["sale_price"].ToString().Trim()),
                                     Int32.Parse(r["inventory"].ToString().Trim())));
            }
        }
        private void resetList()
        {
            var rows = helper.find_all("cargo");
            resetList(rows);
        }

        private void AddChargo(object sender, RoutedEventArgs e)
        {
            MyDialog dialog = new MyDialog(typeof(CarObj),"添加货物信息");
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;

            ObservableCollection<CarObj> newList = dialog.getObjectList();
            List<String> errorList = new List<string>();
            foreach (var item in newList)
            {
                int operatorCode = helper.Cargo_add(item.货物号.Trim(), item.名称.Trim(), item.进价.ToString(), item.售价.ToString(), item.库存量.ToString());
                if (operatorCode == 0)
                {
                    resetList();
                    return;
                }
                else
                {
                    errorList.Add(item.货物号 + " : " + Sql.ErrorCodeToString(operatorCode));
                }
            }
            if(errorList.Count!=0)
            {
                MessageBox.Show(MainWindow.MakeErrorString(errorList), "添加货物出错");
            }
        }
        private void EditAction(object sender, RoutedEventArgs e)
        {
            var index = Chargo_DataGrid.SelectedIndex;
            Console.WriteLine(list[index]);
            MyDialog dialog = new MyDialog(list[index], false,"修改货物信息");
            dialog.ShowDialog();
            if (dialog.DialogResult == false) return;
            
                //list[index] = dialog.getObjResult() as EmpObj;
                var item = dialog.getObjResult() as CarObj;
                var key = list[index].货物号;
                if (item.货物号 != key)
                {
                    helper.Cargo_alter(key, 6, item.货物号, 0);
                    key = item.货物号;
                }
                helper.Cargo_alter(key, 1, item.名称, 0);
                helper.Cargo_alter(key, 2, item.进价.ToString(), 1);
                helper.Cargo_alter(key, 3, item.售价.ToString(), 1);
                helper.Cargo_alter(key, 5, item.库存量.ToString(), 1);
                resetList();
            
        }
    }
}
