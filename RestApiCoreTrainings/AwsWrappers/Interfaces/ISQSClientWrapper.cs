using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using Common;

namespace AwsWrappers.Interfaces
{
    public interface ISQSClientWrapper
    {
        Task<Result> SendMessageAsync(string message, CancellationToken cancellationToken);

        Task<Message> ReceiveMessageAsync(CancellationToken cancellationToken);

        Task<Result> DeleteMessageAsync(string receiptHandle, CancellationToken cancellationToken);
    }
}
