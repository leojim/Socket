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
        string[] headsocket;
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
                    UserSocket(MessageSocket);
                }
            });
            task.Start();                                                                                           
                                                                 
        }


        public void UserSocket(object socket)
        {

            Socket MessageSocket = socket as Socket;
            Task task = new Task(() => {              
                    byte[] byte_ = new byte[1024 * 200];
                    while (true)
                    {
                        MessageSocket.Receive(byte_);
                        string str = Encoding.UTF8.GetString(byte_);
                        if (man.InvokeRequired)
                        {
                            message msg = new message(Message);
                            man.BeginInvoke(msg, str, MessageSocket);

                        }
                    }
                
                

            });
            task.Start();
        }
        public void Message(string str,Socket socket)
        {
                   
                string[] msg = str.Split('|');
                switch (msg[0])
                  {
                    case "1001":
                    if (!userlist.ContainsKey(msg[1]))
                    {
                        man.messagebox.AppendText("\n"+msg[2] + "\n IP: " + msg[1] + "  用户名: " + msg[3] + " 进入聊天室");
                        man.userlistbox.Items.Add(msg[3]);
                        userlist.Add(msg[1], socket);
                        foreach (var item in userlist)
                        {
                            (item.Value as Socket).Send(System.Text.Encoding.UTF8.GetBytes("用户名: " + msg[3] + " 进入聊天室"));
                        }
                    }
                    else {
                        socket.Dispose();
                        socket.Close();
                    }
                       
                    break;
                      case "2001":
                    man.messagebox.AppendText("\n"+msg[2] + "\n IP: " + msg[1] + "  用户名: " + msg[3] + "\n"+msg[4]);                  
                    foreach (var item in userlist)
                    {
                        (item.Value as Socket).Send(System.Text.Encoding.UTF8.GetBytes("\n用户名: " + msg[3] + " \n"+ msg[4]));
                    }
                    break;
                   }
                 
        }     
        public void CloseSocket()
        {
            serverSocket.Close();
            serverSocket.Dispose();
        }
    }
}
