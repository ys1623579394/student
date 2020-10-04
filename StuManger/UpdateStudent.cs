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
    public partial class UpdateStudent : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adp;
        SqlDataReader reader;
        DataSet dat;
        public int id;
        public UpdateStudent(int id)
        {
            this.id = id;
            string ss = ConfigurationManager.ConnectionStrings["Stu"].ToString();
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
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
        private void UpdateStudent_Load(object sender, EventArgs e)
        {
            cmd = conn.CreateCommand();
            string sql1 = "select * from Stu_dent where ID=" + id;
            cmd.CommandText = sql1;
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                textBox1.Text = reader.GetString(1);
                textBox2.Text = reader.GetString(2);
                textBox3.Text = reader.GetInt32(3).ToString();
                if(reader.GetString(4) == "男")
                {
                    this.radioButton2.Checked = true;
                }
                else
                {
                    this.radioButton1.Checked = true;
                }
                textBox5.Text = reader.GetString(5);
                textBox6.Text = reader.GetString(6);
            }

            reader.Close();
            
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
            string sql = string.Format("update Stu_dent set StuName='{0}',StuAge={1},StuSex='{2}',StuClass='{3}',StuPhone='{4}' where StuNum='{5}'", StuName, StuAge, StuSex, StuClass, StuPhone, StuNum);
            cmd = new SqlCommand(sql, conn);
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("修改成功！");
            }
            else
            {
                MessageBox.Show("修改失败！");
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = conn.CreateCommand();
            string sql1 = "select * from Stu_dent where ID=" + id;
            cmd.CommandText = sql1;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                textBox1.Text = reader.GetString(1);
                textBox2.Text = reader.GetString(2);
                textBox3.Text = reader.GetInt32(3).ToString();
                if (reader.GetString(4) == "男")
                {
                    this.radioButton2.Checked = true;
                }
                else
                {
                    this.radioButton1.Checked = true;
                }
                textBox5.Text = reader.GetString(5);
                textBox6.Text = reader.GetString(6);
            }

            reader.Close();
        }
    }
}
