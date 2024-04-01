using AutoMapper;
using WorkTracker.Data.DAL;
using WorkTracker.Data.Model;
using WorkTracker.DTO;

namespace WorkTracker.BusinessLogic
{
    public sealed class WorkBusinessLogic
    {
        private IMapper _mapper;
        private IWorkRepository _workRepo;
        public WorkBusinessLogic(IMapper mapper, IWorkRepository workRepository)
        {
            _mapper = mapper;
            _workRepo = workRepository;
        }
        public async Task AddWork(WorkDTO work) => await _workRepo.AddWork(MapToModel(work));

        public async Task UpdateWork(WorkDTO work) => await _workRepo.UpdateWork(MapToModel(work));

        public async Task DeleteWork(int id) => await _workRepo.DeleteWork(id);

        public async Task<ICollection<WorkDTO>> GetWorks(DateTime from, DateTime to)
        {
            var modelWorks = await _workRepo.GetWorks(from, to);
            var dtoWorks = _mapper.Map<ICollection<WorkDTO>>(modelWorks);
            return dtoWorks;
        }

        private Work MapToModel(WorkDTO work)
        {
            return _mapper.Map<Work>(work);
        }
    }
}
