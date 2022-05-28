using Xunit;
using FluentAssertions;
using WorkoutGlobal.Api.Repositories.ModelsRepositories;
using System.Threading.Tasks;
using System;

namespace WorkoutGlobal.Api.Tests.Repositories
{
    public class UserCredentialsRepositoryTests
    {
        private readonly UserCredentialsRepository _userCredentialsRepository;

        public UserCredentialsRepositoryTests()
        {
            // TODO: Исправить тест
            // _userCredentialsRepository = new UserCredentialsRepository();
        }

        [Fact]
        public async Task GetHashPasswordAsync_ValidUserPasswordCredentials_ReturnHashedPassword()
        {
            // arrange 
            var (password, salt) = ("qwerty123", "123");

            // act
            var result = await _userCredentialsRepository.GetHashPasswordAsync(password, salt);

            // assert

            result.Should().BeOfType(typeof(string));
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().MatchRegex("^([0-9a-z]{1})([0-9a-z]*)$");
        }

        [Fact]
        public async Task GetHashPasswordAsync_NullPassword_ReturnArgumentNullException()
        {
            // arrange 
            var salt = "123";

            // act
            var result = async () => await _userCredentialsRepository.GetHashPasswordAsync(null, salt);

            // assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetHashPasswordAsync_NullPasswordSalt_ReturnArgumentNullException()
        {
            // arrange 
            var password = "123";

            // act
            var result = async () => await _userCredentialsRepository.GetHashPasswordAsync(password, null);

            // assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetHashPasswordAsync_EmptyPassword_ReturnArgumentNullException()
        {
            // arrange 
            var (password, salt) = ("", "123");

            // act
            var result = async () => await _userCredentialsRepository.GetHashPasswordAsync(password, salt);

            // assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetHashPasswordAsync_EmptyPasswordSalt_ReturnArgumentNullException()
        {
            // arrange 
            var (password, salt) = ("qwerty", "");

            // act
            var result = async () => await _userCredentialsRepository.GetHashPasswordAsync(password, salt);

            // assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
