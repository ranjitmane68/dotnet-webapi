using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext _nzWalksDbContext;

        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            this._nzWalksDbContext = nZWalksDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var guid = Guid.NewGuid();
            //get regions from database
            var regions = new List<Region>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "North Island"
                },
                new()
                {
                    Id =  Guid.NewGuid(),
                    Name = "South Island"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Canterbury"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Tasman"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Marlborough"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Waikato"
                },
            };
            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //get regions from database by guid
            var region = _nzWalksDbContext.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }


            return Ok(region);

        }
    }
}
