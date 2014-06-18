namespace NServiceBus.Operations.Notifications
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    public class NotificationCustomeException : ApplicationException
    {
        // Local private members
        private readonly String _exceptionDescription = "";
        protected String _assemblyName = "";
        protected Hashtable _data = null;
        protected DateTime _dateTime;
        protected Int32 _exceptionNumber = 0;
        protected String _exceptionType = "";
        protected String _machineName = "";
        protected String _messageId = "";
        protected String _messageName = "";
        protected String _source = "";
        protected String _stackTrace = "";

        public NotificationCustomeException(string message, string stackTrace, DateTime dateTime, string exceptionType,
            string source)
            : base(message)
        {
            _stackTrace = stackTrace;
            _dateTime = dateTime;
            _exceptionType = exceptionType;
            _source = source;
        }

        protected NotificationCustomeException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            _dateTime = info.GetDateTime("_dateTime");
            _machineName = info.GetString("_machineName");
            _stackTrace = info.GetString("_stackTrace");
            _exceptionType = info.GetString("_exceptionType");
            _assemblyName = info.GetString("_assemblyName");
            _messageName = info.GetString("_messageName");
            _messageId = info.GetString("_messageId");
            _exceptionDescription = info.GetString("_exceptionDescription");
            _data = (Hashtable) info.GetValue("_data", Type.GetType("System.Collections.Hashtable"));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_dateTime", _dateTime);
            info.AddValue("_machineName", _machineName);
            info.AddValue("_stackTrace", _stackTrace);
            info.AddValue("_exceptionType", _exceptionType);
            info.AddValue("_assemblyName", _assemblyName);
            info.AddValue("_messageName", _messageName);
            info.AddValue("_messageId", _messageId);
            info.AddValue("_exceptionDescription", _exceptionDescription);
            info.AddValue("_data", _data, Type.GetType("System.Collections.Hashtable"));
            base.GetObjectData(info, context);
        }
    }
}