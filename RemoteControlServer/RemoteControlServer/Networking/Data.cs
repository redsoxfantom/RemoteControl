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
        public int count { get; set; }
    }

    public class ConnectionResponse
    {
        public int screenWidth { get; set; }
        public int screenHeight { get; set; }
        public String keyboardLocale { get; set; }
    }
    
    public abstract class Command
    {
        
    }
    
    public class MouseCommand : Command
    {
        public float x { get; set; }
        public float y { get; set; }
    }
}
