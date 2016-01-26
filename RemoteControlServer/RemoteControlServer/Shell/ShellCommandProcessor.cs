using RemoteControlServer.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell
{
    /// <summary>
    /// Singleton class that executes commands from the Shell
    /// </summary>
    public class ShellCommandProcessor
    {
        private static ShellCommandProcessor mInstance = null;

        /// <summary>
        /// Maps an input string to it's expected command
        /// </summary>
        private Dictionary<String, ICommand> mLoadedCommands;

        public ShellCommandProcessor Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new ShellCommandProcessor();
                }
                return mInstance;
            }
        }
        private ShellCommandProcessor()
        {

        }
    }
}
