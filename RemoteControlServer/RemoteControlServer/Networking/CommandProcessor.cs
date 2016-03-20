using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Networking
{
    /// <summary>
    /// Listens for commands from connected clients
    /// </summary>
    public class CommandProcessor
    {
        private BlockingCollection<String> msgQueue;
        private static readonly ILog log = LogManager.GetLogger("CommandProcessor");

        public CommandProcessor(BlockingCollection<String> msgQueue)
        {
            this.msgQueue = msgQueue;
        }

        public void StartProcessing()
        {
            Task.Factory.StartNew(() => {
                foreach(var command in msgQueue.GetConsumingEnumerable())
                {
                    log.Info("Got command " + command);
                }
            });
        }
    }
}
