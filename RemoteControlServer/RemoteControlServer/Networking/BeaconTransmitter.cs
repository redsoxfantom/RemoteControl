using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Networking
{
    public class BeaconTransmitter
    {
        UdpClient client;

        public BeaconTransmitter()
        {
            client = new UdpClient();
        }
    }
}
