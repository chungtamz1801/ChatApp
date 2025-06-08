using ChatApp.Secutiry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            MessageScreen messageScreen = new MessageScreen();
            messageScreen.ip = ip_tbx.Text;
            messageScreen.port = Convert.ToInt32(port_tbx.Text);
            this.Hide();
            messageScreen.ShowDialog();
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ip_tbx.Text = Default.GetLocalIP();

        }
    }
}
