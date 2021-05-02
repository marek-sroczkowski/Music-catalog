using WebApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<Supplier> GetSupplierAsync(string username);
        Task<ICollection<Supplier>> GetSuppliersAsync();
        Task<User> GetUserAsync(string username);
        Task<ICollection<User>> GetUsersAsync();
    }
}