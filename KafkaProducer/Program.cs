using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    string message;
    
    do 
    {
        Console.WriteLine("Enter a message:");
        message = Console.ReadLine();

        var kafkaMessage = new Message<Null, string>
        {
            Value = message
        };

        producer.Produce("test_topic", kafkaMessage, deliveryReport =>
        {
            if (deliveryReport.Error.Reason is not "Success")
            {
                Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
            }
            else
            {
                Console.WriteLine($"Message delivered to '{deliveryReport.TopicPartitionOffset}'");
            }
        });
    } while (message != null);
    
}