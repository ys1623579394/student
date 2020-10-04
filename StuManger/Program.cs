using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StuManger
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login login = new Login();
            Form1 f = new Form1();
            login.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(login);
            if(Form1.isLogin)
            {
                f.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(f);
            }
        }
    }
}
