using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PostController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> CreatePost([FromBody] PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);

            await _repositoryManager.PostRepository.CreatePostAsync(post);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{postId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdatePost(Guid postId, [FromBody] PostDto postDto)
        {
            var post = await _repositoryManager.PostRepository.GetPostAsync(postId);

            if (post == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no post with such id.",
                    Details = new StackTrace().ToString()
                });

            var updatedPost = _mapper.Map<Post>(postDto);

            await _repositoryManager.PostRepository.UpdatePostAsync(updatedPost);

            return NoContent();
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var post = await _repositoryManager.PostRepository.GetPostAsync(postId);

            if (post == null)
                return NotFound(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "There is no post with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.PostRepository.DeletePostAsync(post);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var models = await _repositoryManager.PostRepository.GetAllPostsAsync();

            var modelsDto = _mapper.Map<IEnumerable<PostDto>>(models);

            return Ok(modelsDto);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var post = await _repositoryManager.PostRepository.GetPostAsync(postId);

            var postDto = _mapper.Map<PostDto>(post);

            return Ok(postDto);
        }
    }
}
