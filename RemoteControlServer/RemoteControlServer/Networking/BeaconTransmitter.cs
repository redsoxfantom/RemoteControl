using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Networking
{
    public class BeaconTransmitter
    {
        private UdpClient client;
        private String ipAddress;
        private bool initialized;
        private bool isTransmitting;
        private static readonly ILog log = LogManager.GetLogger("BeaconTransmitter");

        public BeaconTransmitter()
        {
            initialized = false;
            isTransmitting = false;
            client = new UdpClient();

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
            isTransmitting = false;
        }

        public void StartTransmitting(String friendlyName)
        {
            if(initialized)
            {
                log.InfoFormat("Beacon transmitting IP Address {0} and friendly name {1}",ipAddress,friendlyName);

                isTransmitting = true;
            }
        }
    }
}
