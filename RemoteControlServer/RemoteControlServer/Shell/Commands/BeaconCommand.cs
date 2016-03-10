using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell.Commands
{
    [ShellInput("beacon")]
    [ShellInput("Beacon")]
    public class BeaconCommand : ICommand
    {
        public void Execute(Processor processor, params string[] parameters)
        {
            if(parameters[0].Equals("start",StringComparison.InvariantCultureIgnoreCase))
            {
                if(parameters.Length > 1)
                {
                    processor.StartBeacon(parameters[1]);
                }
                else
                {
                    Console.WriteLine("Need to define a friendly name for the server!");
                }
            }
            else if (parameters[0].Equals("stop", StringComparison.InvariantCultureIgnoreCase))
            {
                processor.StopBeacon();
            }
        }
    }
}
