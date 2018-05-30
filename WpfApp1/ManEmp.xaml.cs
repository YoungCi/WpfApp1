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
    /// ManEmp.xaml 的交互逻辑
    /// </summary>
    public partial class ManEmp : Page
    {
        Frame frame;
        public ManEmp()
        {
            InitializeComponent();
        }
        public ManEmp(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void ManEmp_add(object sender, RoutedEventArgs e)
        {
            frame.Content = new ManEmp_add(frame);
        }

        private void ManEmp_del(object sender, RoutedEventArgs e)
        {
            frame.Content = new ManEmp_del(frame);
        }

        private void ManEmp_find(object sender, RoutedEventArgs e)
        {
            frame.Content = new ManEmp_find(frame);
        }

        private void ManEmp_alter(object sender, RoutedEventArgs e)
        {
            frame.Content = new ManEmp_alter(frame);
        }

        private void ManEmp_salary(object sender, RoutedEventArgs e)
        {
            frame.Content = new ManEmp_salary(frame);
        }
    }
}
