using AutoMapper;
using SittyCia.Models;
using SittyCia.Models.Dto;

namespace SittyCia.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskEntity, TaskDto>().ReverseMap();
            CreateMap<TaskEntity, CreateTaskDto>().ReverseMap();
            CreateMap<TaskEntity, UpdateTaskDto>().ReverseMap();
        }
    }

}
