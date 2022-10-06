using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace UDP_Server
{
    class Program
    {
        static Socket server;
        static void Main(string[] args)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse("10.10.17.106"), 7734));// Vincular el número de puerto y la IP
            Console.WriteLine("Puerta abierta, cierre al salir");
            Thread t = new Thread(ReciveMsg);// Abrir el hilo de mensajes de recepción
            t.Start();
            Thread t2 = new Thread(sendMsg);// Abrir el hilo de envío de mensajes
            t2.Start();


        }
        /// <summary>
        /// Envía un datagrama al puerto del host con una ip específica
        /// </summary>
        static void sendMsg()
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("10.10.17.106"), 7734);
            while (true)
            {
                string msg = Console.ReadLine();
                server.SendTo(Encoding.UTF8.GetBytes(msg), point);
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
                int length = server.ReceiveFrom(buffer, ref point);// Recibir datagrama
                string message = Encoding.UTF8.GetString(buffer,0,length);
                Console.WriteLine(point.ToString()+ message);

            }
        }
    }
}
