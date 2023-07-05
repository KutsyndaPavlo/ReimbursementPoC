namespace ReimbursementPoC.Infrustructure.EventBusServiceBus;

public interface IServiceBusPersisterConnection : IAsyncDisposable
{
    ServiceBusClient TopicClient { get; }
    ServiceBusAdministrationClient AdministrationClient { get; }
}