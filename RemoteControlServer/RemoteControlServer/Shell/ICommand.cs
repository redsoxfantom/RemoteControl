using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell
{
    /// <summary>
    /// Interface for all shell commands
    /// </summary>
    interface ICommand
    {
        void Execute();
    }
}
