namespace MonitoringNotifications.CustomSerializer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NServiceBus;
    using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
    using NServiceBus.Pipeline;
    using NServiceBus.Serialization;
    using NServiceBus.Serializers.Binary;
    using NServiceBus.Serializers.Json;
    using NServiceBus.Serializers.XML;

    public class MessageSerializerAdapter : IMessageSerializer
    {
        public PipelineExecutor PipelineExecutor { get; set; }

        public string ContentType { get; private set; }

        public bool SkipArrayWrappingForSingleMessages { get; set; }

        private IMessageSerializer _serielizer;

        private IMessageSerializer _defaultSerializer;
        
        private readonly Dictionary<string, IMessageSerializer> _serializers = new Dictionary<string, IMessageSerializer>();
        
        public MessageSerializerAdapter()
        {
            var mapper = new MessageMapper();

            var json = new JsonMessageSerializer(mapper);
            _serializers.Add(json.ContentType, json);

            var bson = new BsonMessageSerializer(mapper);
            _serializers.Add(bson.ContentType, bson);

            var binary = new BinaryMessageSerializer();
            _serializers.Add(binary.ContentType, binary);

            IList<Type> messageTypes = Configure.TypesToScan.Where(MessageConventionExtensions.IsMessageType).ToList();
            
            var xml = new XmlMessageSerializer(mapper);
            
            xml.Initialize(messageTypes);
            
            _serializers.Add(xml.ContentType, xml);

            _defaultSerializer = xml;

            if (!_serializers.ContainsKey(_defaultSerializer.ContentType))
            {
                _serializers.Add(_defaultSerializer.ContentType, _defaultSerializer);
            }
        }

        public void Serialize(object[] messages, Stream stream)
        {
            //if (string.IsNullOrEmpty(ContentType))
            //{
            //    ContentType = ContentTypes.Json;               
            //}

            SetSerilizer(ContentType);

            //if(ContentType == ContentTypes.Json)
            //    _serielizer = new JsonMessageSerializer((new MessageMapper()));

            //if (ContentType == ContentTypes.Xml)
            //    _serielizer = InitialiseXmlSerializer(null);
            
            _serielizer.Serialize(messages, stream);
        }

        public object[] Deserialize(Stream stream, IList<Type> messageTypes = null)
        {
            // look into the headers and determine what serilizer to use
            ContentType = PipelineExecutor.CurrentContext.Get<TransportMessage>("NServiceBus.IncomingPhysicalMessage").Headers[Headers.ContentType];
            
            SetSerilizer(ContentType);

            //if(ContentType == ContentTypes.Json)
            //    _serielizer = new JsonMessageSerializer((new MessageMapper())){SkipArrayWrappingForSingleMessages = SkipArrayWrappingForSingleMessages};

            //if(ContentType == ContentTypes.Xml)
            //    _serielizer = InitialiseXmlSerializer(messageTypes);

            return _serielizer.Deserialize(stream, messageTypes);
        }

        private void SetSerilizer(string contentType)
        {
            if (string.IsNullOrEmpty(ContentType))
            {
                ContentType = ContentTypes.Json;
            }

            _serielizer = _serializers[contentType] ?? _defaultSerializer;
        }

        //private IMessageSerializer InitialiseXmlSerializer(IList<Type> messageTypes)
        //{
        //    var mapper = new MessageMapper();

        //    if (messageTypes == null)
        //        messageTypes = Configure.TypesToScan.Where(MessageConventionExtensions.IsMessageType).ToList();

        //    mapper.Initialize(messageTypes);

        //    var xmlserieliser = new XmlMessageSerializer(mapper);

        //    xmlserieliser.Initialize(messageTypes);

        //    return xmlserieliser;
        //}
    }
}
