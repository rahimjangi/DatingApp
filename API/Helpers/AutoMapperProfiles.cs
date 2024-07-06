using API.Dtos;
using API.Enteties;
using API.Extentions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{

    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
        .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.CalculateAge()))
        .ForMember(d => d.PhotoUrl, o => o.MapFrom(m => m.Photos.FirstOrDefault(x => x.IsMain)!.Url))
        .ReverseMap();
        CreateMap<Photo, PhotoDto>().ReverseMap();
    }
}
