using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace UDP_client
{
    class Program
    {
        static Socket client;
        static void Main(string[] args)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse("255.255.255.0"), 6000));
            Thread t = new Thread(sendMsg);
            t.Start();
            Thread t2 = new Thread(ReciveMsg);
            t2.Start();
            Console.WriteLine("Puerta del cliente abierta, cierre al salir");
        }
        /// <summary>
        /// Envía un datagrama al puerto del host con una ip específica
        /// </summary>
        static void sendMsg()
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("255.255.255.0"), 6001);
            while(true){
                string msg = Console.ReadLine();
                client.SendTo(Encoding.UTF8.GetBytes(msg), point);
            }
        }
        /// <summary>
        /// Recibe el datagrama enviado al número de puerto correspondiente a la ip local
        /// </summary>
        static void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);// Se usa para guardar la IP y el número de puerto del remitente
                byte[] buffer = new byte[1024];
                int length = client.ReceiveFrom(buffer, ref point);// Recibir datagrama
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                Console.WriteLine(point.ToString() + message);
            }
        }

    }
}
