using Amazon;
using Common;
using System;
using System.Threading.Tasks;

namespace MessageProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sqs = new AmazonSQSClientWrapper(RegionEndpoint.EUCentral1, "gmuraczewski-queue"); // TODO move gmuraczewski-queue to config or put from console
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
                    await sqs.SendMessageAsync(message, new System.Threading.CancellationToken());
                }
            }
        }
    }
}
