using Microsoft.EntityFrameworkCore;
using WorkTracker.Data.Model;

namespace WorkTracker.Data.DAL
{
    public sealed class WorkRepository : IWorkRepository
    {
        private WorkContext _db;
        public WorkRepository(WorkContext db)
        {
            _db = db;
        }

        public async Task AddWork(Work work)
        {
            await _db.AddAsync(work);
            await _db.SaveChangesAsync();
            _db.Entry(work).State = EntityState.Detached;
        }

        public async Task DeleteWork(int id)
        {
            var workToDelete = _db.Works.SingleOrDefault(x => x.Id == id);
            if (workToDelete is not null)
            {
                _db.Remove(workToDelete);
                await _db.SaveChangesAsync();
            }

        }

        public async Task<ICollection<Work>> GetWorks(DateTime From, DateTime To)
        {
            return await _db.Works
                .Include(x => x.WorkDetails)
                .Where(w => w.Date >= From && w.Date <= To)
                .ToListAsync();
        }

        public async Task UpdateWork(Work work)
        {
            _db.Update(work);
            await _db.SaveChangesAsync();
            _db.Entry(work).State = EntityState.Detached;
        }
    }
}
