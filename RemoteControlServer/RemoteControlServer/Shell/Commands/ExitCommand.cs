using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell.Commands
{
    /// <summary>
    /// Class executed when user types "exit"
    /// </summary>
    [ShellInput("exit")]
    [ShellInput("done")]
    public class ExitCommand : ICommand
    {
        public void Execute(Processor processor, params string[] parameters)
        {
            processor.Shutdown();
        }

        public string ShowLongHelp()
        {
            return "Shuts down the remote control server\n" +
                   "Parameters: None";
        }

        public string ShowShortHelp()
        {
            return "Shuts down the remote control server";
        }
    }
}
