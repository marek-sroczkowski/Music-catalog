using Microsoft.AspNetCore.Identity;
using WebApi.Models.AccountDtos;
using System.Threading.Tasks;

namespace WebApi.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(LoginUserDto user);
        Task RegisterSupplierAsync(RegisterSupplierDto user);
        Task<PasswordVerificationResult> ValidatePassword(LoginUserDto userModel);
    }
}