using API.Interface;

namespace API.Service
{
    public class ContainerService : IContainerService
    {
        public Task CreateContainer(string name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlob(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllContainers()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllContainersAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}