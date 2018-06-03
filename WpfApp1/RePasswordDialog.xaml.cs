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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// RePasswordDialog.xaml 的交互逻辑
    /// </summary>
    public partial class RePasswordDialog : Window
    {
        public RePasswordDialog()
        {
            InitializeComponent();
        }
        public String GetHashPassword()
        {
            return md5helper.encrypt(rePasswordBox.Password);
        }
        private void Submit(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
