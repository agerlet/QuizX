using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using SharedAssembly.Models;

namespace SharedAssembly.Repositories
{
    public class Repository
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public Repository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }
        
        public async Task Save(QuizAnswerModel model)
        {
            var dynamoDbOperationConfig = new DynamoDBOperationConfig
            {
                Conversion = DynamoDBEntryConversion.V2,
                IsEmptyStringValueEnabled = true,
                OverrideTableName = "QuizX"
            };
            await _dynamoDbContext.SaveAsync(model, dynamoDbOperationConfig);
        }

        public async Task<List<QuizAnswerModel>> Query(string quizId, string studentId = null)
        {
            var scanConditions = new List<ScanCondition>
            {
                new("QuizId", ScanOperator.Equal, quizId)
            };
            if (!string.IsNullOrWhiteSpace(studentId))
            {
                scanConditions.Add(new ScanCondition("StudentId", ScanOperator.Equal, studentId));
            }

            var dynamoDbOperationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = "QuizX"
            };
            var search = _dynamoDbContext.ScanAsync<QuizAnswerModel>(scanConditions, dynamoDbOperationConfig);
            return await search.GetRemainingAsync();
        }
    }
}