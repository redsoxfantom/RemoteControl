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

        public Processor(String[] args)
        {
            done = false;
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
