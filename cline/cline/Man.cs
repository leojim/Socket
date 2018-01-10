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
using System.Threading;
using System.Web;
namespace cline
{
    public partial class Man : Form
    {
        string serverIp;
        int port;
        string username;
        delegate void clineMsaage(string str);
        Socket clinesocet;
        public Man(string ip,int port,string username)
        {
            InitializeComponent();
            this.serverIp = ip;
            this.port = port;
            this.username = username;
            clinesocet = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            try
            {
                messagelist.AppendText("正在建立连接\n");
                clinesocet.Connect(System.Net.IPAddress.Parse(serverIp), port);
                messagelist.AppendText("连接成功\n");
                clinesocet.Send(System.Text.Encoding.UTF8.GetBytes("1001|"+ clinesocet.LocalEndPoint.ToString()+"|"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"|"+username+"|"));
                Thread thread = new Thread(CilenSocket);
                thread.Start(clinesocet);
            }
            catch (Exception ex) {
                messagelist.AppendText("连接异常,请重新登录\n 消息:" + ex.Message+"\n");
            }
            
        }

        public void CilenSocket(object socket)
        {
            
                byte[] byte_ = new byte[1024*200];
                Socket messageSocket = socket as Socket;
                try
                {
                  while (true)
                    {
                        if (this.InvokeRequired)
                        {
                            messageSocket.Receive(byte_);
                            clineMsaage cm = new clineMsaage(ClineMsaage);
                            this.BeginInvoke(cm, System.Text.Encoding.UTF8.GetString(byte_));
                         }
                    }
                   }
                catch(Exception ex) {                 
                    clineMsaage cm = new clineMsaage(ClineMsaage);
                    this.BeginInvoke(cm, "意外退出聊天室,请重新登录\n 消息:"+ex.Message);                   
                }
               
            
           
        }
        public void ClineMsaage(string str) {
            messagelist.AppendText("\n"+str + "\n");
        }     
        private void Man_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login login = new Login();
            login.Dispose();
         
            login.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clinesocet.Send(Encoding.UTF8.GetBytes("2001|" + clinesocet.LocalEndPoint.ToString() + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + username + "|"+cilneMesage.Text));
        }
    }
 
}
