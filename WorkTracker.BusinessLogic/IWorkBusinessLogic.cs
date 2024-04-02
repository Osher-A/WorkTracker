using WorkTracker.DTO;

namespace WorkTracker.BusinessLogic
{
    public interface IWorkBusinessLogic
    {
        Task AddWork(WorkDTO work);
        Task DeleteWork(int id);
        Task<WorkDTO> GetWork(int id);
        Task<ICollection<WorkDTO>> GetWorks(DateTime from, DateTime to);
        Task UpdateWork(WorkDTO work);
    }
}