using API.MODEL;

namespace API.Interface
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(String ContainerName);
        Task<List<BlobModel>> GetAllBlobsByUri(String ContainerName);
        Task<string> GetABlob(String ContainerName, string Name);
        Task CreateBlob(string name, IFormFile file, string containerName, Dictionary<string, string> metadata);
        Task DeleteBlob(string ContainerName, string Name);
    }
}