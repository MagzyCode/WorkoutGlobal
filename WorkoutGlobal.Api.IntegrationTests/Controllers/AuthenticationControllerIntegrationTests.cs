using FluentAssertions;
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
using Xunit;

namespace WorkoutGlobal.Api.IntegrationTests.Controllers
{
    public class AuthenticationControllerIntegrationTests : IAsyncLifetime
    {
        private readonly HttpClient _httpClient = new();
        private readonly IConfiguration _testConfiguration;
        private readonly List<string> _purgeList = new();

        public AuthenticationControllerIntegrationTests()
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
                _ = await _httpClient.DeleteAsync($"api/authentication/purge/{id}");
            
            _httpClient.Dispose();
            await Task.CompletedTask;
        }

        [Fact]
        public async Task Registrate_ValidUser_ReturnCreatedResult()
        {
            // arrange
            var userRegistrationDto = new UserRegistrationDto()
            {
                UserName = "TestUser",
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

            // act
            var registrationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", userRegistrationDto);
            var createdUserCredentialId = registrationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");
            var userCredentialsResponse = await _httpClient.GetAsync($"api/userCredentials/{createdUserCredentialId}");
            var userCredentials = await userCredentialsResponse.Content.ReadFromJsonAsync<UserCredentials>();
            var userAccountResponse = await _httpClient.GetAsync($"api/accounts/account/{userCredentials?.UserName}");
            var userAccount = await userAccountResponse.Content.ReadFromJsonAsync<User>();
            var roleResponse = await _httpClient.GetAsync($"api/userCredentials/{createdUserCredentialId}/role");
            var role = await roleResponse.Content.ReadAsStringAsync();
            _purgeList.Add(createdUserCredentialId);

            // assert
            registrationResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            createdUserCredentialId.Should().MatchRegex(@"[A-Za-z0-9]{8}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{12}");

            userCredentialsResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            userCredentials.Should().NotBeNull();
            userCredentials.Should().BeOfType<UserCredentials>();

            userAccountResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            userAccount.Should().NotBeNull();
            userAccount.Should().BeOfType<User>();
            userAccount!.UserCredentialsId.Should().Be(createdUserCredentialId);

            roleResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            role.Should().Be("User");
        }

        [Fact]
        public async Task RegistrateAndAuthorize_ValidUser_ReturnToken()
        {
            // arrange
            var userRegistrationDto = new UserRegistrationDto()
            {
                UserName = "TestUser2",
                Email = "testUser2@mail.ru",
                Password = "testUserPassword2",
                PhoneNumber = "+375250000001",
                FirstName = "Test",
                LastName = "User",
                Patronymic = "Non",
                DateOfBirth = new(1980, 5, 2),
                ResidencePlace = "Republic of Belarus, Gomel, Pedchenko street, 12, 121",
                Sex = Models.Enums.Sex.Female,
                Height = 190,
                Weight = 60,
                SportsActivity = Models.Enums.SportsActivity.Active,
                ClassificationNumber = "RTD458S5891239"
            };

            // act
            var registrationResponse = await _httpClient.PostAsJsonAsync("api/authentication/registration", userRegistrationDto);
            var createdUserCredentialsId = registrationResponse.Content.ReadAsStringAsync().Result.Replace("\"", "");

            var authorizationResponce = await _httpClient.PostAsJsonAsync("api/authentication/login", new UserAuthorizationDto()
            {
                Password = userRegistrationDto.Password,
                UserName = userRegistrationDto.UserName
            });
            var token = await authorizationResponce.Content.ReadAsStringAsync();

            _purgeList.Add(createdUserCredentialsId);

            // assert
            authorizationResponce.StatusCode.Should().Be(HttpStatusCode.OK);

            token.Should().NotBeEmpty();
            token.Should().MatchEquivalentOf("*.*.*");
        }
    }
}
