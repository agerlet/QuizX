#!/bin/bash

docker run -p 8000:8000 -d amazon/dynamodb-local -jar DynamoDBLocal.jar -sharedDb

aws dynamodb create-table \
  --table-name QuizX \
  --attribute-definitions AttributeName=quizId,AttributeType=S \
                          AttributeName=studentId,AttributeType=S \
  --key-schema AttributeName=quizId,KeyType=HASH \
               AttributeName=studentId,KeyType=RANGE \
  --provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 \
  --endpoint-url=http://localhost:8000