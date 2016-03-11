using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteControlServer.Networking
{
    public class BeaconTransmitter
    {
        private UdpClient server;
        IPEndPoint broadcastAddress;
        private String ipAddress;
        private bool initialized;
        private bool isTransmitting;
        private static readonly ILog log = LogManager.GetLogger("BeaconTransmitter");

        public BeaconTransmitter()
        {
            initialized = false;
            isTransmitting = false;
            broadcastAddress = new IPEndPoint(IPAddress.Broadcast, 50000);

            try
            {
                // Get the local IP address
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip.ToString();
                    }
                }
                log.InfoFormat("Beacon transmitter initialized with ip address {0}", ipAddress);

                initialized = true;
            }
            catch(Exception ex)
            {
                log.Error("Failed to initialize Beacon transmitter", ex);
            }
        }
        
        public bool GetIsTransmitting()
        {
            return isTransmitting;
        }

        public void StopTransmitting()
        {
            if (isTransmitting)
            {
                log.Info("Beacon stopping transmission");
                isTransmitting = false;
            }
        }

        public void StartTransmitting(String friendlyName)
        {
            if(initialized)
            {
                log.InfoFormat("Beacon transmitting IP Address {0} and friendly name {1}",ipAddress,friendlyName);
                isTransmitting = true;

                BeaconPacket packet = new BeaconPacket();
                packet.friendlyName = friendlyName;
                packet.ipAddress = ipAddress;
                packet.count = 0;

                Task.Factory.StartNew(() => {
                    server = new UdpClient();
                    while(isTransmitting)
                    {
                        packet.count++;
                        String packetStr = JsonConvert.SerializeObject(packet);
                        byte[] packetBytes = Encoding.ASCII.GetBytes(packetStr);
                        
                        server.Send(packetBytes, packetBytes.Length, broadcastAddress);
                        Thread.Sleep(2500);
                    }
                    server.Close();
                });
            }
        }
    }
}
