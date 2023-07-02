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
    public class EFOptionRepository : IOptionRepository
    {
        private readonly SurveyAppDbContext _context;

        public EFOptionRepository(SurveyAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Option entity)
        {
            await _context.Options.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var option = await _context.Options.FindAsync(id);
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Option>> GetAllAsync()
        {
            return await _context.Options.AsNoTracking().ToListAsync();
        }

        public async Task<Option> GetByIdAsync(int id)
        {
            return await _context.Options.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Options.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Option entity)
        {
            _context.Options.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
