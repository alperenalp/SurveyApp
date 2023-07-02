using Microsoft.EntityFrameworkCore;
using SurveyApp.Entities;
using SurveyApp.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public class EFQuestionRepository : IQuestionRepository
    {
        private readonly SurveyAppDbContext _context;

        public EFQuestionRepository(SurveyAppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            return question.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Question>> GetAllAsync()
        {
            return await _context.Questions.AsNoTracking().ToListAsync();
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Questions.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Question entity)
        {
            _context.Questions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Question>> GetAllBySurveyId(int surveyId)
        {
            return await _context.Questions.AsNoTracking().Where(x => x.SurveyId == surveyId).ToListAsync();
        }
    }
}
