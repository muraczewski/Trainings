using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using AwsWrappers.Interfaces;
using Common;

namespace AwsWrappers.Services
{
    // Łukasz has in project: if interface is implemented only by one service then this interface is in the same file as service
    public class SQSClientWrapper : ISQSClientWrapper
    {
        private readonly IAmazonSQS _amazonSqs;
        private readonly ISQSProvider _provider;

        public SQSClientWrapper(IAmazonSQS amazonSqs, ISQSProvider provider)
        {
            _amazonSqs = amazonSqs;
            _provider = provider;
        }

        public async Task<Result> SendMessageAsync(string message, CancellationToken cancellationToken)
        {
            var messageSize = Encoding.Unicode.GetByteCount(message);

            // todo it should be from config
            if (messageSize > Constants.MaximumSqsMessageSize)
            {
                return new Result(false, "Message to long");
            }

            var sendMessageRequest = new SendMessageRequest(_provider.QueueUrl, message);
            var response = await _amazonSqs.SendMessageAsync(sendMessageRequest, cancellationToken);

            if (response.HttpStatusCode == HttpStatusCode.OK) // TODO check success code
            {
                return new Result(true, $"Finish sending message to {_provider.QueueName}");
            }

            return new Result(false, $"An error occured during sending message to {_provider.QueueName}");
        }

        public async Task<Message> ReceiveMessageAsync(CancellationToken cancellationToken)
        {
            var receiveMessageRequest = new ReceiveMessageRequest(_provider.QueueUrl);

            var receivedMessageResponse = await _amazonSqs.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);

            if (receivedMessageResponse.HttpStatusCode == HttpStatusCode.OK) //todo
            {
                return receivedMessageResponse.Messages.FirstOrDefault();
            }

            return null;
        }

        public async Task<Result> DeleteMessageAsync(string receiptHandle, CancellationToken cancellationToken)
        {
            var deleteMessageRequest = new DeleteMessageRequest(_provider.QueueUrl, receiptHandle);
            var deleteMessageResponse = await _amazonSqs.DeleteMessageAsync(deleteMessageRequest, cancellationToken);

            if (deleteMessageResponse.HttpStatusCode == HttpStatusCode.OK) //todo
            {
                return new Result();
            }

            return new Result(false, "An error occured during delete message");
        }
    }
}
