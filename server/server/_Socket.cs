using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;
using System.Web;
using System.Windows.Forms;

namespace server
{
    public class _Socket
    {
        //储存在线用户
        delegate void message(string str,Socket socket);
        public SortedList<string, Socket> userlist = new SortedList<string, Socket>();
        Man man;     
        public Socket serverSocket;
        public _Socket(string ip, int port,Man man)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.man = man;
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            serverSocket.Bind(ipe);
            serverSocket.Listen(10);
            Task task = new Task(()=> {
                while (true)
                {
                    Socket MessageSocket = serverSocket.Accept();
                    UserSocket(ip,MessageSocket);
                }
            });
            task.Start();                                                                                           
                                                                 
        }


        public void UserSocket(string ip,object socket)
        {

            
            Task task = new Task(() => {
                Socket MessageSocket = socket as Socket;                              
                    while (true)
                    {
                         byte[] byte_ = new byte[1024 * 200];
                    try
                    {
                        MessageSocket.Receive(byte_);
                        string str = Encoding.UTF8.GetString(byte_);
                        if (man.InvokeRequired)
                        {
                            message msg = new message(Message);
                            man.BeginInvoke(msg, str, MessageSocket);

                        }
                    }
                    catch {
                        MessageSocket.Close();
                        MessageSocket.Dispose();
                        userlist.Remove(ip);
                        //foreach (ListViewItem item in man.userlistbox.Items)
                        //{
                        //    if (item.Name == ip)
                        //    {
                        //        man.userlistbox.Items.Remove(item);
                        //    }
                        //}
                    }
                        
                    }
                
                

            });
            task.Start();
        }
        public void Message(string str,Socket socket)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string[] msg = str.Split('|');
            string userliststr = "";    
            switch (msg[0])
                  {
                //进入聊天室
                case "1001":
                    if (!userlist.ContainsKey(msg[1]))
                    {
                        man.messagebox.AppendText("\n"+msg[2] + "\n IP: " + msg[1] + "  用户名: " + msg[3] + " 进入聊天室");                      
                        man.userlistbox.Items.Add(msg[1],msg[3],0);
                        userlist.Add(msg[1], socket);
                       
                        foreach (ListViewItem item in man.userlistbox.Items)
                        {
                            userliststr += item.Name+"-"+item.Text+",";
                        }
                        foreach (var item in userlist)
                        {
                            (item.Value as Socket).Send(System.Text.Encoding.UTF8.GetBytes("1001|" + "用户名: " + msg[3] + " 进入聊天室"+"|"+userliststr.Substring(0,userliststr.Length-1)));
                        }
                    }
                    else {
                        socket.Dispose();
                        socket.Close();
                    }
                       
                    break;
                    //退出聊天室
                case "1002":
                    man.userlistbox.Items.RemoveByKey(msg[1]);
                    man.messagebox.AppendText("\n" + msg[2] + "\n IP: " + msg[1] + "  用户名: " + msg[3] + "退出聊天室");                 
                    userlist[msg[1]].Close();
                    userlist[msg[1]].Dispose();
                    userlist.Remove(msg[1]);                    
                    foreach (ListViewItem item in man.userlistbox.Items)
                    {
                        userliststr += item.Name + "-" + item.Text + ",";
                    }
                    foreach (var item in userlist)
                    {
                        (item.Value as Socket).Send(System.Text.Encoding.UTF8.GetBytes("1002|" + datetime + "\n用户名: " + msg[3] + " 退出聊天室"+"|"+ userliststr.Substring(0, userliststr.Length - 1)));
                    }
                    break;
                    //接受发送的群聊消息
                      case "2001":
                    man.messagebox.AppendText("\n"+msg[2] + "\n IP: " + msg[1] + "  用户名: " + msg[3] + "\n"+msg[4]);                 
                    foreach (var item in userlist)
                    {
                        if (item.Key != msg[1])
                        {
                            (item.Value as Socket).Send(System.Text.Encoding.UTF8.GetBytes("2001|" + datetime + "\n用户名: " + msg[3] + " \n" + msg[4]));
                        }

                    }
                    break;
                   }
                  man.messagebox.ScrollToCaret();
        }     
        public void CloseSocket()
        {
            serverSocket.Close();
            serverSocket.Dispose();
           
        }
    }
}
