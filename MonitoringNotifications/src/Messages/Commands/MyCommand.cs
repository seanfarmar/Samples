namespace MonitoringNotifications.Messages.Commands
{
    using System;
    using NServiceBus;

    public class MyCommand :ICommand
    {
	    public Guid IdGuid { get; set; }

		public string Name { get; set; }
	    
        public bool Throw { get; set; }
    }
}
