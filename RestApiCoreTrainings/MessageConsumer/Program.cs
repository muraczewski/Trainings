using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Common;

namespace MessageConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> Recipients = new List<string>
            {
                "grzegorz.muraczewski@gmail.com"
            };

            var sqs = new AmazonSQSClientWrapper(RegionEndpoint.EUCentral1, "gmuraczewski-queue"); // TODO move gmuraczewski-queue to config
            Console.WriteLine("AWS SQS Consumer");

            bool shouldExecute = true;

            while (shouldExecute)
            {
                var receivedMessageResponse = await sqs.ReceiveMessageAsync(new System.Threading.CancellationToken());

                if (receivedMessageResponse == null)
                {
                    Console.WriteLine("No message on queue");
                    Console.ReadKey();
                    return;
                }

                var pasteBinWrapper = new PastebinWrapper();
                var paste = await pasteBinWrapper.CreateBinAsync(receivedMessageResponse.Body);

                var sesClient = new AmazonSESCLientWrapper(RegionEndpoint.EUWest1, "gmuraczewski@pgs-soft.com");
                var response = await sesClient.SendMessageAsync(Recipients, "New bin on pastebin was added", paste.Url, new System.Threading.CancellationToken());

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Email sent succesfully");
                    await sqs.DeleteMessageAsync(receivedMessageResponse.ReceiptHandle, new System.Threading.CancellationToken());
                }
                else
                {
                    Console.WriteLine("Problem with sending email");
                }

                Console.WriteLine("Do you want to receive next message? y/n");

                ConsoleKeyInfo answer;

                do
                {
                    answer = Console.ReadKey();
                    Console.WriteLine();
                } while (!Constants.ConsoleCorrectAnswers.Contains(answer.KeyChar));

                if (Constants.ConsoleNegativeAnswers.Contains(answer.KeyChar))
                {
                    shouldExecute = false;
                }
            }
        }
    }
}
