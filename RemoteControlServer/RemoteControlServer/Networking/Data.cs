﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Networking
{
    public class BeaconPacket
    {
        public String ipAddress { get; set; }
        public String friendlyName { get; set; }
    }
}
