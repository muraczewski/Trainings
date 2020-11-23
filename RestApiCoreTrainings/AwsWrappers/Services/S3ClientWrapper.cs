using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using AWSWrappers.Interfaces;
using Common;

namespace AWSWrappers.Services
{
    public class S3ClientWrapper : IS3ClientWrapper
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IS3Provider _s3Provider;

        public S3ClientWrapper(IAmazonS3 amazonS3, IS3Provider s3Provider)
        {
            _amazonS3 = amazonS3;
            _s3Provider = s3Provider;
        }

        public async Task<Result> UploadAsync(string filePath, string key)
        {
            var putObjectRequest = new PutObjectRequest // todo fill information about file
            {
                BucketName = _s3Provider.BucketName,
                FilePath = filePath,
                ContentType = "",
                Key = key,
            };

            var putObjectResponse = await _amazonS3.PutObjectAsync(putObjectRequest);

            if (putObjectResponse.HttpStatusCode == HttpStatusCode.OK) //todo
            {
                return new Result();
            }
            
            return new Result(false, "An error occured during upload file");
        }
    }
}
