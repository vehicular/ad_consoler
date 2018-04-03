using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace vehicular_simulation
{
    public class SocketClient
    {
        /*private static Socket TCP_ClientSocket;
        private static string ip;
        public static bool IsConnectedUnity = false;*/
        public static FormMain form;
        
        private TcpClient client;
        private Thread thread;
        private NetworkStream networkStream;

        /*private delegate void SetConnectedUnity();
        private static SetConnectedUnity setConnected;*/

        /*public static Socket GetSocket()
        {
            return TCP_ClientSocket;
        }*/
        public SocketClient(FormMain _form) {
            form = _form;
            //setConnected = form.setConnectedUnity;

            //TCP_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            /*string hostName;
            if (_ip==""||_ip==null)
            {
                hostName = Dns.GetHostName();
                IPHostEntry IpEntry = Dns.GetHostEntry(hostName);                
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {                    
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ip= IpEntry.AddressList[i].ToString();
                    }
                }
            }
            else
            {
                ip = _ip;
            }            
            Console.WriteLine(_ip.ToString());*/
        }

        public void Start( string ipAddress, int port )
        {
            try
            {
                client = new TcpClient();
                form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Connecting..." });
                client.Connect(ipAddress, port);
            }
            catch { }

            thread = new Thread(new ThreadStart(ClientStartThread));
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Highest;
            thread.Name = "ClientStartThread";
            thread.Start();
        }

        private string GetMessageData(string packet)
        {
            string header = null;
            string data = null;
            int position = packet.IndexOf(':', 0);
            if (position >= 4 && position < packet.Length - 1) //<<...>>:...
            {
                header = packet.Substring(2, position - 4);
                data = packet.Substring(position + 1, packet.Length - position - 1);
            }
            return data;
        }

        private void ClientStartThread()
        {
            try
            {
                do
                {
                    networkStream = client.GetStream();

                    if (networkStream.CanRead)
                    {
                        byte[] bytes = new byte[client.ReceiveBufferSize];
                        networkStream.Read(bytes, 0, Convert.ToInt32(client.ReceiveBufferSize));

                        string returnData = Encoding.ASCII.GetString(bytes);
                        form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "received data: " + returnData });
                        string rsp = returnData.Replace("\0", "");
                        if (GetMessageData(rsp) == "arrived")
                        {
                            form.IsArrived = true;
                        }
                        if (form.isSending_CAR_RUN)
                        {
                            if (rsp.StartsWith("Failed"))
                            {
                                form.Invoke(form.controlButtonRunDelegate, new object[] { false });
                                form.IsError = true;
                            }
                            else
                            {
                                form.Invoke(form.controlButtonRunDelegate, new object[] { true });
                            }
                            form.isSending_CAR_RUN = false;
                        }
                        /*if (returnData.Replace("\0", "") == "Closing connection...")
                        {
                            form.Disconnect();
                        }*/
                    }
                    else
                    {
                        form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Error" });
                        form.Disconnect();
                    }
                } while (client.Connected);
            }
            catch (IOException)
            {
                form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Client disconnected..." });
            }
            catch (InvalidOperationException)
            {
                form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Server is offline..." });
            }
            finally
            {
                form.Disconnect();
            }
        }

        public void Disconnect()
        {
            if (thread != null)
                if (thread.ThreadState == System.Threading.ThreadState.Running ||
                    thread.ThreadState == System.Threading.ThreadState.Stopped )
                    thread.Abort();
            client.Close();
        }

        public void SendMessage(string message)
        {
            try
            {
                if (networkStream.CanWrite)
                {
                    byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "sent data: " + message });
                }
                else
                {
                    form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Error. To disconnect 1" });
                    form.IsArrived = true;
                    Disconnect();
                }
            }
            catch
            {
                form.Invoke(form.insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Error. To disconnect 2" });
                form.IsArrived = true;
                Disconnect();
            }
        }
    }
}
