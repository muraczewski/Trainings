using System;
using System.Threading;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Common
{
    public class AmazonSQSClientWrapper
    {
        private readonly AmazonSQSClient sqs;
        private readonly CreateQueueRequest sqsRequest;
        private readonly CreateQueueResponse createQueueResponse;

        public AmazonSQSClientWrapper(string queueName, RegionEndpoint regionEndpoint)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Amazon SQS");
            Console.WriteLine("******************************************\n");

            sqs = new AmazonSQSClient(regionEndpoint);            
            sqsRequest = new CreateQueueRequest(queueName);
            createQueueResponse = sqs.CreateQueueAsync(sqsRequest).Result;
        }

        public void SendMessage(string message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Sending a message to {sqsRequest.QueueName}\n");
            var sendMessageRequest = new SendMessageRequest(createQueueResponse.QueueUrl, message);
            sqs.SendMessageAsync(sendMessageRequest, cancellationToken);
            Console.WriteLine($"Finish sending message to {sqsRequest.QueueName}\n");            
        }
    }
}
