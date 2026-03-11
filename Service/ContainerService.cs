using System.Diagnostics;
using API.Interface;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace API.Service
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _client;
        public ContainerService(BlobServiceClient client)
        {
            _client=client;
        }

        public async Task CreateContainer(string name)
        {
            BlobContainerClient client = _client.GetBlobContainerClient(name);
            await client.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteBlob(string name)
        {
            BlobContainerClient client = _client.GetBlobContainerClient(name);
            await client.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainers()
        {
            List<String> ContainerName = new ();
            await foreach(BlobContainerItem item in _client.GetBlobContainersAsync())
            {
                ContainerName.Add(item.Name);
            }

            return ContainerName;
        }

        public Task<List<string>> GetAllContainersAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}