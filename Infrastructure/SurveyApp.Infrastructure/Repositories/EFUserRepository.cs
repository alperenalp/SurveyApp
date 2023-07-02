using Microsoft.EntityFrameworkCore;
using SurveyApp.Entities;
using SurveyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly SurveyAppDbContext _context;

        public EFUserRepository(SurveyAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        
    }
}
