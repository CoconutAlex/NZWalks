using AutoMapper;
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
        public async Task<IActionResult> AddWalkDifficulty(Models.DTO.Requests.WalkDifficulties.AddWalkDifficultyRequest walkDifficultyRequest)
        {
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
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id, [FromBody] Models.DTO.Requests.WalkDifficulties.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
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
    }
}
