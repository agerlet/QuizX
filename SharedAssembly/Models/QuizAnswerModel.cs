using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace SharedAssembly.Models
{
    [DynamoDBTable("QuizX")]
    public class QuizAnswerModel
    {
        [DynamoDBHashKey]
        public string QuizId { get; set; }
        [DynamoDBRangeKey]
        public string StudentId { get; set; }
        public List<string> Answers { get; set; }
        public DateTime ArriveAt { get; set; }
        public DateTime? CompleteAt { get; set; }
    }
}