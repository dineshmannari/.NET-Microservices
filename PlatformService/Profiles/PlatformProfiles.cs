using AutoMapper;
using PlatformService.DTO;
using PlatformService.Models;
namespace PlatformService.Profiles{
    public class PlatformProfiles: Profile
    {
        public PlatformProfiles(){
            //servicing the reading scenario
            //Source to target
            //to change target you need to do explicity mapping
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }


    }
}