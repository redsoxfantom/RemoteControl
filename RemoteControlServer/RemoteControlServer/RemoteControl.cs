using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer
{
    class RemoteControl
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RemoteControl));

        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
        }
    }
}
