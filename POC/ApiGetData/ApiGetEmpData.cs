using Microsoft.Extensions.Logging;
using POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace POC.ApiGetData
{
    public class ApiGetEmpData : IApiGetData
    {
        public string GetEmployee(int id, string name)
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[1];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 7777);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                { 
                    sender.Connect(localEndPoint);

                    //_logger.LogInformation("Socket Connected..");


                    byte[] messageSent = Encoding.ASCII.GetBytes(id + "," + name + ",<EOF>");
                    int byteSent = sender.Send(messageSent);


                    byte[] messageReceived = new byte[1024];

                    int byteRecv = sender.Receive(messageReceived);
                    var txt = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);

                    //txt.Replace("\\", " ");
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    return txt;
                }
                catch (Exception e)
                {
                   // _logger.LogError(e.Message.ToString());
                    return "In Client inner try:" + e.Message.ToString();
                }
            }
            catch (Exception e)
            {
                //_logger.LogError(e.Message.ToString());
                return "In Client Outer try: " + e.Message.ToString();
            }
        }
    }
}
