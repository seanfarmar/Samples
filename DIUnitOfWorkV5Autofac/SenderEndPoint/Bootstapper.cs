namespace SenderEndPoint
{
	using System;
	using Messages.Commands;
	using NServiceBus;

	public class Bootstapper : IWantToRunWhenBusStartsAndStops
    {
        private IMyCommand _myCommand;        
        private IMyOtherCommand _myOtherCommand;
		public IBus Bus { get; set; }

		public void Start()
		{
			Console.WriteLine("Press 's' to send lots of commands");
            Console.WriteLine("Press 'e' to send a command that will throw an exception.");

		    string cmd;

		    while ((cmd  = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
			    {
			        case "s":
			            for (int i = 0; i < 1; i++)
			            {
			                _myCommand = new MyCommand
			                {
			                    IdGuid = Guid.NewGuid(),
			                    Name = string.Format("My Name is MyCommand number {0}", i)
			                };

                            Bus.Send(_myCommand);

                            Console.WriteLine("Send a command message number {2} type: {1} with Id {0}."
                                , _myCommand.IdGuid, _myCommand.GetType(),i);
                            Console.WriteLine("==========================================================================");

			                _myOtherCommand = new MyOtherCommand
			                {
			                    IdGuid = Guid.NewGuid(),
			                Name = string.Format("My Name is MyOtherCommand number {0}", i)
			                };

                            Bus.Send(_myOtherCommand);

                            Console.WriteLine("Send a MyOtherCommand message number {2} type: {1} with Id {0}."
                                , _myCommand.IdGuid, _myCommand.GetType(), i);
                            Console.WriteLine("==========================================================================");
			            }
			            break;

                    case "e":
			            var exceptionCommand = new MyCommand
			            {
			                IdGuid = Guid.NewGuid(),
			                Name = "My Name is Demo!",
			                Throw = true
			            };

                        Bus.Send(exceptionCommand);

                        Console.WriteLine("Sending a exceptionCommand the will throw, message type: {1} with Id {0}."
                            , exceptionCommand.IdGuid, exceptionCommand.GetType());
			            Console.WriteLine("==========================================================================");

			            var exceptionOtherCommand = new MyOtherCommand
			            {
			                IdGuid = Guid.NewGuid(),
			                Name = "My Name is exceptionOtherCommand!",
			                Throw = true
			            };

                        Bus.Send(exceptionOtherCommand);

                        Console.WriteLine("Sending a exceptionOtherCommand the will throw, message type: {1} with Id {0}."
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
