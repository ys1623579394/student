using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StuManger
{
    public partial class AddStudent : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        public AddStudent()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            string ss = ConfigurationManager.ConnectionStrings["Stu"].ToString();
            //建立数据库连接
            conn = new SqlConnection(ss);
            try
            {
                //开启连接           
                conn.Open();
                // MessageBox.Show("数据库连接成功!");           
            }
            catch (Exception)
            {
                MessageBox.Show("数据库连接失败!");
            }

        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                this.Close();//esc关闭窗体
                break;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string StuNum = textBox1.Text.Trim();
            string StuName = textBox2.Text.Trim();
            int StuAge;
            Int32.TryParse(textBox3.Text.Trim(), out StuAge);
            string StuClass = textBox5.Text.Trim();
            string StuPhone = textBox6.Text.Trim();
            string StuSex = radioButton1.Checked ? "男" : "女";
            if (String.IsNullOrEmpty(StuNum))
            {
                MessageBox.Show("学号不能为空！");
            }
            if (String.IsNullOrEmpty(StuName))
            {
                MessageBox.Show("姓名不能为空！");
            }
            
            if (String.IsNullOrEmpty(StuClass))
            {
                MessageBox.Show("班级不能为空！");
            }
            if (String.IsNullOrEmpty(StuPhone))
            {
                MessageBox.Show("联系方式不能为空！");
            }
            string sql = string.Format("insert into Stu_dent values ('{0}','{1}','{2}','{3}','{4}','{5}')", StuNum, StuName,StuAge,StuSex, StuClass, StuPhone );
            cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
            this.Close();
        }
    }
}
