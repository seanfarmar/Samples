using NServiceBus;

namespace Subscriber2
{
    using MyMessages;
    using NServiceBus.Unicast;

	/// <summary>
    /// Showing how to manage subscriptions manually
    /// </summary>
	//class Subscriber2Endpoint : IWantToRunWhenTheBusStarts
	//{
	//	public IBus Bus { get; set; }

	//	public void Run()
	//	{
	//		Bus.Subscribe<IMyEvent>();
	//	}

	//	public void Stop()
	//	{
	//		Bus.Unsubscribe<IMyEvent>();
	//	}
	//}
}