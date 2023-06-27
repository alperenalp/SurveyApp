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
    public class EFFilledSurveyRepository : IFilledSurveyRepository
    {
        private readonly SurveyAppDbContext _context;

        public EFFilledSurveyRepository(SurveyAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(FilledSurvey entity)
        {
            _context.FilledSurveys.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var filledSurvey = await _context.FilledSurveys.FindAsync(id);
            _context.FilledSurveys.Remove(filledSurvey);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<FilledSurvey>> GetAllAsync()
        {
            return await _context.FilledSurveys.AsNoTracking().ToListAsync();
        }

        public async Task<FilledSurvey> GetByIdAsync(int id)
        {
            return await _context.FilledSurveys.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(FilledSurvey entity)
        {
            _context.FilledSurveys.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
