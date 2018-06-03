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
    /// Manage_Emp.xaml 的交互逻辑
    /// </summary>
    public partial class Manage_Emp : Page
    {
        Sql helper = new Sql(); 
        Frame frame;
        ObservableCollection<EmpObj> list = new ObservableCollection<EmpObj>();
        public Manage_Emp(Frame frame)
        {

            InitializeComponent();
            this.frame = frame;
            Emp_DataGrid.ItemsSource = list;
        }
        private void EditAction(object sender, RoutedEventArgs e)
        {
            var index = Emp_DataGrid.SelectedIndex;
            Console.WriteLine(list[index]);
            MyDialog dialog = new MyDialog(list[index],false);
            dialog.ShowDialog();
            Console.WriteLine(dialog.DialogResult);
            if(dialog.DialogResult==true)
            {
                list[index] = dialog.getObjResult() as EmpObj;
            }
        }
        private void resetPasswordAction(object sender, RoutedEventArgs e)
        {
            var index = Emp_DataGrid.SelectedIndex;
            String idKey = list[index].工号;
            RePasswordDialog rep = new RePasswordDialog();
            rep.ShowDialog();
            if(rep.DialogResult==true)
            {
                String hashPassword = rep.GetHashPassword();
                //插入
            }
        }
        private void AddEmp(object sender, RoutedEventArgs e)
        {
            MyDialog dialog = new MyDialog(typeof(EmpObj));
            dialog.ShowDialog();
            ObservableCollection<EmpObj> newList = dialog.getObjectList();
            foreach(var item in newList)
            {
                int operatorCode = helper.ManEmp_add(item.工号.Trim(), item.姓名.Trim(), item.性别.ToString(), item.年龄.ToString(), item.基本工资.ToString(), md5helper.encrypt("123456"),0);
                if (operatorCode == 0)
                {
                    resetList();
                    return;
                }
            }
        }
        private void OnDataGridAutoGeneratingColumn(
            object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
            if(e.PropertyName=="Check")
            {
                e.Cancel = true;
            }
            
        }
        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            /*
            var item = (DataGridRow)sender;
            FrameworkElement objElement = Emp_DataGrid.Columns[1].GetCellContent(item);
            var box=VisualTreeHelper.GetChild(objElement, 0) as CheckBox;
            if (box != null)
            {
                box.IsChecked = !box.IsChecked;
                
            }
            */

        }
        private void Del_Selected(object sender, RoutedEventArgs e)
        {
            List<EmpObj> delList = new List<EmpObj>();
            foreach(var item in list)
            {
                if(item.Check==true)
                {
                    delList.Add(item);
                }
            }
            Console.WriteLine(delList.Count());
            foreach(var item in delList)
            {
                list.Remove(item);
            }
        }

        private void find_emp(object sender, RoutedEventArgs e)
        {
            if(QuerySelectBox.SelectedIndex == 0)
            {
                var rows = helper.find_id("Employee","emp_no", find.Text.Trim());
                resetList(rows);
            }
            else if(QuerySelectBox.SelectedIndex == 1)
            {
                var rows = helper.find_name("Employee", "emp_name", find.Text.Trim());
                resetList(rows);
            }
        }
        private void resetList(DataRow[] rows)
        {
            list.Clear();
            foreach (var r in rows)
            {
                Sex sex = (r["sex"].ToString().Trim() == "男" ? Sex.男 : Sex.女);
                list.Add(new EmpObj(r["emp_no"].ToString().Trim(), r["emp_name"].ToString().Trim(), sex, Int32.Parse(r["age"].ToString().Trim()), Double.Parse(r["bas_salary"].ToString().Trim())));
            }
        }
        private void resetList()
        {
            var rows = helper.find_all("Employee");
            resetList(rows);
        }

        private void ManEmp_del_bad(object sender, RoutedEventArgs e)
        {
            helper.ManEmp_del_bad();
            resetList();
        }
    }
}
