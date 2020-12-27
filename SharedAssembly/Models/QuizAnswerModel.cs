using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace SharedAssembly.Models
{
    [DynamoDBTable("QuizX")]
    public class QuizAnswerModel
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("quizId")]
        public string QuizId { get; set; }
        
        [DynamoDBRangeKey]
        [DynamoDBProperty("studentId")]
        public string StudentId { get; set; }
        
        [DynamoDBProperty("answers")]
        public List<string> Answers { get; set; }
        
        [DynamoDBProperty("arriveAt")]
        public DateTime ArriveAt { get; set; }
        
        [DynamoDBProperty("completeAt")]
        public DateTime? CompleteAt { get; set; }
    }
}