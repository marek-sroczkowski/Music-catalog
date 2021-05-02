using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MusicCatalogAPI.Entities;
using MusicCatalogAPI.Interfaces;
using MusicCatalogAPI.Models.AccountDtos;
using MusicCatalogAPI.Repositories;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> GetUser(LoginUserDto userModel)
        {
            var user = await _userRepository.GetUserAsync(userModel.Username);
            return _mapper.Map<UserDto>(user);
        }

        public async Task RegisterSupplierAsync(RegisterSupplierDto newSupplier)
        {
            var newUser = _mapper.Map<Supplier>(newSupplier);
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, newSupplier.Password);
            await _userRepository.AddUserAsync(newUser);
        }

        public async Task<PasswordVerificationResult> ValidatePassword(LoginUserDto userModel)
        {
            var user = await _userRepository.GetUserAsync(userModel.Username);
            return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userModel.Password);
        }
    }
}
