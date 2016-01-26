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

            log.InfoFormat("Initializing with args [{0}]", String.Join(",", args));
            var proc = new Processor(args);

            log.Info("Beginning execution...");
            proc.Run();

            log.Info("Bye");
        }
    }
}
