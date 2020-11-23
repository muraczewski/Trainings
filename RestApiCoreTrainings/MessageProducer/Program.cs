using Common;
using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using AwsWrappers.Interfaces;
using AwsWrappers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MessageProducer
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main()
        {
            RegisterServices();

            var sqsClientWrapper = _serviceProvider.GetService<ISQSClientWrapper>();

            Console.WriteLine("AWS SQS Producer");

            while (true)
            {                
                Console.WriteLine("Type message to send");
                var message = Console.ReadLine();

                Console.WriteLine("Are you sure you want to send message? Y/N");
                Console.WriteLine(message);

                ConsoleKeyInfo answer;

                do
                {
                    answer = Console.ReadKey();
                    Console.WriteLine();
                } while (!Constants.ConsoleCorrectAnswers.Contains(answer.KeyChar));


                if (Constants.ConsolePositiveAnswers.Contains(answer.KeyChar))
                {
                    var result = await sqsClientWrapper.SendMessageAsync(message, new System.Threading.CancellationToken());
                    Console.WriteLine(result.Message);
                }
            }
        }

        // todo here should be hostBuilder
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
