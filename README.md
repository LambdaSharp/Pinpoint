# Amazon Pinpoint Challenge

In this challenge, we're going to explore [Amazon Pinpoint](https://aws.amazon.com/pinpoint/) to send SMS messages.

![](images/AmazonPinpoint.png)

## Level 0: Prerequisites

### Install SDK & Tools
Make sure the following tools are installed.

- [Download and install the .NET Core SDK](https://dotnet.microsoft.com/download)
- [Download and install Git Command Line Interface](https://git-scm.com/downloads)

### Setup AWS Account and CLI
The challenge requires an AWS account. AWS provides a [_Free Tier_](https://aws.amazon.com/free/), which is sufficient for most challenges.

- [Create an AWS Account](https://aws.amazon.com)

### Clone GitHub Repository
Next, you will need to clone this repo into your working directory:

```bash
git clone https://github.com/LambdaSharp/Pinpoint.git
```

### Setup LambdaSharp Deployment Tier
The following command uses the `dotnet` CLI to install the LambdaSharp CLI.

```bash
dotnet tool install --global LambdaSharp.Tool
```

**NOTE:** if you have installed LambdaSharp.Tool in the past, you will need to run `dotnet tool update -g LambdaSharp.Tool` instead.

The following command uses the LambdaSharp CLI to create a new deployment tier on the default AWS account. Specify another account using `--aws-profile ACCOUNT_NAME`.

```bash
lash init --quick-start
```

## Level 1 - Setting up SMS

1. Create a new project with AWS Pinpoint found in the AWS console
2. Configure SMS & Voice
    - Request a long code.
3. Send a simple test message to your mobile phone

## Level 2 - Launching a Campaign

1. Create a segment
    a. Use Import CSV file
    b. Download the example CSV for formatting
    c. Update the CSV file with mobile numbers & names for all group members
        - Use International phone numbers notation (e.g. 16195551234)
2. Create a template that dynamically uses an attribute from your segment
3. Create a campaign using the segment and template you created
4. Choose to launch campaign immediately to get your personalized text message!

## Level 3 - Two Way Customer Communication

1. Go to the `SurveySms/function.cs` file and update APP_ID with the Project ID from the web console
2. Deploy the LambdaSharp project

```bash
lash deploy
```

**NOTE:** Please wait for deploy to finish before moving to the next step

- Enable two-way SMS by going to Settings > SMS and voice > click on your long code number then enable two-way SMS
- Select the SNS topic with `My.Pinpoint` in the title
- Reply to the text from your long code number and see the message appear in your CloudWatch logs. (Go to _CloudFormation > My-Pinpoint > Resources_ then click on the `PinpointSmsTopic`) link

Here is an example of what a message looks like:

```json
{
  "originationNumber": "+XXXXXXXXXX",
  "destinationNumber": "+XXXXXXXXXX",
  "messageKeyword": "keyword_045616484003",
  "messageBody": "This is my response ",
  "inboundMessageId": "8dc9d631-9133-5232-82a8-808675b4c07b",
  "previousPublishedMessageId": "k7hd8hsna3vr9oqf49seqd5opnq302n64divurg0"
}
```

- Respond to the long code phone number with a question to the customer (via SMS in lambda function).
    - Checkout the SDK information: <https://docs.aws.amazon.com/pinpoint/latest/developerguide/send-messages-sms.html>

## Level 4 - State management

- Ask 5 questions to a customer via 5 separate SMS messages. They must respond to each question before moving to the next.
- Provide a summary to the customer at the end of the 5th question.

### Resources

<https://docs.aws.amazon.com/AmazonS3/latest/dev/UploadObjSingleOpNET.html>
<https://docs.aws.amazon.com/AmazonS3/latest/dev/RetrievingObjectUsingNetSDK.html>

## BOSS - Completion Event

Based on level 4, send an event to AWS Pinpoint notifying that the customer completed the survey. Show this to us at the end of the night!

### Resources

<https://docs.aws.amazon.com/cli/latest/reference/pinpoint/put-events.html>
<https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Pinpoint/TPinpointClient.html>
<https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/Pinpoint/MPinpointPutEventsPutEventsRequest.html>

## Tear down is important

- Go to Pinpoint > All projects > LambdaSharp > Settings > SMS and voice
- Select the phone number and click Remove long code
