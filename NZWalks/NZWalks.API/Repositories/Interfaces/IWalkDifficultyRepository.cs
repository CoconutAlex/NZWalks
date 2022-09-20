using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty?> GetAsync(Guid id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty qalkDifficulty);
        Task<WalkDifficulty?> DeleteAsync(Guid id);
        Task<WalkDifficulty?> UpdateAsync(Guid id, WalkDifficulty walkDifficulty);
    }
}
