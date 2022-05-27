using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentDTOs;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly IMapper _mapper;

        public CommentRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            IMapper mapper)
            : base(workoutGlobalContext, configurationManager)
        {
            _mapper = mapper;
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            await CreateAsync(comment);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentsBlockId)
        {
            var comments = await GetAll().Where(comment => comment.CommentsBlockId == commentsBlockId).ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetCreatorCommentsAsync(Guid creatorId)
        {
            var comments = await GetAll().Where(model => model.CommentatorId == creatorId).ToListAsync();

            return comments;
        }
    }
}
