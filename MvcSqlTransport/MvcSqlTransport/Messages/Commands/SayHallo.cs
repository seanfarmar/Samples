namespace Messages.Commands
{
	using System;
	using NServiceBus;

	public class SayHallo : ICommand
    {
	    public Guid Guid { get; set; }

	    public string What { get; set; }
    }
}
