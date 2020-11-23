using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using AwsWrappers.Interfaces;
using AwsWrappers.Services;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace MessageConsumer
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main()
        {
            RegisterServices();

            var sqsClientWrapper = _serviceProvider.GetService<ISQSClientWrapper>();

            Console.WriteLine("AWS SQS Consumer");

            bool shouldExecute = true;

            while (shouldExecute)
            {
                var receivedMessageResponse = await sqsClientWrapper.ReceiveMessageAsync(new CancellationToken());

                if (receivedMessageResponse == null)
                {
                    Console.WriteLine("No message on queue");
                    Console.ReadKey();
                    return;
                }

                var pasteBinWrapper = new PastebinWrapper();
                var paste = await pasteBinWrapper.CreateBinAsync(receivedMessageResponse.Body);

                var sesClient = new AmazonSESClientWrapper(RegionEndpoint.EUWest1, "gmuraczewski@pgs-soft.com");    // TODO remove harcoded values
                var response = await sesClient.SendMessageAsync(Constants.Recipients, "New bin on pastebin was added", paste.Url, new CancellationToken());

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Email sent successfully");
                    var result = await sqsClientWrapper.DeleteMessageAsync(receivedMessageResponse.ReceiptHandle, new CancellationToken());

                    if (!result.IsSuccess)
                    {
                        Console.WriteLine("Problem with deleting message from queue");
                    }
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

        private static void RegisterServices()
        {
            var awsOptions = new AWSOptions
            {
                Profile = "default",
                Region = RegionEndpoint.EUCentral1,
            };

            _serviceProvider = new ServiceCollection()
                .AddScoped<ISQSClientWrapper, SQSClientWrapper>()
                .AddDefaultAWSOptions(awsOptions)
                .AddAWSService<IAmazonSQS>()
                .AddSingleton<ISQSProvider>(s => new SQSProvider("https://sqs.eu-central-1.amazonaws.com/890769921003/gmuraczewski-queue", "gmuraczewski-queue"))
                .BuildServiceProvider();
        }
    }
}
