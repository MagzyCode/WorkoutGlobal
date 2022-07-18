using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.ErrorModels;
using Xunit;

namespace WorkoutGlobal.Api.IntegrationTests.Controllers
{
    public class VideoControllerIntegrationTests : IAsyncLifetime
    {
        private readonly AppTestConnection<Guid> _appTestConnection;
        private Guid _createdCategotyId;
        private Guid _createdUserAccountId;
        private string _createdUserCredentialsId;

        public VideoControllerIntegrationTests() => _appTestConnection = new();

        public async Task InitializeAsync()
        {
            var response = await _appTestConnection.AppClient.PostAsJsonAsync("api/categories",
                new CreationCategoryDto()
                {
                    CategoryName = "TestCategory",
                    CategoryDescription = "TestDescription"
                });
            _createdCategotyId = await response.Content.ReadFromJsonAsync<Guid>();

            var user = new UserRegistrationDto()
            {
                UserName = "TestUser999",
                Email = "testUser@mail.ru",
                Password = "testUserPassword",
                PhoneNumber = "+375250000000",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1978, 5, 2),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 120",
                Sex = Models.Enums.Sex.Male,
                Height = 178,
                Weight = 95,
                SportsActivity = Models.Enums.SportsActivity.Reduced,
                ClassificationNumber = "RTD458S5891238"
            };
            var registrationUserResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/authentication/registration", user);
            _createdUserCredentialsId = await registrationUserResponse.Content.ReadFromJsonAsync<string>();
            await _appTestConnection.AppClient.PutAsync($"api/userCredentials/{_createdUserCredentialsId}/raising", null);

            response = await _appTestConnection.AppClient.GetAsync($"api/accounts/account/{user.UserName}");
            var account = await response.Content.ReadFromJsonAsync<UserDto>();
            _createdUserAccountId = account.Id;
        }

        public async Task DisposeAsync()
        {
            foreach (var id in _appTestConnection.PurgeList)
                await _appTestConnection.AppClient.DeleteAsync($"api/videos/purge/{id}");

            await _appTestConnection.AppClient.DeleteAsync($"api/categories/{_createdCategotyId}");
            await _appTestConnection.AppClient.DeleteAsync($"api/userCredentials/{_createdUserCredentialsId}");

            _appTestConnection.AppClient.Dispose();
            await Task.CompletedTask;
        }

