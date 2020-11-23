using AWSWrappers.Interfaces;

namespace AWSWrappers.Services
{
    public class S3Provider : IS3Provider
    {
        public S3Provider(string bucketName)
        {
            BucketName = bucketName;
        }

        public string BucketName { get; set; }
    }
}
