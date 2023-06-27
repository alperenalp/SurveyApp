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
    public class EFSurveyRepository : ISurveyRepository
    {
        private readonly SurveyAppDbContext _context;

        public EFSurveyRepository(SurveyAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Survey survey)
        {
            await _context.Surveys.AddAsync(survey);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Survey>> GetAllAsync()
        {
            return await _context.Surveys.AsNoTracking().ToListAsync();
        }

        public async Task<Survey> GetByIdAsync(int id)
        {
            return await _context.Surveys.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Survey entity)
        {
            _context.Surveys.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
