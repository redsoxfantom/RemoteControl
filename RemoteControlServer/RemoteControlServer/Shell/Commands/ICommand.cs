using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell.Commands
{
    /// <summary>
    /// Interface for all shell commands
    /// </summary>
    interface ICommand
    {
        void Execute(Processor processor, params string[] parameters);
    }
}
