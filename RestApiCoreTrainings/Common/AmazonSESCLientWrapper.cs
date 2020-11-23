using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class AmazonSESClientWrapper
    {
        private readonly AmazonSimpleEmailServiceClient _client;
        private readonly string _sourceEmail;

        public AmazonSESClientWrapper(RegionEndpoint regionEndpoint, string sourceEmail)
        {
            _client = new AmazonSimpleEmailServiceClient(regionEndpoint);
            _sourceEmail = sourceEmail;
        }

        public async Task<SendEmailResponse> SendMessageAsync(List<string> recipients, string subject, string message, CancellationToken cancellationToken)
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
                var response = await _client.SendEmailAsync(sendRequest, cancellationToken);

                return response;
            }
            catch (System.Exception)
            {
                return new SendEmailResponse
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }
        }
    }
}
