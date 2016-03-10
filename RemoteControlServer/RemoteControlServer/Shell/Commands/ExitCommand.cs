using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell.Commands
{
    /// <summary>
    /// Class executed when user types "done"
    /// </summary>
    [ShellInput("exit")]
    public class ExitCommand : ICommand
    {
        public void Execute(Processor processor, params string[] parameters)
        {
            processor.Shutdown();
        }
    }
}
