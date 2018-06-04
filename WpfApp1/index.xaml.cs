using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Data;

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

        void PageLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(usernameText);
            while (frame.CanGoBack)
                frame.RemoveBackEntry();
        }
        private void login(object sender, RoutedEventArgs e)
        {
            Sql helper;
            helper = new Sql();
            string username = usernameText.Text.Trim();
            string password = passwordText.Password.Trim();
            DataRow[] d= helper.check_in(username);
            if (d.Count() == 0)
            {
                checkErrorLabel.Visibility = Visibility.Visible;
                checkErrorLabel.Content = "登录名不存在！请重新输入！";
                return;
            }
            string pa=null;
            int p=1121212;
            foreach(var v in d)
             {
                pa = v["passward"].ToString().Trim();
                p = (int)v["property"];
            }
            if (md5helper.check(password, pa) == false)
            {
                checkErrorLabel.Visibility = Visibility.Visible;
                checkErrorLabel.Content = "用户密码错误！请重新输入！";
                return;
            }
            checkErrorLabel.Visibility = Visibility.Hidden;
            if (p == 0)
            {
                frame.Content = new Employee(frame);
            }
            else if (p == 1)
            {
                frame.Content = new Manager(frame);
            }
        }
    }
}
