using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WpfApp1
{
    class Sql
    {
        private SqlConnection connection;
        public Sql()
        {
            initSqlConnection();
        }
        private void initSqlConnection()
        {
            connection = new SqlConnection();
            String SqlIP = "127.0.0.1";//本地SQL服务器地址
            String InstanceName = "SQLEXPRESS";//SQL实例名
            String DateBaseName = "Test";//数据库名称
            String UserName = "MuYi41";//登录用户名
            String PassWord = "970814";//登录密码
            //写入连接所使用的字符串信息
            connection.ConnectionString = String.Format("server = {0}; database = {2}; uid = {3}; pwd = {4}", SqlIP, InstanceName, DateBaseName, UserName, PassWord);
            //Console.WriteLine(connection.ConnectionString);
        }

        //增加
        public int ManEmp_add(string id, string name, string sex, string age, string bas_salary, string password, int porperty)
        {
            //String insertSql = String.Format("INSERT INTO employee VALUES ('{0}', '{1}','{2}',{3},{4},'{password}',{porperty})", id, name, sex, age, bas_salary);写错了
            String insertSql = $"INSERT INTO employee VALUES ('{id}', '{name}','{sex}',{age},{bas_salary},'{password}',{porperty})";
            return ExecuteNonQuery(insertSql);
        }
        public DataRow[] check_in(string id)
        {
            String checkSql = String.Format("select passward,property from employee where emp_no = '{0}'", id);
            return ExecutQuery(checkSql);
        }
        public int Cargo_add(string cargo_no, string name, string pur_price, string sale_price, string inventory)
        {
            String insertSql = String.Format("INSERT INTO cargo VALUES ('{0}', '{1}',{2},{3},{4})", cargo_no, name, pur_price, sale_price, inventory);
            SqlCommand cmd = new SqlCommand(insertSql, connection);
            return ExecuteNonQuery(insertSql);
        }
        public int Order_add(string order_no, string order_time, string cargo_no, string cus_no, string emp_no, string qty)
        {
            String insertSql = $"INSERT INTO sale_item VALUES('{order_no}', '{order_time}', '{cargo_no}', '{cus_no}','{emp_no}', {qty})";
            return ExecuteNonQuery(insertSql);
        }
        public int Cust_add(string cust_no, string cust_name, string tel, string addr)
        {
            String insertSql = $"INSERT INTO customer VALUES('{cust_no }', '{cust_name}','{tel}','{addr}')";
            return ExecuteNonQuery(insertSql);
        }

        //删除
        public int ManEmp_del_bad()
        {
            String deleteSql = String.Format("DELETE FROM employee WHERE emp_no NOT IN (SELECT emp_no FROM sale_item)");
            return ExecuteNonQuery(deleteSql);
        }
        public int del_no(string s, string sn, string num)
        {
            String deleteSql = $"DELETE FROM {s} WHERE {sn} LIKE '{num}'";
            return ExecuteNonQuery(deleteSql);
        }

        //查找
        public DataRow[] find_all(string s)
        {
            String querySql = $"SELECT * FROM {s}";
            return ExecutQuery(querySql);
        }
        public DataRow[] find_id(string s, string sn, string id)
        {
            String querySql = $"SELECT * FROM {s} WHERE {sn} LIKE '{id}'";
            return ExecutQuery(querySql);
        }
        public DataRow[] find_name(string s, string sn, string name)
        {
            StringBuilder bu = new StringBuilder();
            bu.Append("%");
            foreach (var c in name)
            {
                if (c == ' ')
                {
                    continue;
                }
                bu.Append(c);
                bu.Append("%");
            }
            name = bu.ToString();
            String querySql = $"SELECT * FROM {s} WHERE {sn} LIKE '{name}'";
            return ExecutQuery(querySql);
        }
        public DataRow[] ManCargo_sale_time(int mon, string car_id)
        {
            string querySaleTimeSql = $"select cargo.car_no,car_name,sum(qty) 销售数量 from cargo, sale_item where cargo.car_no = sale_item.car_no and sale_item.car_no = '{car_id}' and month(sale_date) = {mon} group by cargo.car_no,car_name,month(sale_date)";
            return ExecutQuery(querySaleTimeSql);
        }
        public DataRow[] ManCargo_sale_cust()
        {
            string querySaleCust = $"select cus_no,cus_name,car_name,最大值 from (select C.cus_no 客, D.car_no 货, C.最大值 from(select B.cus_no, max(B.总数量) 最大值 from (select cus_no, car_no, sum(qty) 总数量 from sale_item group by cus_no, car_no) B group by B.cus_no) C, (select cus_no, car_no, sum(qty) 总数量 from sale_item group by cus_no, car_no) D where D.总数量 = C.最大值 and D.cus_no = C.cus_no and D.car_no = D.car_no)E,cargo,customer where cargo.car_no = E.货 and customer.cus_no = E.客";
            return ExecutQuery(querySaleCust);
        }
        public DataRow[] ManCargo_sale_get()
        {
            string querySaleGet = $"select car_name,inventory*(sale_price - pur_price) 盈利 from cargo order by 盈利 DESC";
            return ExecutQuery(querySaleGet);
        }
        public DataRow[] ManCargo_toadd(int minp)
        {
            string queryToAdd = $"select car_name,inventory from cargo where inventory < {minp} order by inventory";
            return ExecutQuery(queryToAdd);
        }
        public DataRow[] ManOrder_max()
        {
            string quertMax = $"select sale_no,sale_date,cargo.car_name,cus_no,emp_no,qty,qty*(sale_price-pur_price) 订单金额 from sale_item,cargo where cargo.car_no = sale_item.car_no and qty*(sale_price-pur_price) = (select max(qty*(sale_price-pur_price)) 最大金额 from sale_item,cargo where cargo.car_no = sale_item.car_no)";
            return ExecutQuery(quertMax);
        }
        public DataRow[] ManOrder_get(int mon)
        {
            string queryOrderGet = $"select month(sale_date) 月份,sum(qty * (sale_price - pur_price)) 盈利 from sale_item,cargo where cargo.car_no = sale_item.car_no and month(sale_date) = {mon} group by month(sale_date)";
            return ExecutQuery(queryOrderGet);
        }
        public DataRow[] ManOrder_empsal(string emp_id, int mon, float dedu)
        {
            string queryEmpsal = $"select employee.emp_no 员工, emp_name 姓名,month(sale_date) 月份,sum(qty * (sale_price - pur_price) * {dedu}) 提成,(bas_salary + sum(qty * (sale_price - pur_price) * {dedu})) 工资 from employee,cargo,sale_item where employee.emp_no = sale_item.emp_no and cargo.car_no = sale_item.car_no and MONTH(sale_date) = {mon} and employee.emp_no = '{emp_id}' group by employee.emp_no,emp_name,month(sale_date),bas_salary";
            return ExecutQuery(queryEmpsal);
        }

        //修改 ty==0 string else int
        public void ManEmp_alter(string id, int Ch, string edit, int ty)
        {
            if (ty == 0)
                edit = $"'{edit}'";
            String[] na = new string[] { "emp_name", "sex", "age", "tot_amt", "bas_salary","passward","property","emp_no" };
            String alterSql = $"UPDATE employee SET {na[Ch - 1]} = {edit} WHERE emp_no = '{id}'";
            ExecuteNonQuery(alterSql);
        }
        public void Cargo_alter(string id, int Ch, string edit, int ty)
        {
            if (ty == 0)
                edit = $"'{edit}'";
            String[] na = new string[] { "car_name", "pur_price", "sale_price", "tot_amt", "inventory","car_no" };
            String alterSql = $"UPDATE cargo SET {na[Ch - 1]} = {edit} WHERE car_no = '{id}'";
            ExecuteNonQuery(alterSql);
        }
        public void ManOrder_alter(string id, int Ch, string edit, int ty)
        {
            if (ty == 0)
                edit = $"'{edit}'";
            String[] na = new string[] { "sale_date", "car_no", "cus_no", "emp_no", "qty" };
            String alterSql = $"UPDATE sale_item SET {na[Ch - 1]} = {edit} WHERE sale_no = '{id}'";
            ExecuteNonQuery(alterSql);
        }

        private DataRow[] ExecutQuery(string s)
        {
            connection.Open();
            //构造一个接收数据的容器
            DataTable dt = new DataTable();
            //构造一个数据适配器
            SqlDataAdapter adapter = new SqlDataAdapter(s, connection);
            //发起查询，并把数据填充到dt中
            adapter.Fill(dt);
            //dt是一个结果集，本身不能去遍历它，我们通过Select方法取出行的数组
            DataRow[] data = dt.Select();
            connection.Close();
            return data;
        }
        private int ExecuteNonQuery(string s)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(s, connection);
            int delLen = 0;
            int operatorCode = 0;
            try
            {
                delLen = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (e.Number == 2627)//主码已存在
                {
                    operatorCode = 2;
                }
                if (e.Number == 515)//主码为空
                {
                    operatorCode = 3;
                }
                if (e.Number == 547)//不符合约束
                {
                    operatorCode = 4;
                }
                if (e.Number == 213)//列名或所提供值的数目与表定义不匹配
                {
                    operatorCode = 5;
                }
                if (e.Number == 102)//插入数据时有语法错误。
                {
                    operatorCode = 6;
                }
                if (e.Number == 207)//输入的值非法
                {
                    operatorCode = 7;
                }
                if (e.Number == 242)//输入的时间不合法
                {
                    operatorCode = 8;
                }
            }
            connection.Close();
            if (operatorCode == 0 && delLen == 0)//受影响行数为0
            {
                operatorCode = 1;
            }
            return operatorCode;
        }
    }
}
