using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        #region Get All Walks

        [HttpGet]
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalk(Models.DTO.Requests.Walks.AddWalkRequest addWalkRequest)
        {
            //Validate the Request
            if (!await ValidatAddWalkAsync(addWalkRequest))
            {
                return BadRequest(ModelState);
            }

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
        [Authorize(Roles = "writer")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] Models.DTO.Requests.Walks.UpdateWalkRequest updateWalkRequest)
        {
            //Validate the Request
            if (!await ValidatUpdateWalkAsync(updateWalkRequest))
            {
                return BadRequest(ModelState);
            }

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

        #region Private Methods 

        private async Task<bool> ValidatAddWalkAsync(Models.DTO.Requests.Walks.AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //        $"Add Walk Data is required.");

            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name),
            //        $"{nameof(addWalkRequest.Name)} cannot be null or empty or white space.");
            //}
            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //        $"{nameof(addWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                $"{nameof(addWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidatUpdateWalkAsync(Models.DTO.Requests.Walks.UpdateWalkRequest updateWalkRequest)
        {
            //if (updateWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest),
            //        $"Add Walk Data is required.");

            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Name),
            //        $"{nameof(updateWalkRequest.Name)} cannot be null or empty or white space.");
            //}
            //if (updateWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequest.Length),
            //        $"{nameof(updateWalkRequest.Length)} should be greater than zero.");
            //}

            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                $"{nameof(updateWalkRequest.RegionId)} is invalid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
