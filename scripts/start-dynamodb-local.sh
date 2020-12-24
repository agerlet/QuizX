#!/bin/bash

docker run -p 8000:8000 -d amazon/dynamodb-local -jar DynamoDBLocal.jar -sharedDb

aws dynamodb create-table \
  --table-name QuizX \
  --attribute-definitions AttributeName=QuizId,AttributeType=S \
                          AttributeName=StudentId,AttributeType=S \
  --key-schema AttributeName=QuizId,KeyType=HASH \
               AttributeName=StudentId,KeyType=RANGE \
  --provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 \
  --endpoint-url=http://localhost:8000