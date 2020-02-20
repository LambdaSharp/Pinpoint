using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Pinpoint;
using Amazon.Pinpoint.Model;
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

        // The Pinpoint project/application ID to use when you send this message.
        // Make sure that the SMS channel is enabled for the project or application
        // that you choose. It's in the web console!
        const string APP_ID = "";

        //--- Properties ---
        public AmazonPinpointClient Client { get; private set; }
        public string PinpointBucket { get; private set; }

        //--- Methods ---
        public override async Task InitializeAsync(LambdaConfig config) {
            Client = new AmazonPinpointClient();
            
            // TODO: s3 bucket for storage
            PinpointBucket = config.ReadS3BucketName("PinpointBucket");
        }

        public override async Task ProcessMessageAsync(Message message) {
            SaveToCloudwatch();

            // Uncomment the code below when you want to reply back to someone
            // await RespondToMessage(message.DestinationNumber, message.OriginationNumber, "yes, i'm listening");
        }

        private async Task RespondToMessage(string from, string to, string text) {
            var sendMessagesRequest = new SendMessagesRequest {
                ApplicationId = APP_ID,
                MessageRequest = new MessageRequest {
                    Addresses = new Dictionary<string, AddressConfiguration> {
                        [to] = new AddressConfiguration {
                            ChannelType = ChannelType.SMS
                        }
                    },
                    MessageConfiguration = new DirectMessageConfiguration {
                        SMSMessage = new SMSMessage {
                            Body = text,
                            MessageType = "TRANSACTIONAL",
                            OriginationNumber = from
                        }
                    }
                }
            };
            try {
                var response = await Client.SendMessagesAsync(sendMessagesRequest);
                LogInfo(response.MessageResponse.RequestId);
                foreach(var messageResponseResult in response.MessageResponse.Result) {
                    LogInfo($"{messageResponseResult.Key}:{messageResponseResult.Value}");
                }
            } catch(System.Exception e) {
                LogError(e);
            }
        }

        private void SaveToCloudwatch() {
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
