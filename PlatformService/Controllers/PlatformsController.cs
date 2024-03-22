using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTO;
using PlatformService.Models;
using PlatformService.SyncDataService.HTTP;

namespace PlatformService.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController: ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient commandDataClient)
        {   
            _repo= repo;
            _commandDataClient= commandDataClient;
            _mapper=mapper;
        }    

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("-->Getting Platforms");

            var platformItem= _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name ="GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem= _repo.GetPlatformById(id);
            if(platformItem!=null){
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();
            
        }
        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platFormModel= _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platFormModel);
            _repo.SaveChanges();
            var platformReadDto= _mapper.Map<PlatformReadDto>(platFormModel);
            try{
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously:{ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformById), new {Id= platformReadDto.Id}, platformReadDto);
        }
    }
}