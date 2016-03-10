using log4net;
using RemoteControlServer.Networking;
using RemoteControlServer.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer
{
    public class Processor
    {
        private Boolean done;
        private ShellCommandProcessor mProc;
        private BeaconTransmitter mTransmitter;
        private static readonly ILog log = LogManager.GetLogger("Processor");

        public Processor(String[] args)
        {
            done = false;
            mProc = ShellCommandProcessor.Instance;
            mTransmitter = new BeaconTransmitter();
        }

        public void Run()
        {
            while(!done)
            {
                Console.Write("$> ");
                String commandInput = Console.ReadLine();
                String[] splitInput = commandInput.Split(' ');

                // Send the command to the command processor
                mProc.ProcessCommand(this, splitInput[0], splitInput.Skip(1).ToArray());
            }

            log.Info("Bye");
        }

        public void Shutdown()
        {
            done = true;
        }

        public void StartBeacon(String friendlyName = "DefaultName")
        {
            if(mTransmitter.GetIsTransmitting())
            {
                log.Info("Beacon transmitter was already running, shutting down");
                mTransmitter.StopTransmitting();
            }
            mTransmitter.StartTransmitting(friendlyName);
        }

        public void StopBeacon()
        {
            if (mTransmitter.GetIsTransmitting())
            {
                mTransmitter.StopTransmitting();
            }
            else
            {
                log.Info("Beacon is not transmitting");
            }
        }
    }
}
