using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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
                },            };

            //map domain models to DTO
            var regionsDto = new List<RegionDto>();

            foreach (var region in regions)
            {
                regionsDto.Add(
                    new RegionDto()
                    {
                        Id = region.Id,
                        Name = region.Name,
                        Code = region.Code,
                        RegionImageUrl = region.RegionImageUrl
                    }
                );
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //get regions from database by guid
            var regionDomain = _nzWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);

        }
    }
}
