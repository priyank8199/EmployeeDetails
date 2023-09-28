using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EmployeeServer
{
    public class BaseEmployee
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string UserCompany { get; set; }

        public string UserDesignation { get; set; }
    }
    public class EmpServer
    {
        static void Main(string[] args)
        {
            ExecuteServer();
        }
        public static void ExecuteServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 7777);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {

                listener.Bind(localEndPoint);
                listener.Listen(10);

                while(true)
                { 
                    Console.WriteLine("Waiting connection ... ");
                    Socket clientSocket = listener.Accept();

                    byte[] bytes = new Byte[1024];
                    string data = null;

                    while (true)
                    {

                        int numByte = clientSocket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes, 0, numByte);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }
                    string[] text = data.Split(",");
                    int UserId = Convert.ToInt32(text[0]);
                    string FirstName = text[1];
                    List<BaseEmployee> be = new List<BaseEmployee>();

                    using (StreamReader sr = new StreamReader("C:/Users/user/source/repos/POC/EmployeeServer/EmployeeData.json"))
                    {
                        string jsondata = sr.ReadToEnd();
                        be = JsonConvert.DeserializeObject<List<BaseEmployee>>(jsondata);
                    }

                    var final = be.Find(x => x.UserId == UserId && x.FirstName == FirstName);
                    var msg = JsonConvert.SerializeObject(final);

                    //var ms1 = msg.escape

                    byte[] message = Encoding.ASCII.GetBytes(msg);
                    clientSocket.Send(message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In server: ",e.Message.ToString());
            }
        }
    }

}
