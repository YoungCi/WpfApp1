﻿using System;
using System.Collections.Generic;
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
    /// Employee.xaml 的交互逻辑
    /// </summary>
    public partial class Employee : Page
    {
        Frame frame;
        public Employee()
        {
            InitializeComponent();
        }
        public Employee(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }
        private void CargoAction(object sender, RoutedEventArgs e)
        {
            frame.Content = new Employee_Chargo(frame);
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            frame.Content = new AddCustomer(frame);
        }
        private void Emp_order(object sender, RoutedEventArgs e)
        {
             frame.Content = new Emp_order(frame);
        }
    }
}
