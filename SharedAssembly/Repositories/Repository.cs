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
        public async Task Save(QuizAnswerModel model)
        {
            using var client = new AmazonDynamoDBClient();
            using var dbContext = new DynamoDBContext(client);
            var dynamoDbOperationConfig = new DynamoDBOperationConfig
            {
                Conversion = DynamoDBEntryConversion.V2,
                IsEmptyStringValueEnabled = true
            };
            await dbContext.SaveAsync(model, dynamoDbOperationConfig);
        }

        public async Task<List<QuizAnswerModel>> Query(string quizId)
        {
            using var client = new AmazonDynamoDBClient();
            using var dbContext = new DynamoDBContext(client);

            var scanConditions = new[] {new ScanCondition("QuizId", ScanOperator.Equal, quizId)};
            var search = dbContext.ScanAsync<QuizAnswerModel>(scanConditions);
            return await search.GetRemainingAsync();
        }
    }
}