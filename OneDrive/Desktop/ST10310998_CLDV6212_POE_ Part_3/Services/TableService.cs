using Azure.Data.Tables;
using ST10310998_CLDV6212_POE__Part_1.Models;
using System.Threading.Tasks;

namespace ST10310998_CLDV6212_POE__Part_1.Services
{
    public class TableService
    {
        private readonly TableClient _tableClient;

        // Constructor initializes the TableClient for the "CustomerProfiles" table
        public TableService(TableServiceClient tableServiceClient)
        {
            _tableClient = tableServiceClient.GetTableClient("CustomerProfiles");
            _tableClient.CreateIfNotExists();
        }

        // Add a new customer profile entity to the table
        public async Task AddEntityAsync(CustomerProfile profile)
        {
            await _tableClient.AddEntityAsync(profile);
        }

        // Retrieve an entity based on PartitionKey and RowKey
        public async Task<CustomerProfile> GetEntityAsync(string partitionKey, string rowKey)
        {
            return await _tableClient.GetEntityAsync<CustomerProfile>(partitionKey, rowKey);
        }

        // Delete an entity using PartitionKey and RowKey
        public async Task DeleteEntityAsync(string partitionKey, string rowKey)
        {
            await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }
    }
}
