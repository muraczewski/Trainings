namespace AwsWrappers.Interfaces
{
    public interface ISQSProvider
    {
        string QueueUrl { get; set; }
        string QueueName { get; set; }
    }
}
