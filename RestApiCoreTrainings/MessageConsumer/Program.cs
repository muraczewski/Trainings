using System;
using System.Collections.Generic;
using Amazon;
using Common;

namespace MessageConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> Recipients = new List<string>
            {
                "grzegorz.muraczewski@gmail.com"
            };

        var sqs = new AmazonSQSClientWrapper(RegionEndpoint.EUCentral1, "gmuraczewski-queue"); // TODO move gmuraczewski-queue to config
            Console.WriteLine("AWS SQS Consumer");

            var receivedMessageResponse = sqs.ReceiveMessage();

            if (receivedMessageResponse == null)
            {
                return;
            }

            var pasteBinWrapper = new PastebinWrapper();
            var paste = pasteBinWrapper.CreateBin(receivedMessageResponse.Body);

            var sesClient = new AmazonSESCLientWrapper(RegionEndpoint.EUWest1, "gmuraczewski@pgs-soft.com");
            var response = sesClient.SendMessage(Recipients, "New bin on pastebin was added", paste.Url, new System.Threading.CancellationToken());

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Email sent succesfully");
                sqs.DeleteMessage(receivedMessageResponse.ReceiptHandle);
            }
            else
            {
                Console.WriteLine("Problem with sending email");
            }

            Console.ReadKey();
        }
    }
}
