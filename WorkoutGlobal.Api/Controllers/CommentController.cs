using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CommentController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreateComment([FromBody] CreationCommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            await _repositoryManager.CommentRepository.CreateCommentAsync(comment);

            return Ok(StatusCodes.Status201Created);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetComment(Guid commentId)
        {
            var comment = await _repositoryManager.CommentRepository.GetCommentAsync(commentId);

            if (comment == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no comment with such id.",
                    Details = new StackTrace().ToString()
                });

            var commentDto = _mapper.Map<CommentDto>(comment);

            return Ok(commentDto);
        }

    }
}
