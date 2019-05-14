using System;
using System.Linq;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace MessageConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("Amazon SQS");
            Console.WriteLine("******************************************\n");
            var sqs = new AmazonSQSClient(RegionEndpoint.EUCentral1);

            var queueUrl = sqs.GetQueueUrlAsync("gmuraczewski-queue").Result.QueueUrl;

            var receiveMessageRequest = new ReceiveMessageRequest(queueUrl);            

            var receivedMessageResponse = sqs.ReceiveMessageAsync(receiveMessageRequest).Result.Messages.FirstOrDefault();   

            if (receivedMessageResponse == null)
            {
                return;
            }

            var pasteBinWrapper = new PastebinWrapper();
            var paste = pasteBinWrapper.CreateBin(receivedMessageResponse.Body);

            // TODO send email with paste.Url;

            var deleteMessageRequest = new DeleteMessageRequest(queueUrl, receivedMessageResponse.ReceiptHandle);
            sqs.DeleteMessageAsync(deleteMessageRequest);        

            Console.ReadKey();
        }
    }
}
