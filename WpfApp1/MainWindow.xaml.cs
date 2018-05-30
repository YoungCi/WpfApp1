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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Content = new index(frame);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("***");
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("222");
            Environment.Exit(0);
        }

        private void DragAction(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
