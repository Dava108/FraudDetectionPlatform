using System.Text.Json;
using Confluent.Kafka;

namespace TransactionApi.Services;

public class KafkaProducerService(IConfiguration configuration) : IKafkaProducerService
{
    private readonly string _bootstrapServers =
        configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

    public async Task PublishAsync<T>(string topic, T message)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _bootstrapServers
        };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var json = JsonSerializer.Serialize(message);

        await producer.ProduceAsync(
            topic,
            new Message<Null, string>
            {
                Value = json
            });
    }
}