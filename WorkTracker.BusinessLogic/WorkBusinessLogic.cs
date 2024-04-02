using AutoMapper;
using WorkTracker.Data.DAL;
using WorkTracker.Data.Model;
using WorkTracker.DTO;

namespace WorkTracker.BusinessLogic
{
    public sealed class WorkBusinessLogic(IMapper mapper, IWorkRepository workRepository) : IWorkBusinessLogic
    {
        public async Task AddWork(WorkDTO work) => await workRepository.AddWork(MapToModel(work));

        public async Task<WorkDTO> GetWork(int id)
        {
            var work = await workRepository.GetWork(id);
            return mapper.Map<WorkDTO>(work);
        }

        public async Task UpdateWork(WorkDTO work) => await workRepository.UpdateWork(MapToModel(work));

        public async Task DeleteWork(int id) => await workRepository.DeleteWork(id);

        public async Task<ICollection<WorkDTO>> GetWorks(DateTime from, DateTime to)
        {
            var modelWorks = await workRepository.GetWorks(from, to);
            var dtoWorks = mapper.Map<ICollection<WorkDTO>>(modelWorks);
            return dtoWorks;
        }

        private Work MapToModel(WorkDTO work)
        {
            return mapper.Map<Work>(work);
        }
    }
}
