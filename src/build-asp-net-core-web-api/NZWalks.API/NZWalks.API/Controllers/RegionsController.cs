using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        public RegionsController()
        {

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "North Island"
                },
                new Region()
                {
                    Id =  Guid.NewGuid(),
                    Name = "South Island"
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Canterbury"
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Tasman"
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Marlborough"
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland"
                },
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Name = "Waikato"
                },
            };

            return Ok(regions);
        }
    }
}