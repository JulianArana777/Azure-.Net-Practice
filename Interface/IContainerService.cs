namespace API.Interface
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContainersAndBlobs();
        Task<List<string>> GetAllContainers();
        Task CreateContainer(string name);
        Task DeleteContainer(string name);
    }
}