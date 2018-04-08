using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace AwaleProject.modele
{
    class Client
    {
        string serverIp;
        TcpClient tcpclnt;
        int port;

        public Client(string serverIp, int port)
        {
            this.ServerIp = serverIp;
            this.Port = port;
        }

        public int Port { get => port; set => port = value; }
        public string ServerIp { get => serverIp; set => serverIp = value; }
        public TcpClient Tcpclnt { get => tcpclnt; set => tcpclnt = value; }

        public void Start()
        {
           
                Tcpclnt = new TcpClient();

                Tcpclnt.Connect(ServerIp, Port);
        }

        public void End()
        {
            Tcpclnt.Close();
        }

        public void Send(string msg)
        {
            Console.WriteLine("Send -client");
            Stream stm = Tcpclnt.GetStream();
            
            byte[] ba = Encoding.UTF8.GetBytes(msg);

            stm.Write(ba, 0, ba.Length);
            
        }

        public string Receive()
        {
            Console.WriteLine("Listern -clietn");
            Stream stm = Tcpclnt.GetStream();

            byte[] bb = new byte[Tcpclnt.ReceiveBufferSize];
            int k = stm.Read(bb, 0, Tcpclnt.ReceiveBufferSize);

            return Encoding.UTF8.GetString(bb, 0, k);
        }
    }
}
