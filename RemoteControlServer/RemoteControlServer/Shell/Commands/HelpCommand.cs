using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell.Commands
{
    [ShellInput("Help")]
    [ShellInput("help")]
    [ShellInput("?")]
    public class HelpCommand : ICommand
    {
        public void Execute(Processor processor, params string[] parameters)
        {
            if(parameters.Length > 0)
            {
                processor.ShellProcessor.ShowHelp(parameters[0]);
            }
            else
            {
                processor.ShellProcessor.ShowHelp();
            }
        }

        public string ShowLongHelp()
        {
            return "Displays command information.\n" +
                   "Parameters: [command name]\n" +
                   "    [command name] - Get information about a specific command\n" +
                   "        If no command is given, returns all commands known to the system";
        }

        public string ShowShortHelp()
        {
            return "Displays command information";
        }
    }
}
