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
            if (parameters.Length > 0)
            {
                if (parameters[0].Equals("start", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (parameters.Length > 1)
                    {
                        processor.StartBeacon(parameters[1]);
                    }
                    else
                    {
                        processor.StartBeacon();
                    }
                }
                else if (parameters[0].Equals("stop", StringComparison.InvariantCultureIgnoreCase))
                {
                    processor.StopBeacon();
                }
            }
            else
            {
                Console.WriteLine("Parameters: start/stop [friendly name]");
            }
        }

        public string ShowLongHelp()
        {
            return "Turns on and off the beacon. The beacon notifies any remote control apps within range that this server can be connected to\n"+
                   "Parameters: start/stop [friendly name]\n"+
                   "    start/stop - turns the server on and off\n"+
                   "    [friendly name] - optional parameter that designates a name for this server. This name will appear on any remote control apps within range.\n"+
                   "        If no name is designated, friendly name will be \"DefaultName\"";
        }

        public string ShowShortHelp()
        {
            return "Turns on and off the beacon. The beacon notifies any remote control apps within range that this server can be connected to";
        }
    }
}
