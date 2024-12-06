using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ST10310998_CLDV6212_POE__Part_1.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadFileAsync(string containerName, string blobName, Stream content, string contentType = null)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

                var blobClient = containerClient.GetBlobClient(blobName);

                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType ?? "application/octet-stream"
                };

                // Correct the order of parameters in the UploadAsync method call
                await blobClient.UploadAsync(content, new BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeaders
                });
            }
            catch (Exception ex)
            {
                // Log exception (consider using a logging framework)
                Console.WriteLine($"An error occurred while uploading the file: {ex.Message}");
                throw; // Re-throw the exception to let the caller handle it
            }
        }
    }
}
