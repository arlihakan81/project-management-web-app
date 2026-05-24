using AutoMapper;
using PMWA.Application.Dtos.Board;
using PMWA.Application.Dtos.Column;
using PMWA.Application.Dtos.Project;
using PMWA.Application.Dtos.Role;
using PMWA.Application.Dtos.Task;
using PMWA.Application.Dtos.User;
using PMWA.Domain.Entities;
using System.Xml.Serialization;

namespace PMWA.Application.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Project, ProjectDto>().ForMember(des => des.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt));
            CreateMap<CreateProjectDto, Project>();

            CreateMap<User, UserDto>().ForMember(des => des.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<Role, RoleDto>();

            CreateMap<TaskItem, TaskDto>()
                .ForMember(des => des.LastModifiedDate, opt => opt.MapFrom(src => src.ModifiedAt))
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(des => des.Assignee, opt => opt.MapFrom(src => src.User))
                .ForMember(des => des.Column, opt => opt.MapFrom(src => src.Column));
            CreateMap<CreateTaskDto, TaskItem>();

            CreateMap<Column, ColumnDto>().ForMember(des => des.Board, opt => opt.MapFrom(src => src.Board));

            CreateMap<Board, BoardDto>().ForMember(des => des.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(des => des.Columns, opt => opt.MapFrom(src => src.Columns)); 
        }

    }
}
