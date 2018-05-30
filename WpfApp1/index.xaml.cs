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
    /// index.xaml 的交互逻辑
    /// </summary>
    public partial class index : Page
    {
        Frame frame;
        public index(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void Manager(object sender, RoutedEventArgs e)
        {
            frame.Content = new Manager(frame);
        }

        private void Employee(object sender, RoutedEventArgs e)
        {
            frame.Content = new Employee(frame);
        }
    }
}
