using API.Entities;
using AutoMapper;

namespace API;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles(){
        CreateMap<AppUser, MemberDTO>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)); // auto mapper is able to match GetAge with Age in DTO // photo individual mapping
        CreateMap<Photo, PhotoDTO>();
    }

}
