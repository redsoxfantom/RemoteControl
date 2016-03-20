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
        IPEndPoint beaconEndpoint;
        IPAddress beaconAddress;
        private String ipAddress;
        private bool initialized;
        private bool isTransmitting;
        private static readonly ILog log = LogManager.GetLogger("BeaconTransmitter");
        private String defaultName;

        public BeaconTransmitter()
        {
            initialized = false;
            isTransmitting = false;
            beaconAddress = IPAddress.Parse("224.0.0.1");
            beaconEndpoint = new IPEndPoint(beaconAddress, 50000);

            try
            {
                // Get this computer's host name
                defaultName = Dns.GetHostName();
                // Get the local IP address
                var host = Dns.GetHostEntry(defaultName);
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip.ToString();
                    }
                }
                log.InfoFormat("Beacon transmitter initialized with ip address {0} and default name {1}", ipAddress,defaultName);

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
                BeaconPacket packet = new BeaconPacket();
                if(friendlyName != null)
                {
                    packet.friendlyName = friendlyName;
                }
                else
                {
                    packet.friendlyName = defaultName;
                }
                log.InfoFormat("Beacon transmitting IP Address {0} and friendly name {1}", ipAddress, packet.friendlyName);
                isTransmitting = true;

                packet.ipAddress = ipAddress;
                packet.count = 0;

                Task.Factory.StartNew(() => {
                    server = new UdpClient(50000);
                    
                    server.JoinMulticastGroup(beaconAddress);

                    while(isTransmitting)
                    {
                        packet.count++;
                        String packetStr = JsonConvert.SerializeObject(packet);
                        byte[] packetBytes = Encoding.ASCII.GetBytes(packetStr);
                        
                        server.Send(packetBytes, packetBytes.Length, beaconEndpoint);
                        Thread.Sleep(500);
                    }
                    server.Close();
                });
            }
        }
    }
}
