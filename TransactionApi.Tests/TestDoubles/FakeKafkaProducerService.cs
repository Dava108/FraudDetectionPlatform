using TransactionApi.Services;

namespace TransactionApi.Tests.TestDoubles;

public class FakeKafkaProducerService : IKafkaProducerService
{
    public Task PublishAsync<T>(string topic, T message)
    {
        return Task.CompletedTask;
    }
}