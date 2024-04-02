using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Data.Model;

namespace WorkTracker.Data.DAL
{
    public interface IWorkRepository
    {
        Task AddWork(Work work);
        Task UpdateWork(Work workDTO);
        Task DeleteWork(int id);
        Task<ICollection<Work>> GetWorks(DateTime From, DateTime To);
        Task<Work?> GetWork(int id);
    }
}
