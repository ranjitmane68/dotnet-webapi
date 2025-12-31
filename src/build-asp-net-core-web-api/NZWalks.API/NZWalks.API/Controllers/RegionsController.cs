using Microsoft.AspNetCore.Mvc;
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
            var regionDomainModels = _nzWalksDbContext.Regions.ToList();

            //map domain models to DTO
            var regionsDto = new List<RegionDto>();

            foreach (var region in regionDomainModels)
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

        [HttpPost]
        public IActionResult Create([FromBody] RegionDto addRegionDto)
        {
            //map or convert DTO to domain model
            var regionDomainModel = new Region
            {
                Name = addRegionDto.Name,
                Code = addRegionDto.Code,
                RegionImageUrl = addRegionDto.RegionImageUrl
            };

            //use domain model to create region
            _nzWalksDbContext.Regions.Add(regionDomainModel);
            _nzWalksDbContext.SaveChanges();

            //map domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionModel = _nzWalksDbContext.Regions.FirstOrDefault(region => region.Id == id);
            if (regionModel == null)
            {
                return NotFound();
            }

            regionModel.Name = updateRegionDto.Name;
            regionModel.Code = updateRegionDto.Code;
            regionModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            //it tracks changes and saves changes to DB
            _nzWalksDbContext.SaveChanges();

            //convert domain model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionModelToDelete = _nzWalksDbContext.Regions.FirstOrDefault(region => region.Id == id);
            if (regionModelToDelete == null)
            {
                return NotFound();
            }
            _nzWalksDbContext.Regions.Remove(regionModelToDelete);
            _nzWalksDbContext.SaveChanges();

            //return deleted region back
            //map region model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionModelToDelete.Id,
                Name = regionModelToDelete.Name,
                Code = regionModelToDelete.Code,
                RegionImageUrl = regionModelToDelete.RegionImageUrl

            };
            return Ok(regionDto);
        }
    }
}

