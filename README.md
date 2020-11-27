# What is xed?

The xed is a tool to help education.

The xed started from a simple idea/question: Choose the right words to complete the dialogue.

In the beginning, it's just a paragraph with some words being pulled out. As the replacements, some textboxes are in the positions of the missing words.

Then, in the second iteration, the textboxes are replaced by the draggable boxes so that the user can drag the text into the paragraph.

The third iteration, the whole page should be able to work with the devices with touch screen. 

## Architecture

The xed comes with 3 parts: 
- the service 
- student portal
- teacher portal 

### The service

The service is a dotnet core 3.1 WebAPI running in the background to collect data from student portal and get ready to be queried by the teacher portal.

#### Restore

    # At the root of this repository, run the following command
    $ dotnet restore

#### Build

    # At the root of this repository, run the following command
    $ dotnet build

#### Test

    # At the root of this repository, run the following command
    $ dotnet test
    
#### Run
    
    # At the root of this repository, run the following command
    $ dotnet run --project ./api/api.csproj
    
#### Build docker container

    # At the root of this repository, run the following command
    # [tag] should be replaced by the actual tag name e.g. 0.03
    # Go to Docker Hub to find out the latest tag
    $ docker build -t agerlet/xed-api:[tag] .
    
    # login to docker hub
    $ docker login 
    $ docker push agerlet/xed-api:[tag]

### The student portal

The student portal is a react project to present the quiz to the students and report the student activities.

#### Install

    # At the student directory of this repository, run the following command
    $ yarn install

#### Build

    # At the student directory of this repository, run the following command
    $ yarn build

#### Test

    # At the student of this repository, run the following command
    $ yarn test
    
#### Run
    
    # At the student of this repository, run the following command
    $ yarn start

### The teacher portal

The teacher portal is a react project to present the answers to the teacher.

#### Install

    # At the teacher directory of this repository, run the following command
    $ yarn install

#### Build

    # At the teacher directory of this repository, run the following command
    $ yarn build

#### Test

    # At the teacher of this repository, run the following command
    $ yarn test
    
#### Run
    
    ## At the teacher of this repository, run the following command
    $ yarn start
