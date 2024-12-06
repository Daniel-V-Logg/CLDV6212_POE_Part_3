using Azure.Storage.Queues;
using System.Threading.Tasks;

namespace ST10310998_CLDV6212_POE__Part_1.Services
{
    public class QueueStorageService
    {
        private readonly QueueServiceClient _queueServiceClient;

        public QueueStorageService(QueueServiceClient queueServiceClient)
        {
            _queueServiceClient = queueServiceClient;
        }

        public async Task AddMessageAsync(string queueName, string message)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
        }
    }
}
