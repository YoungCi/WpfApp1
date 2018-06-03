using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataGridDemo
{
    [Serializable]
    public class ItemObj:ICloneable
    {
        private bool check;
        public ItemObj()
        {
            Check = false;
        }

        public bool Check { get; set; }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                throw new Exception("Not Serializable!");
            }
        }
    }
    public enum Sex { 男,女}
    public enum UserType { 管理员,员工}
    [Serializable]
    public class EmpObj:ItemObj
    {
        private string id;
        private string name;
        private Sex sex;
        private int age;
        private double baseSaray;
        private UserType userType;
        public string 工号 { get => id; set => id = value; }
        public string 姓名 { get => name; set => name = value; }
        public Sex 性别 { get => sex; set => sex = value; }
        public int 年龄 { get => age; set => age = value; }
        public double 基本工资 { get => baseSaray; set => baseSaray = value; }
        public UserType 用户类型 { get => userType; set => userType = value; }

        public EmpObj(){}
        public EmpObj(string id,string name,Sex sex,int age,double baseSaray,UserType userType)
        {
            工号 = id;
            姓名 = name;
            性别 = sex;
            年龄 = age;
            基本工资 = baseSaray;
            用户类型 = userType;
        }
    }
}