        [Fact]
        public async Task GetAllVideos_ValidConnection_ReturnAllVideos()
        {
            // arrange
            var creationVideo = new CreationVideoDto()
            {
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "First video on YouTube",
                Description = "That is first video made on YouTube",
                IsPublic = true,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var createVideoId = await createVideoResponse.Content.ReadFromJsonAsync<Guid>();
            _appTestConnection.PurgeList.Add(createVideoId);

            // act
            var getAllResponse = await _appTestConnection.AppClient.GetAsync("api/videos");
            var getAllContent = await getAllResponse.Content.ReadFromJsonAsync<List<VideoDto>>();

            // assert
            getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            getAllContent.Should().NotBeEmpty();
            getAllContent.Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GetVideo_ModelExisted_ReturnModel()
        {
            // arrange 
            var creationVideo = new CreationVideoDto()
            {
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "First video on YouTube",
                Description = "That is first video made on YouTube",
                IsPublic = true,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var createVideoId = await createVideoResponse.Content.ReadFromJsonAsync<Guid>();
            _appTestConnection.PurgeList.Add(createVideoId);

            // act
            var getResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{createVideoId}");
            var getContent = await getResponse.Content.ReadFromJsonAsync<VideoDto>();

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            getContent.Should().NotBeNull();
            getContent.Should().BeEquivalentTo(creationVideo);
        }

        [Fact]
        public async Task GetVideo_ModelNotExisted_ReturnNotFoundStatus()
        {
            // arrange 
            var invalidVideoId = Guid.Empty;

            // act
            var getResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{invalidVideoId}");
            var getContent = await getResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            getContent.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            getContent.Message.Should().Be("There is no video with such id");
        }

        [Fact]
        public async Task CreateVideo_ValidModel_ReturnCreatedId()
        {
            // arrange
            var creationVideo = new CreationVideoDto()
            {
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "First video on YouTube",
                Description = "That is first video made on YouTube",
                IsPublic = true,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };

            // act
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var createVideoId = await createVideoResponse.Content.ReadFromJsonAsync<Guid>();
            _appTestConnection.PurgeList.Add(createVideoId);
            var getResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{createVideoId}");
            var getContent = await getResponse.Content.ReadFromJsonAsync<VideoDto>();

            // assert
            createVideoResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            createVideoId.ToString().Should().MatchRegex(@"[A-Za-z0-9]{8}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{12}");

            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            getContent.Should().BeEquivalentTo(creationVideo);
        }

        [Fact]
        public async Task CreateVideo_InvalidModel_ReturnBadRequestStatus()
        {
            // arrange
            var creationVideo = new CreationVideoDto()
            {
                Link = null,
                Title = null,
                Description = null,
                IsPublic = true,
                CategoryId = Guid.Empty,
                UserId = Guid.Empty
            };

            // act
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var error = await createVideoResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            createVideoResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            error.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            error.Message.Should().Be("Dto model isn't valid.");
        }

        [Fact]
        public async Task UpdateVideo_ModelExisted_ReturnNoContentStatus()
        {
            // arrange 
            var creationVideo = new CreationVideoDto()
            {
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "First video on YouTube",
                Description = "That is first video made on YouTube",
                IsPublic = true,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var createdVideoId = await createVideoResponse.Content.ReadFromJsonAsync<Guid>();
            var updationUser = new VideoDto()
            {
                Id = createdVideoId,
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "Test",
                Description = "That is first video made on YouTube",
                IsPublic = false,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };
            _appTestConnection.PurgeList.Add(createdVideoId);

            // act
            var updateResponse = await _appTestConnection.AppClient.PutAsJsonAsync($"api/videos/{createdVideoId}", updationUser);
            var getResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{createdVideoId}");
            var getContent = await getResponse.Content.ReadFromJsonAsync<VideoDto>();

            // assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            updationUser.Should().BeEquivalentTo(getContent);
        }

        [Fact]
        public async Task UpdateVideo_ModelNotExisted_ReturnNotFoundStatus()
        {
            // arrange 
            var updationUser = new VideoDto()
            {
                Id = Guid.Empty,
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "Test",
                Description = "That is first video made on YouTube",
                IsPublic = false,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };

            // act
            var updateResponse = await _appTestConnection.AppClient.PutAsJsonAsync($"api/videos/{updationUser.Id}", updationUser);
            var updateContent = await updateResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            updateContent.Should().BeOfType<ErrorDetails>();
            updateContent.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            updateContent.Message.Should().Be("There is no video with such id");
        }

        [Fact]
        public async Task DeleteVideo_ModelExisted_ReturnNoContentStatus()
        {
            // arrange
            var creationVideo = new CreationVideoDto()
            {
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "First video on YouTube",
                Description = "That is first video made on YouTube",
                IsPublic = true,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var createdVideoId = await createVideoResponse.Content.ReadFromJsonAsync<Guid>();
            _appTestConnection.PurgeList.Add(createdVideoId);

            // act
            var deleteResponse = await _appTestConnection.AppClient.DeleteAsync($"api/videos/{createdVideoId}");
            var getResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{createdVideoId}");
            var getContent = await getResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            getContent.Should().BeOfType<ErrorDetails>();
            getContent.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            getContent.Message.Should().Be("There is no video with such id");
        }

        [Fact]
        public async Task DeleteVideo_ModelNotExisted_ReturnNotFoundStatus()
        {
            // arrange 
            var invalidVideoId = Guid.Empty;

            // act
            var deleteResponse = await _appTestConnection.AppClient.DeleteAsync($"api/videos/{invalidVideoId}");
            var deleteContent = await deleteResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            deleteContent.Should().BeOfType<ErrorDetails>();
            deleteContent.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            deleteContent.Message.Should().Be("There is no video with such id");
        }

        [Fact]
        public async Task GetVideoCommentsBlock_ModelExisted_ReturnModel()
        {
            // arrange
            var creationVideo = new CreationVideoDto()
            {
                Link = @"https://youtu.be/jNQXAC9IVRw",
                Title = "First video on YouTube",
                Description = "That is first video made on YouTube",
                IsPublic = true,
                CategoryId = _createdCategotyId,
                UserId = _createdUserAccountId,
            };
            var createVideoResponse = await _appTestConnection.AppClient.PostAsJsonAsync("api/videos", creationVideo);
            var createdVideoId = await createVideoResponse.Content.ReadFromJsonAsync<Guid>();
            _appTestConnection.PurgeList.Add(createdVideoId);

            // act
            var getBlockResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{createdVideoId}/commentsBlock");
            var getBlockContent = await getBlockResponse.Content.ReadFromJsonAsync<CommentsBlockDto>();

            var getCommentBlockResponse = await _appTestConnection.AppClient.GetAsync($"api/commentsBlocks/{getBlockContent.Id}");
            var getCommentBlockContent = await getCommentBlockResponse.Content.ReadFromJsonAsync<CommentsBlockDto>();

            // assert
            getBlockResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            getBlockContent.Should().NotBeNull();
            getBlockContent.Should().BeEquivalentTo(getCommentBlockContent);
        }

        [Fact]
        public async Task GetVideoCommentsBlock_ModelNotExisted_ReturnNotFoundStatus()
        {
            // arrange 
            var invalidVideoId = Guid.Empty;

            // act
            var getBlockResponse = await _appTestConnection.AppClient.GetAsync($"api/videos/{invalidVideoId}/commentsBlock");
            var getBlockContent = await getBlockResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            getBlockResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            getBlockContent.Should().BeOfType<ErrorDetails>();
            getBlockContent.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            getBlockContent.Message.Should().Be("There is no video with such id");
        }
    }
}
