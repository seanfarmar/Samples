namespace ServiceControl.Raygun.Notifications.SenderEndPoint
{
    using System;
    using Messages.Commands;
    using NServiceBus;

    public class Bootstapper : IWantToRunWhenBusStartsAndStops
    {
        private MyCommand _myCommand;

        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Press 's' to send lots of commands");
            Console.WriteLine("Press 't' to send a test command");
            Console.WriteLine("Press 'e' to send a command that will throw an exception.");

            string cmd;

            while ((cmd = Console.ReadKey().Key.ToString().ToLower()) != "q")
            {
                Console.WriteLine(Environment.NewLine);

                switch (cmd)
                {
                    case "s":
                        for (int i = 0; i < 30; i++)
                        {
                            _myCommand = Bus.CreateInstance<MyCommand>(m =>
                            {
                                m.IdGuid = Guid.NewGuid();
                                m.Name = string.Format("My Name is MyCommand number {0}", i);
                            });

                            Bus.SendLocal(_myCommand);

                            Console.WriteLine("Send a command message number {2} type: {1} with Id {0}."
                                , _myCommand.IdGuid, _myCommand.GetType(), i);
                            Console.WriteLine(
                                "==========================================================================");
                        }
                        break;

                    case "e":
                        var exceptionCommand = Bus.CreateInstance<MyCommand>(m =>
                        {
                            m.IdGuid = Guid.NewGuid();
                            m.Name = "My Name is Demo!";
                            m.Throw = true;
                        });

                        Bus.SendLocal(exceptionCommand);

                        Console.WriteLine("Sending a exceptionCommand that will throw, message type: {1} with Id {0}."
                            , exceptionCommand.IdGuid, exceptionCommand.GetType());
                        Console.WriteLine("==========================================================================");

                        break;

                    case "t":

                        var testCommand = Bus.CreateInstance<MyTestCommand>(m =>
                        {
                            m.IdGuid = Guid.NewGuid();
                            m.Name = "My Name is TEST!";
                            m.Throw = true;
                        });

                        Bus.Send(testCommand);

                        Console.WriteLine("Sending a testCommand, message type: {1} with Id {0}."
                            , testCommand.IdGuid, testCommand.GetType());
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