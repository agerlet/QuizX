#!/bin/bash

cd ./Teacher.Lambda/src/Teacher.Lambda/
dotnet lambda deploy-function quizx-query

cd ../../../Student.Lambda/src/Student.Lambda/ 
dotnet lambda deploy-function quizx-answer

cd ../../../