# Pinpoint

![](https://d1.awsstatic.com/product-marketing/Pinpoint/Product-page-diagram_Amazon-Pinpoint-with-Journeys-@2x.59f755aedb4ea26ddbdeade13529046129c3d7a1.png)

## Level 0: Prerequisites

### Install SDK & Tools

Make sure the following tools are installed.

- [Download and install the .NET Core SDK](https://dotnet.microsoft.com/download)
- [Download and install the AWS Command Line Interface](https://aws.amazon.com/cli/)
- [Download and install Git Command Line Interface](https://git-scm.com/downloads)

### Setup AWS Account and CLI

The challenge requires an AWS account. AWS provides a [_Free Tier_](https://aws.amazon.com/free/), which is sufficient for most challenges.

- [Create an AWS Account](https://aws.amazon.com)
- [Configure your AWS profile with the AWS CLI for us-east-1 (N. Virginia)](https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-configure.html#cli-quick-configuration)

### Setup LambdaSharp Deployment Tier

The following command uses the `dotnet` CLI to install the LambdaSharp CLI.

```bash
dotnet tool install --global LambdaSharp.Tool
```

**NOTE:** if you have installed LambdaSharp.Tool in the past, you will need to remove it first by running `dotnet tool uninstall -g LambdaSharp.Tool` first.

The following command uses the LambdaSharp CLI to create a new deployment tier on the default AWS account. Specify another account using `--aws-profile ACCOUNT_NAME`.

```bash
lash init --quick-start
```

## Level 1 - Setting up SMS

- Create a new project with AWS Pinpoint found in the AWS console
- Configure SMS & Voice
  - Request a long code.
- Send a simple test message to one of the group member's number

## Level 2 - Launching a Campaign

- Create a segment: import CSV file (download example CSV for formatting) with SMS numbers & names for all group members
- Create a template that dynamically uses an attribute from your segment
- Create a campaign using the segment and template you created
- Choose to launch campaign immediately to get your personalized text message!

## Level 3 - Two Way Customer Communication

- Clone this repo
- Go to the src/SurveySms/function.cs file and update APP_ID with the Project ID from the web console
- Deploy a lambda sharp project

```bash
lash deploy
```

** NOTE ** Please wait for deploy to finish before moving to the next step

- Enable two-way SMS by going to Settings > SMS and voice > click on your long code number then enable two-way SMS
- Select the SNS topic with `CustomerSurvey` in the title
- Reply to the text from your long code number and see the message appear in your CloudWatch logs. Here is an example of what a message looks like:

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

## Boss - State management

- Ask 5 questions to a customer via 5 separate SMS messages. They must respond to each question before moving to the next.
- Provide a summary to the customer at the end of the 5th question.

## Tear down is important

- Go to Pinpoint > All projects > LambdaSharp > Settings > SMS and voice
- Select the phone number and click Remove long code
