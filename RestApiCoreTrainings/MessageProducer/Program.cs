using Amazon;
using Common;
using System;
using System.Collections.Generic;

namespace MessageProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var correctAnswers = new List<char>
            {
                'y', 'Y', 'n', 'N'
            };

            var sqs = new AmazonSQSClientWrapper("gmuraczewski-queue", RegionEndpoint.EUCentral1); // TODO move gmuraczewski-queue to config

            while (true)
            {
                Console.WriteLine("AWS SQS Producer");
                Console.WriteLine("Type message to send");
                var message = Console.ReadLine();

                Console.WriteLine("Are you sure you want to send message? Y/N");
                Console.WriteLine(message);

                ConsoleKeyInfo answer;

                do
                {
                    answer = Console.ReadKey();
                    Console.WriteLine();
                } while (!correctAnswers.Contains(answer.KeyChar));

                if (answer.KeyChar == 'y' || answer.KeyChar == 'Y')
                {
                    sqs.SendMessage(message, new System.Threading.CancellationToken());
                }
            }
        }
    }
}
