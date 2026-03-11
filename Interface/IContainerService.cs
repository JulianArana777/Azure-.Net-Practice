namespace API.Interface
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainersAndBlobs();
        Task<List<string>> GetAllContainers();
        Task CreateContainer(string name);
        Task DeleteBlob(string name);
    }
}