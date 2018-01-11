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
        Login login;
        public Man(string ip,int port,string username,Login lgoin)
        {
            InitializeComponent();
            this.serverIp = ip;
            this.port = port;
            this.username = username;
            this.login = lgoin;
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
            string[] strs = str.Split('|');
            string[] userlist;
            switch (strs[0])
            {
                case "1001":
                    messagelist.AppendText("\n" + strs[1] + "\n");
                   userlist = strs[2].Split(',');
                    clineuserlist.Items.Clear();
                    foreach (var item in userlist)
                    {                      
                        clineuserlist.Items.Add(item.Split('-')[0], item.Split('-')[1], 0);
                    }
                    break;
                case "1002":
                    messagelist.AppendText("\n" + strs[1] + "\n");
                    userlist = strs[2].Split(',');
                    clineuserlist.Items.Clear();
                    foreach (var item in userlist)
                    {
                        clineuserlist.Items.Add(item.Split('-')[0], item.Split('-')[1], 0);
                    }
                    break;
                case "2001":                         
                    messagelist.AppendText("\n\n" + strs[1] + "\n");                  
                    break;
            }
          
            messagelist.ScrollToCaret();
        }          

        private void button1_Click(object sender, EventArgs e)
        {
            if (cilneMesage.Text != "")
            {
               
                messagelist.AppendText("\n\n"+cilneMesage.Text);
                //messagelist.SelectedText = "\n"+cilneMesage.Text;
                messagelist.ScrollToCaret();
                clinesocet.Send(Encoding.UTF8.GetBytes("2001|" + clinesocet.LocalEndPoint.ToString() + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + username + "|" + cilneMesage.Text));
                cilneMesage.Text = "";

            }

           
        }

       

        private void Man_FormClosing(object sender, FormClosingEventArgs e)
        {

            login.Dispose();
            login.Close();
            clinesocet.Send(Encoding.UTF8.GetBytes("1002|" + clinesocet.LocalEndPoint.ToString() + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + username + "|"));
            Environment.Exit(Environment.ExitCode);
        }
    }
 
}
