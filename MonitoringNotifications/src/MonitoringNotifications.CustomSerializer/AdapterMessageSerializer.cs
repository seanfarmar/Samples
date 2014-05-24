namespace MonitoringNotifications.CustomSerializer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NServiceBus;
    using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
    using NServiceBus.Serialization;
    using NServiceBus.Serializers.Json;
    using NServiceBus.Serializers.XML;
    using NServiceBus.Unicast.Messages;

    public class AdapterMessageSerializer : IMessageSerializer
    {
        public LogicalMessageFactory MessageFactory { get; set; }

        public string ContentType { get; private set; }

        public bool SkipArrayWrappingForSingleMessages { get; set; }

        private IMessageSerializer _serielizer;

        public void Serialize(object[] messages, Stream stream)
        {
            if (string.IsNullOrEmpty(ContentType))
            {
                ContentType = ContentTypes.Json;               
            }
            if(ContentType == ContentTypes.Json)
                _serielizer = new JsonMessageSerializer((new MessageMapper()));

            if (ContentType == ContentTypes.Xml)
                _serielizer = InitialiseXmlSerializer(null);
            
            _serielizer.Serialize(messages, stream);
        }

        public object[] Deserialize(Stream stream, IList<Type> messageTypes = null)
        {
            // look into the headers and determine what serilizer to use

            NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext physicalMessageContext = 
                MessageFactory.PipelineExecutor.CurrentContext as NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext;

            if (physicalMessageContext == null)
                ContentType = ContentTypes.Xml;

            if (physicalMessageContext != null)
                ContentType = physicalMessageContext.PhysicalMessage.Headers[Headers.ContentType];

            if(ContentType == ContentTypes.Json)
                _serielizer = new JsonMessageSerializer((new MessageMapper())){SkipArrayWrappingForSingleMessages = SkipArrayWrappingForSingleMessages};

            if(ContentType == ContentTypes.Xml)
                _serielizer = InitialiseXmlSerializer(messageTypes);

            return _serielizer.Deserialize(stream, messageTypes);
        }

        private IMessageSerializer InitialiseXmlSerializer(IList<Type> messageTypes)
        {
            var mapper = new MessageMapper();

            if (messageTypes == null)
                messageTypes = Configure.TypesToScan.Where(MessageConventionExtensions.IsMessageType).ToList();

            mapper.Initialize(messageTypes);

            var xmlserieliser = new XmlMessageSerializer(mapper);

            xmlserieliser.Initialize(messageTypes);

            return xmlserieliser;
        }
    }
}
