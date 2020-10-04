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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.label3.Parent = this;
            this.label1.BackColor = Color.Transparent;
            this.label2.BackColor = Color.Transparent;
            this.label3.BackColor = Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ss = ConfigurationManager.ConnectionStrings["Stu"].ToString();
            SqlConnection conn = new SqlConnection(ss);
            try
            {
                 //开启连接           
                 conn.Open();           
                // MessageBox.Show("数据库连接成功!");           
            }
            catch (Exception)
            {
                 //MessageBox.Show("数据库连接失败!");
            }         
            String uname = txtName.Text.Trim();
            string pword = txtPass.Text.Trim();
            if(uname == ""|| pword == "")
            {
                MessageBox.Show("请输入用户名或密码！");
                return;
            }
            else
            {
                SqlCommand cmd = conn.CreateCommand();
                SqlDataAdapter adp = new SqlDataAdapter();
                string sql2 = "select * from account where name='"+ uname + " 'and pass='" + pword + " ' ";
                cmd.CommandText = sql2;
                adp.SelectCommand = cmd;
                DataSet dat = new DataSet();
                adp.Fill(dat, "account");
                if(dat.Tables["account"].Rows.Count == 0)
                {
                    MessageBox.Show("用户名或密码错误！");
                    return;
                }
                else
                {
                    Form1.isLogin = true;
                    Form1.username = this.txtName.Text;
                    this.Close();
                }
                cmd.Clone();
            }
            conn.Close();
           
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
        private void button2_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPass.Text = "";
        }
       
        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }
    }
}
