using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace AwaleProject.modele
{
    class Serveur
    {
        string ip;
        int port;
        TcpListener myList;
        Socket s;

        public Serveur(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public string Ip { get => ip; set => ip = value; }
        public int Port { get => port; set => port = value; }
        public TcpListener MyList { get => myList; set => myList = value; }
        public Socket S { get => s; set => s = value; }

        public void Start()
        {
                IPAddress ipAd = IPAddress.Parse(Ip);

                 MyList = new TcpListener(ipAd, Port);
                
                MyList.Start();

                S = MyList.AcceptSocket();
                
          
        }

        public void Stop()
        {
            S.Close();
            MyList.Stop();

        }

        public string Listen()
        {
            Console.WriteLine("Listern -serveur");
            byte[] b = new byte[S.ReceiveBufferSize];
            int k = S.Receive(b);
            return Encoding.UTF8.GetString(b, 0, k);
           
         }

        public void Send(string msg)
        {
            Console.WriteLine("Send -serveur");
            S.Send(Encoding.UTF8.GetBytes(msg));
        }
    
    }
}
