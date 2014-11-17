namespace Subscriber1
{
    using System;
    using MyMessages;
    using NServiceBus.Logging;
    using NServiceBus.Saga;

    public class EventMessageHandler : Saga<EventSagaData>
        , IAmStartedByMessages<EventMessage>
        , IHandleTimeouts<EventTimeout>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (EventMessageHandler));

        public void Handle(EventMessage message)
        {
            Logger.Info(string.Format("Subscriber 1 received EventMessage with Id {0}.", message.EventId));
            Logger.Info(string.Format("Message time: {0}.", message.Time));
            Logger.Info(string.Format("Message duration: {0}.", message.Duration));
            Console.WriteLine("==========================================================================");

            RequestTimeout<EventTimeout>(TimeSpan.FromSeconds(10));
        }

        public void Timeout(EventTimeout state)
        {
            Console.WriteLine("Handeling timeout");
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<EventSagaData> mapper)
        {
            mapper.ConfigureMapping<EventMessage>(m => m.EventId).ToSaga(s => s.Id);
        }
    }

    public class EventTimeout
    {
    }

    public class EventSagaData : ContainSagaData
    {
    }
}