using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
       // CreateMap<ApplicationUser, UserDto>();
        CreateMap<Author,AuthorDto>();
        CreateMap<Departament, DepartamentDto>();
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dto => dto.Description,
                config => config.MapFrom(employee => employee.Departament.Description))
            .ForMember(dto=>dto.Address,config=>config.MapFrom(employee=>employee.ApplicationUser.Address))
            .ForMember(dto=>dto.Email,config=>config.MapFrom(employee=>employee.ApplicationUser.Email))
            .ForMember(dto=>dto.FullName,config=>config.MapFrom(employee=>employee.ApplicationUser.Fullname))
            .ForMember(dto=>dto.PhoneNumber,config=>config.MapFrom(employee=>employee.ApplicationUser.PhoneNumber))
            .ForMember(dto=>dto.UserName,config=>config.MapFrom(employee=>employee.ApplicationUser.UserName));

        CreateMap<Client, ClientdDto>()
            .ForMember(dto=>dto.Address,config=>config.MapFrom(client=>client.ApplicationUser.Address))
            .ForMember(dto=>dto.Email,config=>config.MapFrom(client=>client.ApplicationUser.Email))
            .ForMember(dto=>dto.FullName,config=>config.MapFrom(client=>client.ApplicationUser.Fullname))
            .ForMember(dto=>dto.PhoneNumber,config=>config.MapFrom(client=>client.ApplicationUser.PhoneNumber))
            .ForMember(dto=>dto.UserName,config=>config.MapFrom(client=>client.ApplicationUser.UserName));

    }
    
}
