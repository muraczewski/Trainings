using AwsWrappers.Interfaces;

namespace AwsWrappers.Services
{
    public class SQSProvider : ISQSProvider
    {
        // here shouldn't be argument
        // queueName should be from config
        // call to aws for queueUrl
        public SQSProvider(string queueUrl, string queueName)
        {
            QueueUrl = queueUrl;
            QueueName = queueName;
        }

        public string QueueUrl { get; set; }
        public string QueueName { get; set; }
    }
}
