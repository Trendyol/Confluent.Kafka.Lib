using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Confluent.Kafka.Utility.Tests.IntegrationTests.Producers
{
    public sealed class KafkaProducer : IKafkaProducer
    {
        private readonly IConfiguration _configuration;

        public KafkaProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task ProduceAsync<TKey, TValue>(string topic, Message<TKey, TValue> message)
        {
            using var producer = new ProducerBuilder<TKey, TValue>(new ProducerConfig
                {
                    BootstrapServers = _configuration.GetValue<string>("BootstrapServers")
                })
                .Build();
            
            await producer.ProduceAsync(topic, message);
        }
    }
}