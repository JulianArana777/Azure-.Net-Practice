using System.Diagnostics;
using API.Interface;
using API.MODEL;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace API.Service
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _client;
        public BlobService(BlobServiceClient client)
        {
            _client = client;
        }

        public async Task CreateBlob(string name, IFormFile file, string ContainerName, BlobModel blobmodel)
        {
            BlobContainerClient blobContainerClient = _client.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(name);
            var httpheaders = new Azure.Storage.Blobs.Models.BlobHttpHeaders()
            {
              ContentType = file.ContentType  
            };
            await blobClient.UploadAsync(file.OpenReadStream(),httpheaders);

        }

        public async Task DeleteBlob(string ContainerName, string Name)
        {
            BlobContainerClient blobContainerClient = _client.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(Name);
            await blobClient.DeleteIfExistsAsync();

        }

        public async Task<string> GetABlob(string containerName, string name)
        {
            BlobContainerClient blobContainerClient = _client.GetBlobContainerClient(containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(name);

            if (await blobClient.ExistsAsync())
            {
                return blobClient.Uri.AbsoluteUri;
            }

            return null;
        }

        public async Task<List<string>> GetAllBlobs(string ContainerName)
        {
            BlobContainerClient blobContainerClient = _client.GetBlobContainerClient(ContainerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            List<string> blobnames = new List<string>();
            await foreach (var blob in blobs)
            {
                blobnames.Add(blob.Name);
            }
            return blobnames;

        }

        public Task<List<BlobModel>> GetAllBlobsByUri(string ContainerName)
        {
            throw new NotImplementedException();
        }
    }
}