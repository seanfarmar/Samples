namespace NSebHost
{
    using System;
    using Nancy.Hosting.Self;
    using NServiceBus;

    public class Bootstrapper : IWantToRunWhenBusStartsAndStops
    {
        // initialize an instance of NancyHost (found in the Nancy.Hosting.Self package)
        static readonly NancyHost Host = new NancyHost(new Uri("http://localhost:12345"));      
        
        public void Start()
        {
            Host.Start(); // start hosting
        }
        
        public void Stop()
        {
            Host.Stop(); // stop hosting
        }
    }
}
