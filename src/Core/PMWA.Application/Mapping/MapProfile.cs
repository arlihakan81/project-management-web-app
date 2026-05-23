using AutoMapper;
using PMWA.Application.Dtos.Project;
using PMWA.Application.Dtos.Role;
using PMWA.Application.Dtos.User;
using PMWA.Domain.Entities;

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
        }

    }
}
