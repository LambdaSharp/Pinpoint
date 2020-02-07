# Pinpoint

- SMS is faster... so setup that up.
- Setting a campaign which requires a segment.
- Testing messaging.
- Add a journey but keep it simple.
- two messaging - https://docs.aws.amazon.com/pinpoint/latest/userguide/channels-sms-two-way.html
- do a lambda function listens

## Level 0: Prerequisites

### Install SDK & Tools
Make sure the following tools are installed.
* [Download and install the .NET Core SDK](https://dotnet.microsoft.com/download)
* [Download and install the AWS Command Line Interface](https://aws.amazon.com/cli/)
* [Download and install Git Command Line Interface](https://git-scm.com/downloads)

### Setup AWS Account and CLI
The challenge requires an AWS account. AWS provides a [*Free Tier*](https://aws.amazon.com/free/), which is sufficient for most challenges.
* [Create an AWS Account](https://aws.amazon.com)
* [Configure your AWS profile with the AWS CLI for us-east-1 (N. Virginia)](https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-configure.html#cli-quick-configuration)

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

## Level 1
- Create a new project with AWS Pinpoint found in the AWS console
- Configure SMS & Voice
- Request a long code 
- Send a simple test message to one of the group member's number

## Level 2
- Create a segment: import CSV file (download example CSV for formatting) with SMS numbers & names for all group members
- Create a template that dynamically uses an attribute from your segment
- Create a campaign using the segment and template you created
- Choose to launch campaign immediately to get your personalized text message!

## Level 3
- Clone this repo
- Deploy a lambda sharp project 
```bash
lash deploy 
```
- Enable two-way SMS by going to Settings > SMS and voice > click on your long code number then enable two-way SMS
- Select the SNS topic with `CustomerSurvey` in the title
- Reply to the text from your long code number and see the message appear in your Cloudwatch logs. Here is an example of what a message looks like:
```{
    "originationNumber": "+XXXXXXXXXX",
    "destinationNumber": "+XXXXXXXXXX",
    "messageKeyword": "keyword_045616484003",
    "messageBody": "This is my response ",
    "inboundMessageId": "8dc9d631-9133-5232-82a8-808675b4c07b",
    "previousPublishedMessageId": "k7hd8hsna3vr9oqf49seqd5opnq302n64divurg0"
}```

## Level 4
- Create a survey
- Different actions for various responses (Pinpoint keywords)

## BOSS LEVEL (via code)
- Based on level 3, add a new person to a segment 
- Send a campaign to the segment


## Tear down is important

- Go to Pinpoint > All projects > LambdaSharp > Settings > SMS and voice
- Select the phone number and click Remove long code 

