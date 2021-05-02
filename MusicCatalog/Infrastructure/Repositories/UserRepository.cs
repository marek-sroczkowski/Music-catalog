using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<User>> GetUsersAsync() => await _dbContext.Users.ToListAsync();

        public async Task<ICollection<Supplier>> GetSuppliersAsync() => await _dbContext.Suppliers
            .Include(s => s.Albums)
            .ToListAsync();

        public async Task<User> GetUserAsync(string username) => await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username.Equals(username));

        public async Task<Supplier> GetSupplierAsync(string username) => await _dbContext.Suppliers
            .Include(s => s.Albums)
            .FirstOrDefaultAsync(s => s.Username.Equals(username));

        public async Task AddUserAsync(User user)
        {
            switch(user)
            {
                case Supplier _ when user is Supplier:
                    await _dbContext.Suppliers.AddAsync(user as Supplier);
                    break;
                default:
                    await _dbContext.Users.AddAsync(user);
                    break;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
