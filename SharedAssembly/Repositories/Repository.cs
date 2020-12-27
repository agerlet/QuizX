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
        private const string OverrideTableName = "QuizX";
        private readonly IDynamoDBContext _dynamoDbContext;
        private static readonly DynamoDBOperationConfig DynamoDbOperationConfig = new()
        {
            Conversion = DynamoDBEntryConversion.V2,
            IsEmptyStringValueEnabled = true,
            OverrideTableName = OverrideTableName
        };

        public Repository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }
        
        public async Task Save(QuizAnswerModel model)
        {
            await _dynamoDbContext.SaveAsync(model, DynamoDbOperationConfig);
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
            
            var search = _dynamoDbContext.ScanAsync<QuizAnswerModel>(scanConditions, DynamoDbOperationConfig);
            return await search.GetRemainingAsync();
        }
    }
}