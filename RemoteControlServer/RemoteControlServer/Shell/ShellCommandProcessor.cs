using log4net;
using RemoteControlServer.Shell.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private static readonly ILog log = LogManager.GetLogger("ShellCommandProcessor");

        /// <summary>
        /// Maps an input string to it's expected command
        /// </summary>
        private Dictionary<String, ICommand> mLoadedCommands;

        public static ShellCommandProcessor Instance
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
            mLoadedCommands = new Dictionary<string, ICommand>();

            // First, get all ICommands that have a ShellCommand attribute
            var commands = Assembly.GetExecutingAssembly().GetTypes().Where((type) => {
                return type.GetCustomAttributes(typeof(ShellInputAttribute), true).Length > 0;
            });

            // Next, for each command we found, add it to the list of loaded commands
            foreach(var command in commands)
            {
                // Get all the command ids that the type supports
                var attr = ((ShellInputAttribute[])Attribute.GetCustomAttributes(command, typeof(ShellInputAttribute)))
                            .Select(attrib=>attrib.Command).ToList();
                ICommand cmd = (ICommand)Activator.CreateInstance(command);

                log.DebugFormat("Loaded command {0} to process inputs '{1}'",cmd.GetType().Name,String.Join(",",attr));

                foreach(var cmdAttr in attr)
                {
                    mLoadedCommands.Add(cmdAttr, cmd);
                }
            }
        }

        public void ProcessCommand(String command)
        {

        }
    }
}
