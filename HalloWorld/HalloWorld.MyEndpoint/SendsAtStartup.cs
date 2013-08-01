using NServiceBus;
using HalloWorld.Commands;

namespace HalloWorld.MyEndpoint
{
    public class SendsAtStartup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Bus.SendLocal<SaySomething>(m => m.What = "Hallo World!");
        }

        public void Stop(){}
    }
}
