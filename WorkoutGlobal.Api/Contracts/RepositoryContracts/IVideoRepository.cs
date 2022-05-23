﻿using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.RequestParameters;

namespace WorkoutGlobal.Api.Contracts.RepositoryContracts
{
    /// <summary>
    /// Base structure for video repository.
    /// </summary>
    public interface IVideoRepository
    {
        public int Count { get; } 

        public Task<IEnumerable<Video>> GetAllVideosAsync(bool isPublic = true);

        public Task<Video> GetVideoAsync(Guid id);

        public Task<IEnumerable<Video>> GetPageVideosAsync(VideoParameters parameters, bool isPublic);

        public Task AddVideoAsync(Video video);
    }
}
