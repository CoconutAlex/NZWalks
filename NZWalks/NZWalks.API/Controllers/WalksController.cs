using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        #region Get All Walks

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await walkRepository.GetAllAsync();

            var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walkDTO);
        }

        #endregion

        #region Get Walk by Id

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walk = await walkRepository.GetAsync(id);
            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkDTO);
        }

        #endregion

        #region Add Walk

        [HttpPost]
        public async Task<IActionResult> AddWalk(Models.DTO.Requests.Walks.AddWalkRequest addWalkRequest)
        {
            var request = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            var response = await walkRepository.AddAsync(request);

            var responseDTO = mapper.Map<Models.DTO.Walk>(response);

            return CreatedAtAction("GetWalkAsync", new { id = responseDTO.Id }, responseDTO);
        }

        #endregion

        #region Delete Walk

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }

            var deletedWalkDTO = mapper.Map<Models.DTO.Walk>(deletedWalk);

            return Ok(deletedWalkDTO);
        }
        #endregion

        #region Update Walk

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] Models.DTO.Requests.Walks.UpdateWalkRequest updateWalkRequest)
        {
            var request = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            var response = await walkRepository.UpdateAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var responseDTO = mapper.Map<Models.DTO.Walk>(response);

            return Ok(responseDTO);
        }

        #endregion
    }
}
