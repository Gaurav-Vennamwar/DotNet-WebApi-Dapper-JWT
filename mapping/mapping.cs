using AutoMapper;
using DotnetAPI.Dtos;
using DotnetAPI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegisterationDto, UserComplete>();
    }
}
