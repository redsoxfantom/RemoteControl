using RemoteControlServer.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer
{
    public class Processor
    {
        private Boolean done;
        private ShellCommandProcessor mProc;

        public Processor(String[] args)
        {
            done = false;
            mProc = ShellCommandProcessor.Instance;
        }

        public void Run()
        {
            while(!done)
            {
                Console.Write("$> ");
                String commandInput = Console.ReadLine();
            }
        }
    }
}
