﻿using log4net;
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
        private static readonly ILog log = LogManager.GetLogger("ShellInputProcessor");

        /// <summary>
        /// Maps an input string to it's expected command
        /// </summary>
        private Dictionary<String, ICommand> mLoadedCommands;

        /// <summary>
        /// Maps a command object to all command strings that invoke it
        /// </summary>
        private Dictionary<ICommand, List<String>> mCommandStrings;

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
            mCommandStrings = new Dictionary<ICommand, List<string>>();

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
                mCommandStrings.Add(cmd, attr);
            }
        }

        public void ProcessCommand(Processor processor, String command, params string[] parameters)
        {
            if(mLoadedCommands.ContainsKey(command))
            {
                mLoadedCommands[command].Execute(processor, parameters);
            }
            else
            {
                Console.WriteLine(String.Format("Command {0} not found",command));
            }
        }

        public void ShowHelp(string command = null)
        {
            if(command != null)
            {
                if(mLoadedCommands.ContainsKey(command))
                {
                    Console.WriteLine(mLoadedCommands[command].ShowLongHelp());
                }
                else
                {
                    Console.WriteLine(String.Format("Command {0} not found", command));
                }
            }
            else
            {
                foreach(var cmd in mCommandStrings.Keys)
                {
                    var cmdStrings = mCommandStrings[cmd];
                    Console.WriteLine(String.Format("{0} - {1}",String.Join(",",cmdStrings),cmd.ShowShortHelp()));
                }
            }
        }
    }
}
