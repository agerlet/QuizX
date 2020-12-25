#!/bin/bash

cd ./Teacher.Lambda/src/Teacher.Lambda/
dotnet restore
dotnet lambda deploy-function

cd ../../../Student.Lambda/src/Student.Lambda/
dotnet restore 
dotnet lambda deploy-function

cd ../../../student
yarn install
yarn build

cd ../teacher
yarn install
yarn build
aws s3 sync ./build s3://teacher.quizx.cc/

cd ../