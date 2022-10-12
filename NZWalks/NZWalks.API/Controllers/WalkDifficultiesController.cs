using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        #region Get All Walk Difficulties

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficulties = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultiesDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties);
            return Ok(walkDifficultiesDTO);
        }

        #endregion

        #region Get Walk Difficulty by Id

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }
        #endregion

        #region Add Walk Difficulty

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDifficulty(Models.DTO.Requests.WalkDifficulties.AddWalkDifficultyRequest walkDifficultyRequest)
        {
            //Validate Request
            //if (!ValidatAddWalkDifficultyAsync(walkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var request = new Models.Domain.WalkDifficulty()
            {
                Code = walkDifficultyRequest.Code
            };

            var response = await walkDifficultyRepository.AddAsync(request);

            var responseDTO = mapper.Map<Models.DTO.WalkDifficulty>(response);

            return CreatedAtAction("GetWalkDifficultyAsync", new { id = responseDTO.Id }, responseDTO);
        }

        #endregion

        #region Delete Walk Difficulty

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var deletedWalkDifficulty = await walkDifficultyRepository.DeleteAsync(id);
            if (deletedWalkDifficulty == null)
            {
                return NotFound();
            }

            var deletedWalkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(deletedWalkDifficulty);

            return Ok(deletedWalkDifficultyDTO);
        }

        #endregion

        #region Update Walk Difficulty

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] Models.DTO.Requests.WalkDifficulties.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //Validate Request
            //if (!ValidatUpdateWalkDifficultyAsync(updateWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var request = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };

            var response = await walkDifficultyRepository.UpdateAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }

            var responseDTO = mapper.Map<Models.DTO.WalkDifficulty>(response);

            return Ok(responseDTO);
        }

        #endregion

        #region Private Methods 

        private bool ValidatAddWalkDifficultyAsync(Models.DTO.Requests.WalkDifficulties.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"Add Walk Difficulty Data is required.");

                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidatUpdateWalkDifficultyAsync(Models.DTO.Requests.WalkDifficulties.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    $"Add Walk Difficulty Data is required.");

                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code),
                    $"{nameof(updateWalkDifficultyRequest.Code)} cannot be null or empty or white space.");
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
