using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Mapping;
using MusicCatalogAPI.Models.AccountDtos;
using MusicCatalogAPI.Repositories.Interfaces;
using MusicCatalogAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MusicCatalogTests
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserServiceTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task Login_Should_Be_Successful()
        {
            LoginUserDto loginUser = new LoginUserDto { Username = "User1", Password = "Pass1234" };
            User expectedUser = new User { Username = "User1", PasswordHash = _passwordHasher.HashPassword(_mapper.Map<User>(loginUser), loginUser.Password) };
            _userRepoMock.Setup(r => r.GetUserAsync(loginUser.Username)).ReturnsAsync(expectedUser);

            UserService userService = new UserService(_userRepoMock.Object, _mapper, _passwordHasher);
            var result = await userService.GetUser(loginUser);

            var expected = JsonConvert.SerializeObject(expectedUser);
            var actual = JsonConvert.SerializeObject(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Supplier_Register_Should_Be_Successful()
        {
            var registerSupplier = new RegisterSupplierDto { Username = "Supplier1", Password = "1234" };
            List<User> users = new List<User>(); 
            _userRepoMock.Setup(r => r.AddUserAsync(It.IsAny<User>())).Callback((User user) => users.Add(user));

            UserService userService = new UserService(_userRepoMock.Object, _mapper, _passwordHasher);
            await userService.RegisterSupplierAsync(registerSupplier);

            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task Password_Should_Be_Verified_Correctly()
        {
            LoginUserDto loginUser = new LoginUserDto { Username = "User1", Password = "Pass1234" };
            User expectedUser = new User { Username = "User1", PasswordHash = _passwordHasher.HashPassword(_mapper.Map<User>(loginUser), loginUser.Password) };
            _userRepoMock.Setup(r => r.GetUserAsync(loginUser.Username)).ReturnsAsync(expectedUser);

            UserService userService = new UserService(_userRepoMock.Object, _mapper, _passwordHasher);
            var result = await userService.ValidatePassword(loginUser);

            Assert.Equal(PasswordVerificationResult.Success, result);
        }

        [Fact]
        public async Task PasswordShouldBeVerifiedAsIncorrect()
        {
            LoginUserDto loginUser = new LoginUserDto { Username = "User1", Password = "Pass1234" };
            User expectedUser = new User { Username = "User1", PasswordHash = _passwordHasher.HashPassword(_mapper.Map<User>(loginUser), "OtherPassword") };
            _userRepoMock.Setup(r => r.GetUserAsync(loginUser.Username)).ReturnsAsync(expectedUser);

            UserService userService = new UserService(_userRepoMock.Object, _mapper, _passwordHasher);
            var result = await userService.ValidatePassword(loginUser);

            Assert.Equal(PasswordVerificationResult.Failed, result);
        }
    }
}
