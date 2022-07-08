using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkoutGlobal.Api.IntegrationTests.Configuration;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Dto;
using WorkoutGlobal.Api.Models.Enums;
using WorkoutGlobal.Api.Models.ErrorModels;
using Xunit;

namespace WorkoutGlobal.Api.IntegrationTests.Controllers
{
    public class UserCredentialsControllerIntegrationTests : IAsyncLifetime
    {
        private readonly HttpClient _httpClient = new();
        private readonly IConfiguration _testConfiguration;
        private readonly List<string> _purgeList = new();

        public UserCredentialsControllerIntegrationTests()
        {
            _testConfiguration = ConfigurationAccessor.GetTestConfiguration();
        }

        public async Task InitializeAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                _httpClient.BaseAddress = new Uri(_testConfiguration["BaseHost"]);
            });
        }

        public async Task DisposeAsync()
        {
            foreach (var id in _purgeList)
                _ = await _httpClient.DeleteAsync($"api/userCredentials/purge/{id}");

            _httpClient.Dispose();
            await Task.CompletedTask;
        }

        [Fact]
        public async Task GetAllUserCredentials_ModelsExisted_ReturnAllUsers()
        {
            // arrange
            var url = "api/userCredentials";

            // act
            var getAllUsersResponse = await _httpClient.GetAsync(url);
            var users = await getAllUsersResponse.Content.ReadFromJsonAsync<List<UserCredentialDto>>();

            // assert
            getAllUsersResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            users.Should().NotBeEmpty();
            users!.Count.Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GetUserCredential_ModelsExisted_ReturnExistedUser()
        {
            // arrange
            var user = new UserRegistrationDto()
            {
                UserName = "TestUser2",
                Email = "testUser2@mail.ru",
                Password = "testUserPassword2",
                PhoneNumber = "+375250000002",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1950, 1, 1),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 122",
                Sex = Sex.Male,
                Height = 186,
                Weight = 115,
                SportsActivity = SportsActivity.Moderate,
                ClassificationNumber = "RTD458S5890002"
            };
            var creationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", user);
            var userId = creationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");
            _purgeList.Add(userId);

            // act
            var getUserResponse = await _httpClient.GetAsync($"api/userCredentials/{userId}");
            var responseUser = await getUserResponse.Content.ReadFromJsonAsync<UserCredentials>();

            // assert
            getUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            responseUser.Should().NotBeNull();
            responseUser.Should().BeOfType<UserCredentials>();
            responseUser!.UserName.Should().Be(user.UserName);
            responseUser!.Email.Should().Be(user.Email);
            responseUser!.PhoneNumber.Should().Be(user.PhoneNumber);
        }

        [Fact]
        public async Task GetUserCredential_ModelsNotExisted_ReturnNotFoundStatus()
        {
            // arrange
            var invalidUserId = Guid.Empty;

            // act
            var getUserResponse = await _httpClient.GetAsync($"api/userCredentials/{invalidUserId}");
            var error = await getUserResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            getUserResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            error!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            error!.Message.Should().Be("User don't exists.");
        }

        [Fact]
        public async Task UpdateUserCredential_ModelExisted_ReturnNoContentStatus()
        {
            // arrange
            var registrateUser = new UserRegistrationDto()
            {
                UserName = "TestUser3",
                Email = "testUser3@mail.ru",
                Password = "testUserPassword3",
                PhoneNumber = "+375250000003",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1950, 1, 1),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 123",
                Sex = Sex.Female,
                Height = 186,
                Weight = 115,
                SportsActivity = SportsActivity.Moderate,
                ClassificationNumber = "RTD458S5890003"
            };
            var creationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", registrateUser);
            var userId = creationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");
            var updationModel = new UpdationUserCredentialsDto()
            {
                Id = userId,
                UserName = "ChangeValue",
                Email = "testUser3@mail.ru",
                Password = "testUserPassword3",
                PhoneNumber = "+375250000003"
            };
            _purgeList.Add(userId);

            // act
            var updateResponse = await _httpClient.PutAsJsonAsync($"api/userCredentials/{userId}", updationModel);

            var createdUserResponse = await _httpClient.GetAsync($"api/userCredentials/{userId}");
            var createdUser = await createdUserResponse.Content.ReadFromJsonAsync<UserCredentials>();

            // assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            createdUser!.UserName.Should().Be("ChangeValue");
            createdUser!.Email.Should().Be(registrateUser.Email);
            createdUser!.PhoneNumber.Should().Be(registrateUser.PhoneNumber);
        }

        [Fact]
        public async Task UpdateUserCredential_ModelNotExisted_ReturnNotFoundStatus()
        {
            // arrange
            var invalidUpdationModel = new UpdationUserCredentialsDto()
            {
                Id = Guid.Empty.ToString(),
                UserName = "ChangeValue",
                Email = "testUser3@mail.ru",
                Password = "testUserPassword3",
                PhoneNumber = "+375250000003"
            };

            // act
            var updateResponse = await _httpClient.PutAsJsonAsync($"api/userCredentials/{invalidUpdationModel.Id}", invalidUpdationModel);
            var error = await updateResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            error!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            error!.Message.Should().Be("User don't exists.");

        }

        [Fact]
        public async Task DeleteUserCredential_ModelExisted_ReturnNoContentStatus()
        {
            // arrange
            var registrateUser = new UserRegistrationDto()
            {
                UserName = "TestUser4",
                Email = "testUser4@mail.ru",
                Password = "testUserPassword4",
                PhoneNumber = "+375250000004",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1950, 1, 1),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 124",
                Sex = Sex.Female,
                Height = 186,
                Weight = 115,
                SportsActivity = SportsActivity.Moderate,
                ClassificationNumber = "RTD458S5890004"
            };
            var creationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", registrateUser);
            var userId = creationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");
            _purgeList.Add(userId);

            // act
            var deleteResponse = await _httpClient.DeleteAsync($"api/userCredentials/{userId}");
            var getUserResponce = await _httpClient.GetAsync($"api/userCredentials/{userId}");
            var getUserContent = await getUserResponce.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            getUserResponce.StatusCode.Should().Be(HttpStatusCode.NotFound);

            getUserContent.Should().NotBeNull();
            getUserContent.Should().BeOfType<ErrorDetails>();
            getUserContent!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            getUserContent!.Message.Should().Be("User don't exists.");
        }

        [Fact]
        public async Task DeleteUserCredential_ModelNotExisted_ReturnNotFoundStatus()
        {
            // arrange
            var invalidUserId = Guid.Empty;

            // act
            var deleteResponse = await _httpClient.DeleteAsync($"api/userCredentials/{invalidUserId}");
            var deleteContent = await deleteResponse.Content.ReadFromJsonAsync<ErrorDetails>();
            var getResponse = await _httpClient.GetAsync($"api/userCredentials/{invalidUserId}");

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
            
            deleteContent!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            deleteContent!.Message.Should().Be("User don't exists.");

            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetUserCredentialRole_ModelExisted_ReturnRole()
        {
            // arrange
            var registrateUser = new UserRegistrationDto()
            {
                UserName = "TestUser5",
                Email = "testUser5@mail.ru",
                Password = "testUserPassword5",
                PhoneNumber = "+375250000005",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1950, 1, 1),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 125",
                Sex = Sex.Male,
                Height = 186,
                Weight = 115,
                SportsActivity = SportsActivity.Moderate,
                ClassificationNumber = "RTD458S5890005"
            };
            var creationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", registrateUser);
            var userId = creationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");
            _purgeList.Add(userId);

            // act
            var getRoleResponse = await _httpClient.GetAsync($"api/userCredentials/{userId}/role");
            var getRoleContent = await getRoleResponse.Content.ReadAsStringAsync();

            // assert
            getRoleResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            getRoleContent!.Should().NotBeEmpty("User");
            getRoleContent!.Should().Be("User");
        }

        public async Task GetUserCredentialRole_UserRole_ReturnNoContentStatus()
        {
            // arrange
            var invalidUserId = Guid.Empty;

            // act
            var getRoleResponse = await _httpClient.GetAsync($"api/userCredentials/{invalidUserId}/role");
            var getRoleContent = await getRoleResponse.Content.ReadFromJsonAsync<ErrorDetails>();

            // assert
            getRoleResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            getRoleContent!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            getRoleContent!.Message.Should().Be("User don't exists.");
        }

        public async Task GetUserCredentialRole_TrainerRole_ReturnNoContentStatus()
        {
            // arrange
            var registrateUser = new UserRegistrationDto()
            {
                UserName = "TestUser5",
                Email = "testUser5@mail.ru",
                Password = "testUserPassword5",
                PhoneNumber = "+375250000005",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1950, 1, 1),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 125",
                Sex = Sex.Male,
                Height = 186,
                Weight = 115,
                SportsActivity = SportsActivity.Moderate,
                ClassificationNumber = "RTD458S5890005"
            };
            var creationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", registrateUser);
            var userId = creationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");
            _purgeList.Add(userId);

            // act
            var beforeRaisingResponse = await _httpClient.GetAsync($"api/userCredentials/{userId}/role");
            var beforeRaisingContent = await beforeRaisingResponse.Content.ReadAsStringAsync();
            var raisingResponse = await _httpClient.PutAsync($"api/userCredentials/{userId}/raising", null);
            var afterRaisingResponse = await _httpClient.GetAsync($"api/userCredentials/{userId}/role");
            var afterRaisingContent = await afterRaisingResponse.Content.ReadAsStringAsync();

            // assert
            beforeRaisingContent!.Should().BeEquivalentTo("User");

            raisingResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            afterRaisingContent.Should().BeEquivalentTo("Trainer");
        }

    }
}
