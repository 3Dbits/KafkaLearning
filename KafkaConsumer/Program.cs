using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "test-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
{
    consumer.Subscribe("test_topic");

    Console.WriteLine("Consuming messages...");

    while (true)
    {
        var consumeResult = consumer.Consume();

        Console.WriteLine($"Received message: {consumeResult.Message.Value} | " +
            $"Topic: {consumeResult.Topic} | " +
            $"Partition: {consumeResult.Partition} | " +
            $"Offset: {consumeResult.Offset}");
    }
}