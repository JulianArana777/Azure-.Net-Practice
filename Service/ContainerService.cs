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

        public async Task DeleteContainer(string name)
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

        public async Task<List<string>> GetAllContainersAndBlobs()
        {
            List<string> name=new();
            name.Add("-----Account Name: "+_client.AccountName+"-----");
            name.Add("----------------------------------------------------------------");
            await foreach (BlobContainerItem item in _client.GetBlobContainersAsync())
            {
                name.Add("-----"+item.Name);
                BlobContainerClient _Container = _client.GetBlobContainerClient(item.Name);
                await foreach(BlobItem blobItem in _Container.GetBlobsAsync())
                {
                    var BlobCL = _Container.GetBlobClient(blobItem.Name);
                    BlobProperties properties = await BlobCL.GetPropertiesAsync();
                    string tempblop = blobItem.Name;
                    if (properties.Metadata.ContainsKey("titleS"))
                    {
                        tempblop += "("+ properties.Metadata["title"] + ")";
                    }
                    name.Add(">>"+tempblop);

                }
                name.Add("----------------------------------------------------------------");
            }
            return name;

        }
    }
}