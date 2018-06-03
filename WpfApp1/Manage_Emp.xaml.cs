using DataGridDemo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        Frame frame;
        ObservableCollection<EmpObj> list = new ObservableCollection<EmpObj>();
        public Manage_Emp(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            Emp_DataGrid.ItemsSource = list;
            list.Add(new EmpObj("C00001", "叶加博", Sex.男, 22, -100));
            list.Add(new EmpObj("C00002", "杨思旖", Sex.女, 21, 100));
           
        }
        private void EditAction(object sender, RoutedEventArgs e)
        {
            var index = Emp_DataGrid.SelectedIndex;
            DataGridRow rowContainer = (DataGridRow)Emp_DataGrid.ItemContainerGenerator.ContainerFromIndex(index);

            var box = Emp_DataGrid.Columns[1].GetCellContent(rowContainer) as CheckBox;
            box.IsChecked = !box.IsChecked;
            Console.WriteLine(list[index]);
            MyDialog dialog = new MyDialog(list[index],false);
            dialog.ShowDialog();
            Console.WriteLine(dialog.DialogResult);
            if(dialog.DialogResult==true)
            {
                list[index] = dialog.getObjResult() as EmpObj;
            }
        }
        private void AddEmp(object sender, RoutedEventArgs e)
        {

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
    }
}
