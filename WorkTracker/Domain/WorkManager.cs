using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.DAL;
using WorkTracker.DTO;
using WorkTracker.Model;

namespace WorkTracker.Domain
{
    public sealed class WorkManager
    {
        private IMapper _mapper;
        private IWorkRepository _workRepo;
        public WorkManager(IMapper mapper, IWorkRepository workRepository)
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
