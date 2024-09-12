using AutoMapper;
using SittyCia.Core.Dto;
using SittyCia.Core.Models;


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
