using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer.Shell
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShellInputAttribute : Attribute
    {
        /// <summary>
        /// The command string that will cause this command to execute. Must be unique per command
        /// </summary>
        public String Command { get; set; }

        public ShellInputAttribute(String command)
        {
            Command = command;
        }
    }
}
