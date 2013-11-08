namespace RabbitCompetingConsumers.Messages.Events
{
    public interface IBeginFinancialStatementProcessing : IFinancialStatement
    {
        long MetaDataKey { get; set; }
        long MessageKey { get; set; }
    }
}