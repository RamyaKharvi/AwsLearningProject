using System.Net;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using ASP_AWSTest.Constant;
using ASP_AWSTest.IService;
using ASP_AWSTest.Response;

namespace ASP_AWSTest.Service;

public class SNSService : ISNSService
{
    private readonly IAmazonSimpleNotificationService _amazonSnsService;
    public SNSService(IAmazonSimpleNotificationService amazonSimpleNotificationService)
    {
        _amazonSnsService = amazonSimpleNotificationService;
    }
    public async Task<(Response<PublishResponse>, HttpStatusCode)> SendSMSAsync(string phNo)
    {
        try
        {
            var publishRequest = new PublishRequest
            {
                PhoneNumber = phNo,
                Message = SNSConstant.Message,
            };

            var response = await _amazonSnsService.PublishAsync(publishRequest);

            return (Response<PublishResponse>.SuccessResult(message:$"SMS sent to {phNo}.", data:response), HttpStatusCode.OK);

        }
        catch (Exception ex)
        {
            return (Response<PublishResponse>.FailureResult(ex.Message), HttpStatusCode.InternalServerError);
        }
    }

    public async Task<(Response<PublishResponse>, HttpStatusCode)> SendEmailAsync(string topicArn)
    {
        try
        {
            var publishRequest = new PublishRequest
            {
                TopicArn = topicArn,
                Message = SNSConstant.Message,
                Subject = SNSConstant.EmailSubject
            };

            var response = await _amazonSnsService.PublishAsync(publishRequest);

            return (Response<PublishResponse>.SuccessResult(message: "Email sent successfully.", data: response), HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return (Response<PublishResponse>.FailureResult(ex.Message), HttpStatusCode.InternalServerError);
        }
    }
}
