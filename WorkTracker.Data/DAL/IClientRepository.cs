using WorkTracker.Data.Model;

namespace WorkTracker.Data.DAL
{
    public interface IClientRepository
    {
        Task AddClient(Client client);
        Task DeleteClient(int id);
        Task<Client?> GetClient(int id);
        Task<ICollection<Client>> GetClients();
        Task UpdateClient(Client client);
    }
}