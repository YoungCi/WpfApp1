﻿using System;
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
        public MyDialog(ItemObj item,bool isAddable)
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
        }
        public MyDialog(Type type)
        {
            InitializeComponent();
            initList(type);
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
        }
    }
}
