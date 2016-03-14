using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Networking
{
    public class ConnectionListener
    {
        private TcpClient client;
        private IPEndPoint connectionEndpoint;
        private IPAddress connectionIp = IPAddress.Any;
        private static readonly ILog log = LogManager.GetLogger("ConnectionListener");
        private bool isListening = false;

        public ConnectionListener()
        {
            connectionEndpoint = new IPEndPoint(connectionIp, 50001);
        }

        public void StartListening()
        {
            Task.Factory.StartNew(()=>{
                log.Info("Connection Listener started");
                client = new TcpClient(connectionEndpoint);
                client.ReceiveTimeout = 1000;
                StreamReader clientReader = new StreamReader(client.GetStream());
                isListening = true;

                while(isListening)
                {
                    try
                    {
                        String connectionAttempt = clientReader.ReadLine();
                        log.InfoFormat("Got message {0}", connectionAttempt);
                    }
                    catch(Exception)
                    {}
                }

                log.InfoFormat("Shutting down connectionListener");
                client.Close();
            });
        }

        public void StopListening()
        {
            isListening = false;
            log.Info("Connection Listener stopped");
        }
    }
}
