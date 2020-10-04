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
    public partial class Form1 : Form
    {
        public static bool isLogin = false;
        int id;
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adp;
        DataSet dat;
        public static string username
        {
            get;
            set;
        }
        public Form1()
        {
            InitializeComponent();
            this.label1.BackColor = Color.Transparent;
            this.groupBox1.BackColor = Color.Transparent;
            this.groupBox2.BackColor = Color.Transparent;
            this.dataGridView1.BorderStyle = BorderStyle.None;
            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Text += "| 管理员：" + username;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridViewDataLoad();
            
            //this.stu_dentTableAdapter.Fill(this.students.Stu_dent);

        }

        private void DataGridViewDataLoad()
        {
            String sql1 = "select * from Stu_dent order by StuNum";
            adp = new SqlDataAdapter(sql1,conn);
            dat = new DataSet();
            adp.Fill(dat);
            dataGridView1.DataSource = dat.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string num = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();
            String sql4 = "Select * from Stu_dent where 1=1";
            if(!String.IsNullOrEmpty(num))
            {
                sql4 += " and StuNum=" + num;
            }
            if(!String.IsNullOrEmpty(name))
            {
                sql4 += " and StuName like '%" + name + "%'";
            }
            sql4 += " order by StuNum";
            adp = new SqlDataAdapter(sql4, conn);
            dat = new DataSet();
            adp.Fill(dat);
            dataGridView1.DataSource = dat.Tables[0];
            
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
            AddStudent addStudent = new AddStudent();
            addStudent.StartPosition = FormStartPosition.CenterScreen;
            //addStudent.Show();
            addStudent.ShowDialog();
            DataGridViewDataLoad();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请在学生信息显示框中双击所要删除学生所在的那一行即可！！！","提示");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请在学生信息显示框中单击所要删修改学生所在的那一行的任意文字区域即可！", "提示");
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定删除该学生信息？", "删除", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                Console.WriteLine(id);
                string sql2 = "delete from Stu_dent where ID=" + id;
                cmd = new SqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();

            }
            DataGridViewDataLoad();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            UpdateStudent updatestudent = new UpdateStudent(id);
            updatestudent.StartPosition = FormStartPosition.CenterScreen;
            updatestudent.ShowDialog();
            DataGridViewDataLoad();
        }
    }
}
