# What is QuizX?

The QuizX is a tool to help education.

The QuizX started from a simple idea/question: Choose the right words to complete the dialogue.

In the beginning, it's just a paragraph with some words being pulled out. As the replacements, some textboxes are in the positions of the missing words.

Then, in the second iteration, the textboxes are replaced by the draggable boxes so that the user can drag the text into the paragraph.

The third iteration, the whole page should be able to work with the devices with touch screen. 

## Architecture

The QuizX comes with 4 parts: 
- student Lambda Function 
- student portal
- teacher Lambda Function
- teacher portal 

### The Lambda Functions

The Lambda Functions are created by AWS SAM (Hello World Example) to collect data from student portal and get ready to be queried by the teacher portal.

For more info, please find [this](./TeacherFunction/README.md) for teacher Lambda Function and [this](./StudentFunction/README.md) for student Lambda Function.

    # In the sam folder, 
    $ sam local start-api 

For more info about AWS SAM CLI, please refer [AWS Documentation](https://docs.aws.amazon.com/serverless-application-model/)

### The student portal

The student portal is a react project to present the quiz to the students and report the student activities.

For more info, please find this [README.md](./student/README.md) for student portal. 

### The teacher portal

The teacher portal is a react project to present the answers to the teacher.

For more info, please find this [README.md](./teacher/README.md) for teacher portal.

## Scripts

Some of the unit tests reply on the docker container dynamodb-local. 

    # At the root of this repository, run the following script to start dynamodb-local.
    $ ./scripts/start-dynamodb-local.sh

    # At the root of this repository, run the following script to stop & remove dynamodb-local.
    $ ./scripts/stop-dynamodb-local.sh

