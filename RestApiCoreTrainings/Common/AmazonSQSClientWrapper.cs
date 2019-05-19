using System;
using System.Linq;
using System.Threading;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Common
{
    public class AmazonSQSClientWrapper
    {
        private readonly AmazonSQSClient _sqsClient;
        private readonly CreateQueueRequest _sqsRequest;
        private readonly CreateQueueResponse _createQueueResponse;
        private readonly string _queueUrl; 


        public AmazonSQSClientWrapper(RegionEndpoint regionEndpoint, string queueName)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Amazon SQS");
            Console.WriteLine("******************************************\n");

            _sqsClient = new AmazonSQSClient(regionEndpoint);
            _sqsRequest = new CreateQueueRequest(queueName);
            _createQueueResponse = _sqsClient.CreateQueueAsync(_sqsRequest).Result;
            _queueUrl = _sqsClient.GetQueueUrlAsync(queueName).Result.QueueUrl;
        }

        public void SendMessage(string message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Sending a message to {_sqsRequest.QueueName}\n");
            var sendMessageRequest = new SendMessageRequest(_createQueueResponse.QueueUrl, message);
            _sqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);
            Console.WriteLine($"Finish sending message to {_sqsRequest.QueueName}\n");            
        }

        public Message ReceiveMessage() 
        {
            var receiveMessageRequest = new ReceiveMessageRequest(_queueUrl);

            var receivedMessageResponse = _sqsClient.ReceiveMessageAsync(receiveMessageRequest).Result.Messages.FirstOrDefault();

            return receivedMessageResponse;
        }

        public void DeleteMessage(string receiptHandle)
        {
            var deleteMessageRequest = new DeleteMessageRequest(_queueUrl, receiptHandle);
            _sqsClient.DeleteMessageAsync(deleteMessageRequest);
        }
    }
}
