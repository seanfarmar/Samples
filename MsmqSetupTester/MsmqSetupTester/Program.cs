using NServiceBus.Setup.Windows.Msmq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqSetupTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to install MSMQ.To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
                var result =  MsmqSetup.StartMsmqIfNecessary(true);

                Console.WriteLine("install result was: {0}", result);
            }
        }
    }
}
