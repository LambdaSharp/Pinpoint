using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using LambdaSharp;
using LambdaSharp.SimpleNotificationService;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace My.CustomerSurvey.SurveySms {

    public class Message {

        //--- Properties ---

        public string MessageBody { get; set; }
        public string DestinationNumber { get; set; }   
        public string OriginationNumber { get; set; }
        public string InboundMessageId { get; set; }    
    }

    public class Function : ALambdaTopicFunction<Message> {

        //--- Methods ---
        public override async Task InitializeAsync(LambdaConfig config) {

            // TO-DO: add function initialization and reading configuration settings
        }

        public override async Task ProcessMessageAsync(Message message) {
            await SaveToCloudwatchAsync(message);
            // TO-DO: add business logic
        }

        public async Task SaveToCloudwatchAsync(Message message) {
            // LogInfo($"Message.Text = {message.Text}");
            LogInfo($"CurrentRecord.Message = {CurrentRecord.Message}");
            LogInfo($"CurrentRecord.MessageAttributes = {CurrentRecord.MessageAttributes}");
            foreach(var attribute in CurrentRecord.MessageAttributes) {
                LogInfo($"CurrentRecord.MessageAttributes.{attribute.Key} = {attribute.Value}");
            }
            LogInfo($"CurrentRecord.MessageId = {CurrentRecord.MessageId}");
            LogInfo($"CurrentRecord.Signature = {CurrentRecord.Signature}");
            LogInfo($"CurrentRecord.SignatureVersion = {CurrentRecord.SignatureVersion}");
            LogInfo($"CurrentRecord.SigningCertUrl = {CurrentRecord.SigningCertUrl}");
            LogInfo($"CurrentRecord.Subject = {CurrentRecord.Subject}");
            LogInfo($"CurrentRecord.Timestamp = {CurrentRecord.Timestamp}");
            LogInfo($"CurrentRecord.TopicArn = {CurrentRecord.TopicArn}");
            LogInfo($"CurrentRecord.Type = {CurrentRecord.Type}");
            LogInfo($"CurrentRecord.UnsubscribeUrl = {CurrentRecord.UnsubscribeUrl}");
        }
    }
}
