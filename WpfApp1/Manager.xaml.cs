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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Manager : Page
    {
        Frame frame;
        public Manager()
        {
            InitializeComponent();
        }
        public Manager(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void ManEmp(object sender, RoutedEventArgs e)
        {
            
        }

        private void ManCrago(object sender, RoutedEventArgs e)
        {
        }
    }
}
