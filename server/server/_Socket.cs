using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;

namespace server
{
  public class _Socket
  {
        //储存在线用户
        public SortedList<string,string> userlist = new SortedList<string, string>();
        Form1 man = new Form1();
        public _Socket(string ip,int port)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip),port);
            serverSocket.Bind(ipe);
            serverSocket.Listen(10);
            Task task = new Task(() => {
                Socket userSocket = serverSocket.Accept();
                Thread thread = new Thread(UserSocket);
                thread.Start(userSocket);
            });
            task.Start();
        }
        public void UserSocket(object socket)
        {
           
            Socket usersoket = socket as Socket;
            Thread thread = new Thread(MesageSocket);
            thread.Start(usersoket);
        }
        public void MesageSocket(object socket)
        {
            byte[] byte_ = new byte[1024 * 1024];
            Socket MessageSocket = socket as Socket;
            while (true)
            {
                MessageSocket.Receive(byte_);
                man.richTextBox1.AppendText(System.Text.ASCIIEncoding.ASCII.GetString(byte_));
                
            }
        }
    }
}
