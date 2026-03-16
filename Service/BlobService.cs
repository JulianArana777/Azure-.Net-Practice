using System.Diagnostics;
using System.Reflection.Metadata;
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

        public async Task CreateBlob(string name, IFormFile file, string containerName, Dictionary<string, string> metadata)
        {
            var container = _client.GetBlobContainerClient(containerName);

            var blob = container.GetBlobClient(name);

            using var stream = file.OpenReadStream();

            await blob.UploadAsync(stream, true);

            if (metadata != null)
            {
                await blob.SetMetadataAsync(metadata);
            }
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

        public async Task<List<BlobModel>> GetAllBlobsByUri(string ContainerName)
        {
            BlobContainerClient containerclient = _client.GetBlobContainerClient(ContainerName);
            var blobs = containerclient.GetBlobsAsync();
            List <BlobModel> names = new List<BlobModel>();

            await foreach (var blob in blobs)
            {
                var blobclient = containerclient.GetBlobClient(blob.Name);
                BlobModel model = new()
                {
                    URL= blobclient.Uri.AbsoluteUri
                };
                BlobProperties properties = await blobclient.GetPropertiesAsync();
                if (properties.Metadata.ContainsKey("title"))
                {
                    model.Title = properties.Metadata["title"];
                }
                if (properties.Metadata.ContainsKey("comment"))
                {
                    model.Title = properties.Metadata["comment"];
                }

                names.Add(model);
            }
            return names;

        }
    }
}