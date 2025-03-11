using Amazon.SQS;
using ASP_AWSTest.Response;
using ASP_AWSTest.Constant;
using ASP_AWSTest.IService;
using System.Net;
using Amazon.SQS.Model;

namespace ASP_AWSTest.Service;

public class SQSService : ISQSService
{
    private readonly IAmazonSQS _amazonSQS;
    public SQSService(IAmazonSQS amazonSQS)
    {
        _amazonSQS = amazonSQS;
    }
    public async Task<(Response<SendMessageResponse>, HttpStatusCode)> SendQueueMessageAsync(string queueUrl)
    {
        try
        {
            var messageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = SQSConst.Message + DateTime.Now.ToLongDateString()
            };

            var response = await _amazonSQS.SendMessageAsync(messageRequest);
            return (Response<SendMessageResponse>.SuccessResult(message: "Queue successfull!", data: response), HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return (Response<SendMessageResponse>.FailureResult(ex.Message), HttpStatusCode.InternalServerError);
        }
    }
}
