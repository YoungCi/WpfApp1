using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class md5helper
    {
        static MD5 md5 = new MD5CryptoServiceProvider();//加密器
        public static bool check(string password,string cryptograph)
        {
            //验证
            if (cryptograph == encrypt(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string encrypt(string password)
        {
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(password));//加密成字节流
            string encrypt = BitConverter.ToString(output);//转化为字符串存储
            return encrypt;
        }
    }
    
}
