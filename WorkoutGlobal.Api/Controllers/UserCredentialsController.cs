﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkoutGlobal.Api.Contracts.RepositoryManagerContracts;
using WorkoutGlobal.Api.Filters.ActionFilters;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.DTOs.UserCredentialDTOs;
using WorkoutGlobal.Api.Models.ErrorModels;

namespace WorkoutGlobal.Api.Controllers
{
    [Route("api/userCredentials")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserCredentialsController(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet("{userCredentialId}")]
        public async Task<IActionResult> GetUserCredential(string userCredentialId)
        {
            var userCredential = await _repositoryManager.UserCredentialRepository.GetUserCredentialsAsync(userCredentialId);

            if (userCredential == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var userCredentialDto = _mapper.Map<UserCredentialDto>(userCredential);

            return Ok(userCredentialDto);
        }

        [HttpPut("{userCredentialId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateUserCredential(string userCredentialId, [FromBody] UserCredentialDto userCredentialDto)
        {
            var userCredential = await _repositoryManager.UserCredentialRepository.GetUserCredentialsAsync(userCredentialId);

            if (userCredential == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var updateUserCredential = _mapper.Map<UserCredentials>(userCredentialDto);

            await _repositoryManager.UserCredentialRepository.UpdateUserCredentialsAsync(updateUserCredential);

            return NoContent();
        }

        [HttpDelete("{userCredentialId}")]
        public async Task<IActionResult> DeleteUserCredential(string userCredentialId)
        {
            var userCredential = await _repositoryManager.UserCredentialRepository.GetUserCredentialsAsync(userCredentialId);

            if (userCredential == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            await _repositoryManager.UserCredentialRepository.DeleteUserCredentialsAsync(userCredential);

            return NoContent();
        }

        [HttpGet("{userCredentialId}/role")]
        public async Task<IActionResult> GetUserCredentialRole(string userCredentialId)
        {
            var userCredential = await _repositoryManager.UserCredentialRepository.GetUserCredentialsAsync(userCredentialId);

            if (userCredential == null)
                return BadRequest(new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "There is no category with such id.",
                    Details = new StackTrace().ToString()
                });

            var role = _repositoryManager.UserCredentialRepository.GetUserCredentialsRole(userCredentialId);

            return Ok(role);
        }

    }
}
