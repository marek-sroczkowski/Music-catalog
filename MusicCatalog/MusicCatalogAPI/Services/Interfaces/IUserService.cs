using Microsoft.AspNetCore.Identity;
using MusicCatalogAPI.Models.AccountDtos;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(LoginUserDto user);
        Task RegisterSupplierAsync(RegisterSupplierDto user);
        Task<PasswordVerificationResult> ValidatePassword(LoginUserDto userModel);
    }
}