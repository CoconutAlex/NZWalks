﻿namespace NZWalks.API.Models.DTO.Requests.Walks
{
    public class UpdateWalkRequest
    {
        public string? Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
