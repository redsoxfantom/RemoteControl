using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteControlServer.Networking
{
    public class ConnectionListener
    {
        private TcpListener listener;
        private TcpClient client;
        private IPEndPoint connectionEndpoint;
        private IPAddress connectionIp = IPAddress.Any;
        private static readonly ILog log = LogManager.GetLogger("ConnectionListener");
        private bool isListening = false;
        private CommandProcessor mProc;
        private BlockingCollection<string> msgQueue;

        public ConnectionListener()
        {
            connectionEndpoint = new IPEndPoint(connectionIp, 50001);
            listener = new TcpListener(connectionEndpoint);
            msgQueue = new BlockingCollection<string>();
            mProc = new CommandProcessor(msgQueue);
        }

        public void StartListening()
        {
            log.Info("Connection Listener started");

            Task.Factory.StartNew(()=>{
                listener.Start();
                isListening = true;

                while(isListening)
                {
                    try
                    {
                        client = listener.AcceptTcpClient(); // Wait for a client to connect
                        log.Info("Client Connected");
                        StreamWriter clientWriter = new StreamWriter(client.GetStream());
                        StreamReader clientReader = new StreamReader(client.GetStream());

                        // Create and send a response packet
                        ConnectionResponse resp = new ConnectionResponse();
                        resp.screenWidth = (int)SystemParameters.PrimaryScreenWidth;
                        resp.screenHeight = (int)SystemParameters.PrimaryScreenHeight;
                        resp.keyboardLocale = CultureInfo.CurrentUICulture.DisplayName;
                        String responseString = JsonConvert.SerializeObject(resp);
                        clientWriter.WriteLine(responseString);
                        clientWriter.Flush();

                        while (true)
                        {
                            msgQueue.Add(clientReader.ReadLine());
                        }
                    }
                    catch(Exception ex)
                    {
                        log.Error("Failed to listen for connections", ex);
                    }
                    finally
                    {
                        log.InfoFormat("Shutting down connectionListener");
                        if (client != null)
                        {
                            client.Close();
                        }
                        msgQueue.CompleteAdding();
                    }
                }
            });
        }

        public void StopListening()
        {
            isListening = false;
            listener.Stop();
            msgQueue.CompleteAdding();
            log.Info("Connection Listener stopped");
        }
    }
}
