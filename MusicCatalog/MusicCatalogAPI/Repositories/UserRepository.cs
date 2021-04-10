using Microsoft.EntityFrameworkCore;
using MusicCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<User>> GetUsersAsync() => await dbContext.Users.ToListAsync();

        public async Task<ICollection<Supplier>> GetSuppliersAsync() => await dbContext.Suppliers
            .Include(s => s.Albums)
            .ToListAsync();

        public async Task<User> GetUserAsync(string username) => await dbContext.Users
            .FirstOrDefaultAsync(u => u.Username.Equals(username));

        public async Task<Supplier> GetSupplierAsync(string username) => await dbContext.Suppliers
            .Include(s => s.Albums)
            .FirstOrDefaultAsync(s => s.Username.Equals(username));

        public async void AddUserAsync<T>(T user) where T : User
        {
            if(user is Supplier)
                await dbContext.Suppliers.AddAsync(user as Supplier);

            await dbContext.SaveChangesAsync();
        }

        public async void DeleteUserAsync(User user)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
