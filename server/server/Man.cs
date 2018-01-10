using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace server
{
    public partial class Man : Form
    {    
        public Man()
        {          
            InitializeComponent();          

        }
      
        delegate void message(string str, string ip);
        _Socket socket = null;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                socket = new _Socket(ip.Text, Convert.ToInt32(port.Text),this);
                messagebox.AppendText("已开启监听\n");
            }
            catch(Exception ex) {
                messagebox.AppendText("开启异常 :"+ex.Message+ "\n");
            }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                socket.CloseSocket();
                messagebox.AppendText("已关闭监听"+"\n");
            }
            catch (Exception ex)
            {
                messagebox.AppendText("关闭异常 :" + ex.Message+ "\n");
            }
           
        }
       
      
    }
}
