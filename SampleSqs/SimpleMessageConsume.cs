using Amazon.SQS;
using Amazon.SQS.Model;

namespace SampleSqs;

public class SimpleMessageConsume
{
    public async Task ConsumeMessageAsync()
    {
        var sqsClient = new AmazonSQSClient();
        var queueUrl = await sqsClient.GetQueueUrlAsync("Client");

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl.QueueUrl
        };

        while (true)
        {
            await Task.Delay(1000);
            var result = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);
            foreach (var message in result.Messages)
            {
                Console.WriteLine(result.Messages[0].MessageId);
                await sqsClient.DeleteMessageAsync(queueUrl.QueueUrl, message.ReceiptHandle);
            }
        }
    }
}