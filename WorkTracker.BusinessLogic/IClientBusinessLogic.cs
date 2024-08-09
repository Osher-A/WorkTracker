using WorkTracker.DTO;

namespace WorkTracker.BusinessLogic
{
    public interface IClientBusinessLogic
    {
        Task AddClient(ClientDTO client);
        Task DeleteClient(int id);
        Task<ClientDTO> GetClient(int id);
        Task<ICollection<ClientDTO>> GetClients();
        Task UpdateClient(ClientDTO client);
    }
}