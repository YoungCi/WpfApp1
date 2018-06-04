using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataGridDemo
{
    /// <summary>
    /// MyDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MyDialog : Window
    {
        dynamic content;
        bool canShowId=false;
        public MyDialog(ItemObj item,bool isAddable,String title)
        {
            InitializeComponent();
            if(!isAddable)
            {
                data.CanUserAddRows = false;
                Height = 150;
            }
            initList(item.GetType());
            dynamic trueItem = item.Clone();
            content.Add(trueItem);
            SetTitle(title);
        }
        public MyDialog(Type type,String title)
        {
            InitializeComponent();
            initList(type);
            canShowId = true;
            SetTitle(title);
        }
        private void SetTitle(String title)
        {
            Title = title;
        }
        private void initList(Type type)
        {
            Type genericClass = typeof(ObservableCollection<>);
            // MakeGenericType is badly named
            Type constructedClass = genericClass.MakeGenericType(type);
            content = Activator.CreateInstance(constructedClass);
            data.ItemsSource = content;
        }
        public ItemObj getObjResult()
        {
            return content[0];
        }
        public dynamic getObjectList()
        {
            return content;
        }
        private void Submit(object sender, RoutedEventArgs e)
        {
            
            DialogResult = true;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void OnDataGridAutoGeneratingColumn(
            object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
            if (e.PropertyName == "Check")
            {
                e.Cancel = true;
            }
            if(canShowId==false)
            {
                if (e.PropertyName == "工号" ||
                   e.PropertyName == "货物号" ||
                   e.PropertyName == "订单号")
                    e.Cancel = true;
            }
        }
    }
}
