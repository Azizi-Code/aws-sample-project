using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using SampleSqs;

var sqsClient = new AmazonSQSClient();
var queueUrl = await sqsClient.GetQueueUrlAsync("Client");

var request = new SendMessageRequest
{
    QueueUrl = queueUrl.QueueUrl,
    MessageBody = JsonSerializer.Serialize(new { Message = "Hello World!" })
};

await sqsClient.SendMessageAsync(request);

// Call consume method
var test = new SimpleMessageConsume();
await test.ConsumeMessageAsync();