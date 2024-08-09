using AutoMapper;
using WorkTracker.Data.Model;
using WorkTracker.DTO;

namespace WorkTracker.BusinessLogic
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<WorkDTO, Work>().ReverseMap();

            CreateMap<WorkDetailsDTO, WorkDetails>().ReverseMap();

            CreateMap<ClientDTO, Client>().ReverseMap();
        }
    }
}
