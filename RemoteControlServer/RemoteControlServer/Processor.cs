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
        public ShellCommandProcessor ShellProcessor { get; }
        private ConnectionListener mConnListener;
        private BeaconTransmitter mTransmitter;
        private static readonly ILog log = LogManager.GetLogger("Processor");

        public Processor(String[] args)
        {
            done = false;
            ShellProcessor = ShellCommandProcessor.Instance;
            mTransmitter = new BeaconTransmitter();
            mConnListener = new ConnectionListener();
        }

        public void Run()
        {
            mConnListener.StartListening();
            while(!done)
            {
                Console.Write("$> ");
                String commandInput = Console.ReadLine();
                String[] splitInput = commandInput.Split(' ');

                // Send the command to the command processor
                ShellProcessor.ProcessCommand(this, splitInput[0], splitInput.Skip(1).ToArray());
            }

            log.Info("Bye");
        }

        public void Shutdown()
        {
            mConnListener.StopListening();
            StopBeacon();
            done = true;
        }

        public void StartBeacon(String friendlyName = null)
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
