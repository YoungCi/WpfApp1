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
    /// ManCargo_toadd.xaml 的交互逻辑
    /// </summary>
    public partial class ManCargo_toadd : Page
    {
        Frame frame;
        public ManCargo_toadd()
        {
            InitializeComponent();
        }
        public ManCargo_toadd(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }
    }
}
