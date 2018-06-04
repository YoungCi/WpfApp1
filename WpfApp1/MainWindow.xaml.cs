using System;
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
        private void goBack(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DragAction(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void home(object sender, RoutedEventArgs e)
        {
            frame.Content = new index(frame);
        }
        public static String MakeErrorString(List<String> list)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var i in list)
            {
                builder.AppendLine(i);
            }
            return builder.ToString();
        }
    }
}
