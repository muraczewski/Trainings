using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task SendMessageAsync(string message, CancellationToken cancellationToken)
        {
            var messageSize = System.Text.Encoding.Unicode.GetByteCount(message);

            if (messageSize > Constants.MaximumSQSMessageSize)
            {
                Console.WriteLine("Message to long");
                return;
            }

            Console.WriteLine($"Sending a message to {_sqsRequest.QueueName}\n");
            var sendMessageRequest = new SendMessageRequest(_createQueueResponse.QueueUrl, message);

            await _sqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);
            Console.WriteLine($"Finish sending message to {_sqsRequest.QueueName}\n");            
        }

        public async Task<Message> ReceiveMessageAsync(CancellationToken cancellationToken) 
        {
            var receiveMessageRequest = new ReceiveMessageRequest(_queueUrl);

            var receivedMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);

            return receivedMessageResponse.Messages.FirstOrDefault();
        }

        public async Task DeleteMessageAsync(string receiptHandle, CancellationToken cancellationToken)
        {
            var deleteMessageRequest = new DeleteMessageRequest(_queueUrl, receiptHandle);
            await _sqsClient.DeleteMessageAsync(deleteMessageRequest, cancellationToken);
        }
    }
}
