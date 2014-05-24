namespace MonitoringNotifications.Messages.Commands
{
    using System;
    using NServiceBus;

    public class MyTestCommand :ICommand
    {
	    public Guid IdGuid { get; set; }

		public string Name { get; set; }
	    
        public bool Throw { get; set; }
    }
}
