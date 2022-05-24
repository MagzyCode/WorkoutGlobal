using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Context;
using WorkoutGlobal.Api.Contracts.RepositoryContracts;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentsBlockDTOs;
using WorkoutGlobal.Api.Repositories.BaseRepositories;

namespace WorkoutGlobal.Api.Repositories.ModelsRepositories
{
    public class CommentsBlockRepository : BaseRepository<CommentsBlock>, ICommentsBlockRepository
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsBlockRepository(
            WorkoutGlobalContext workoutGlobalContext, 
            IConfiguration configurationManager,
            ICommentRepository commentRepository,
            IMapper mapper) 
            : base(workoutGlobalContext, configurationManager)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task CreateCommentBlockAsync(CommentsBlock commentsBlock)
            => await CreateAsync(commentsBlock);

        public async Task<IEnumerable<Comment>> GetBlockCommentsAsync(Guid commentBlockId)
        {
            var comments = await _commentRepository.GetBlockCommentsAsync(commentBlockId);

            return comments;
        }

        public async Task<CommentsBlock> GetCommentsBlockAsync(Guid commentBlockId)
        {
            var model = await GetModelAsync(commentBlockId);

            return model;
        }

        public async Task<CommentsBlock> GetCommentsBlockByVideoIdAsync(Guid videoId)
        {
            var model = await GetAll().Where(x => x.CommentedVideoId == videoId).FirstOrDefaultAsync();

            return model;
        }
    }
}
