using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cline
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (username.Text != "" && ServerIp.Text != "" && prot_.Text != "")
            {
                Man man = new Man(ServerIp.Text,Convert.ToInt32(prot_.Text),username.Text);
                this.Visible = false; ;
                man.ShowDialog();
                
            }
        }
    }
}
