using Microsoft.EntityFrameworkCore;
using WorkTracker.Data.Model;

namespace WorkTracker.Data.DAL
{
    public sealed class WorkRepository(WorkContext db) : IWorkRepository
    {
        private readonly WorkContext _db = db;

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

        public async Task<Work?> GetWork(int id)
        {
            return await _db.Works
                .Include(x => x.WorkDetails)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
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
            var existingWork = await _db.Works
               .Include(x => x.WorkDetails)
               .SingleOrDefaultAsync(x => x.Id == work.Id);

            if (existingWork is null)
            {
                return;
            }

            // Update the properties of the existing entity
            _db.Entry(existingWork).CurrentValues.SetValues(work);

            // Update the related entities
            foreach (var detail in work.WorkDetails)
            {
                var existingDetail = existingWork.WorkDetails.SingleOrDefault(d => d.Id == detail.Id);
                if (existingDetail != null)
                {
                    _db.Entry(existingDetail).CurrentValues.SetValues(detail);
                }
                else
                {
                    existingWork.WorkDetails.Add(detail);
                }
            }

            // Remove related entities that are no longer present
            foreach (var existingDetail in existingWork.WorkDetails.ToList())
            {
                if (!work.WorkDetails.Any(d => d.Id == existingDetail.Id))
                {
                    _db.WorksDetails.Remove(existingDetail);
                }
            }

            await _db.SaveChangesAsync();

            _db.Entry(existingWork).State = EntityState.Detached;
        }
    }
}
