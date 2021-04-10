using MusicCatalogAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public interface IUserRepository
    {
        void AddUserAsync<T>(T user) where T : User;
        Task<Supplier> GetSupplierAsync(string username);
        Task<ICollection<Supplier>> GetSuppliersAsync();
        Task<User> GetUserAsync(string username);
        Task<ICollection<User>> GetUsersAsync();
    }
}