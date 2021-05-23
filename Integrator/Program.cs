using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace Integrator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Message for user that the program is running
            Console.WriteLine("MediaIntegrator is working");

            //Start watcher
            Watcher watcher = new Watcher();

            //Keep conection open
            for (; ; )
                Console.Read();
        }

       
    }
}
