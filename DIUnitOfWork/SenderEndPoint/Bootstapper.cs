namespace SenderEndPoint
{
	using System;
	using Messages.Commands;
	using NServiceBus;

	public class Bootstapper : IWantToRunWhenBusStartsAndStops
    {
		public IBus Bus { get; set; }

		public void Start()
		{
			Console.WriteLine("Press 's' to send a command");
            Console.WriteLine("Press 'e' to send a command that will throw an exception.");

		    string cmd;

		    while ((cmd  = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
			    {
			        case "s":
			            var myCommand = Bus.CreateInstance<IMyCommand>(m =>
			            {
			                m.IdGuid = Guid.NewGuid();
			                m.Name = "My Name is Demo!";
			            });

			            Bus.Send(myCommand);

			            Console.WriteLine("Send a command message type: {1} with Id {0}."
			                , myCommand.IdGuid, myCommand.GetType());
			            Console.WriteLine("==========================================================================");
			            break;
                    case "e":
                        var exceptionCommand = Bus.CreateInstance<IMyCommand>(m =>
			            {
			                m.IdGuid = Guid.NewGuid();
			                m.Name = "My Name is Demo!";
			                m.Throw = true;
			            });

                        Bus.Send(exceptionCommand);

                        Console.WriteLine("Sending a command the will throw, message type: {1} with Id {0}."
                            , exceptionCommand.IdGuid, exceptionCommand.GetType());
			            Console.WriteLine("==========================================================================");

                        break;
			    }
            }
		}

		public void Stop()
		{
		}
    }
}
