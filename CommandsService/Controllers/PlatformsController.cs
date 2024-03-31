using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repo=repository;
            _mapper=mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine("--> Getting platforms from command service");
            var platformItem= _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {    
            Console.WriteLine("--> Inbound POST #Command Service");
            return Ok("Inbound test of From Platfirms COntroller ");
        }

    }
}