using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.DTO;
using WorkTracker.Model;

namespace WorkTracker.Utilities
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<WorkDTO, Work>().ReverseMap();
        }
    }
}
