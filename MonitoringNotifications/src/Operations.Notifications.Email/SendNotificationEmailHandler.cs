namespace MonitoringNotifications.Operations.Notifications.Email
{
    using System.Configuration;
    using Messages.Commands;
    using NServiceBus;
    using NServiceBus.Logging;

    public class SendNotificationEmailHandler : IHandleMessages<SendNotificationEmail>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendNotificationEmail));

        public void Handle(SendNotificationEmail message)
        {
            var supperssExternal = (bool)new AppSettingsReader().GetValue("SupperssExternal", typeof(bool));      

            Logger.InfoFormat("Handeling SendNotificationEmail to: {0} and Context {1}", message.To, message.Parameters["Context"]);

            if (supperssExternal) return; 
            
            Logger.DebugFormat("Sending an email...");
            
            // mail.Send(mailMessage);            
        }
    }
}