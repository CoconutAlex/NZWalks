using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        #region Get All Regions

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();

            //Without Auto Mapper

            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };

            //    regionsDTO.Add(regionDTO);
            //});

            //With AutoMapper

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        #endregion

        #region Get Region by Id

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsyc")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        #endregion

        #region Add Region

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.Requests.Regions.AddRegionRequest addRegionRequest)
        {
            //Request to Domain Model
            var request = new Models.Domain.Region
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //Pass details to Repository
            var response = await regionRepository.AddAsync(request);

            //Convert to DTO
            var responseDTO = mapper.Map<Models.DTO.Region>(response);

            return CreatedAtAction("GetRegionAsyc", new { id = responseDTO.Id }, responseDTO);
        }

        #endregion

        #region Delete Region

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id);
            if (deletedRegion == null)
            {
                return NotFound();
            }

            var deletedRegionDTO = mapper.Map<Models.DTO.Region>(deletedRegion);

            return Ok(deletedRegionDTO);
        }

        #endregion

        #region Update Region

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.Requests.Regions.UpdateRegionRequest updateRegionRequest)
        {
            var request = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            var response = await regionRepository.UpdateAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var responseDTO = mapper.Map<Models.DTO.Region>(response);

            return Ok(responseDTO);
        }

        #endregion
    }
}
