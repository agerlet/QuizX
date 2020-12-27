#!/bin/bash

cd ./Teacher.Lambda/src/Teacher.Lambda/
dotnet restore
dotnet lambda deploy-function

cd ../../../Student.Lambda/src/Student.Lambda/
dotnet restore 
dotnet lambda deploy-function

cd ../../../student
cp ./src/config.release.json ./src/config.json
yarn install
yarn build
aws s3 sync ./build s3://student.quizx.cc/
cp ./src/config.development.json ./src/config.json


cd ../teacher
cp ./src/config.release.json ./src/config.json
yarn install
yarn build
aws s3 sync ./build s3://teacher.quizx.cc/
cp ./src/config.development.json ./src/config.json

cd ../