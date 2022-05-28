using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.CommentDTOs;
using WorkoutGlobal.Api.Models.DTOs.CommentsBlockDTOs;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/videoBlocks")]
    [ApiController]
    public class CommentsBlockController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CommentsBlockController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet("{commentsBlockId}")]
        public async Task<IActionResult> GetCommentsBlock(Guid commentsBlockId)
        {
            var commentsBlock = await _repositoryManager.CommentsBlockRepository.GetCommentsBlockAsync(commentsBlockId);

            if (commentsBlock == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no comment block with such id.",
                    Details = new StackTrace().ToString()
                });

            var commentsBlockDto = _mapper.Map<CommentsBlockDto>(commentsBlock);

            return Ok(commentsBlockDto);
        }

        [HttpGet("videos/{videoId}")]
        public async Task<IActionResult> GetCommentsBlockByVideoId(Guid videoId)
        {
            var video = await _repositoryManager.VideoRepository.GetVideoAsync(videoId);

            if (video == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no video with such id.",
                    Details = new StackTrace().ToString()
                });

            var commentsBlock = await _repositoryManager.VideoRepository.GetVideoCommentsBlockAsync(videoId);
                // await _repositoryManager.CommentsBlockRepository.GetCommentsBlockByVideoIdAsync(videoId);

            var commentsBlockDto = _mapper.Map<CommentsBlockDto>(commentsBlock);

            return Ok(commentsBlockDto);
        }

        [HttpGet("{commentsBlockId}/comments")]
        public async Task<IActionResult> GetBlockComments(Guid commentsBlockId)
        {
            var blockComments = await _repositoryManager.CommentsBlockRepository.GetCommentsBlockAsync(commentsBlockId);

            if (blockComments == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no comment block with such id.",
                    Details = new StackTrace().ToString()
                });

            var comments = await _repositoryManager.CommentsBlockRepository.GetCommentsBlockCommentsAsync(commentsBlockId);

            var commentsDto = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return Ok(commentsDto);
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateCommentsBlock([FromBody] CommentsBlockDto commentsBlockDto)
        {
            var creationCommentsBlock = _mapper.Map<CommentsBlock>(commentsBlockDto);

            await _repositoryManager.CommentsBlockRepository.CreateCommentBlockAsync(creationCommentsBlock);

            return Created($"api/commentsBlock/{creationCommentsBlock.Id}", creationCommentsBlock.Id);
        }
    }
}
