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
