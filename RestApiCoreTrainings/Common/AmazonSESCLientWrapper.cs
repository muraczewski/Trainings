using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Collections.Generic;
using System.Threading;

namespace Common
{
    public class AmazonSESCLientWrapper
    {
        private readonly AmazonSimpleEmailServiceClient _client;
        private readonly string _sourceEmail;

        public AmazonSESCLientWrapper(RegionEndpoint regionEndpoint, string sourceEmail)
        {
            _client = new AmazonSimpleEmailServiceClient(regionEndpoint);
            _sourceEmail = sourceEmail;
        }

        public SendEmailResponse SendMessage(List<string> recipients, string subject, string message, CancellationToken cancellationToken)
        {
            try
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = _sourceEmail,
                    Destination = new Destination(recipients),
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Text = new Content(message),
                        },
                    },
                };
                var response = _client.SendEmailAsync(sendRequest, cancellationToken).Result;

                return response;
            }
            catch (System.Exception e)
            {
                return new SendEmailResponse
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }
        }
    }
}
