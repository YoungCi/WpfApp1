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
    public enum UserType { 员工,管理员 }
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
    [Serializable]
    public class CarObj:ItemObj
    {
        String id;
        String name;
        Double pur_price;
        Double sale_price;
        int inventory;

        public string 货物号 { get => id; set => id = value; }
        public string 名称 { get => name; set => name = value; }
        public double 进价 { get => pur_price; set => pur_price = value; }
        public double 售价 { get => sale_price; set => sale_price = value; }
        public int 库存量 { get => inventory; set => inventory = value; }
        public CarObj() { }
        public CarObj(string id,string name,double pur_price,double sale_price,int inventory)
        {
            货物号 = id;
            名称 = name;
            进价 = pur_price;
            售价 = sale_price;
            库存量 = inventory;
        }
    }
    public class OrderObj:ItemObj
    {
        private string sale_no;
        private string sale_date;
        private string car_no;
        private string cus_no;
        private string emp_no;
        private int qty;
        public string 订单号 { get => sale_no; set => sale_no = value; }
        public string 销售日期 { get => sale_date; set => sale_date = value; }
        public string 货物编号{ get =>  car_no; set =>  car_no = value; }
        public string  客户编号{ get => cus_no; set => cus_no = value; }
        public string 员工编号{ get =>  emp_no;set => emp_no= value; }
        public int 货物数量 { get => qty; set => qty = value; }
        public OrderObj() { }
        public OrderObj(string order_no,string sale_date, string car_no, string cus_no, string emp_no, int qty)
        {
            订单号 = order_no;
            销售日期 = sale_date;
            货物编号 = car_no;
            客户编号 = cus_no;
            员工编号 = emp_no;
            货物数量 = qty;
        }
    }
}
