namespace SenderEndPoint
{
	using NServiceBus;

	public class MessageConfig : IWantToRunBeforeConfiguration
	{
		public void Init()
		{
			Configure.With().DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Messages.Commands"));
		}
	}
}