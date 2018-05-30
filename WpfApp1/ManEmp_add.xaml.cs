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
    /// ManEmp_add.xaml 的交互逻辑
    /// </summary>
    public partial class ManEmp_add : Page
    {
        Frame frame;
        public ManEmp_add()
        {
            InitializeComponent();
        }
        public ManEmp_add(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }
    }
}
