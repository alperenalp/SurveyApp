using Microsoft.EntityFrameworkCore;
using SurveyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Data
{
    public class SurveyAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<FilledSurvey> FilledSurveys { get; set; }

        public SurveyAppDbContext(DbContextOptions<SurveyAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.LastName).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Username).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Password).HasMaxLength(50);

            modelBuilder.Entity<User>().HasMany(u => u.Surveys)
                                       .WithOne(s => s.User)
                                       .HasForeignKey(s => s.UserId);

            //Survey
            modelBuilder.Entity<Survey>().Property(s => s.Title).IsRequired();
            modelBuilder.Entity<Survey>().Property(s => s.Title).HasMaxLength(100);

            modelBuilder.Entity<Survey>().HasMany(s => s.Questions)
                                         .WithOne(q => q.Survey)
                                         .HasForeignKey(q => q.SurveyId);

            modelBuilder.Entity<Survey>().HasMany(s => s.FilledSurveys)
                                         .WithOne(q => q.Survey)
                                         .HasForeignKey(q => q.SurveyId);

            //Question
            modelBuilder.Entity<Question>().Property(q => q.Title).IsRequired();
            modelBuilder.Entity<Question>().Property(q => q.Title).HasMaxLength(100);
            modelBuilder.Entity<Question>().Property(q => q.Type).IsRequired();

            modelBuilder.Entity<Question>().HasMany(q => q.Options)
                                           .WithOne(o => o.Question)
                                           .HasForeignKey(o => o.QuestionId);

            modelBuilder.Entity<Question>().HasOne(q => q.Survey)
                                           .WithMany(s => s.Questions)
                                           .HasForeignKey(q => q.SurveyId);

            //FilledSurvey
            modelBuilder.Entity<FilledSurvey>().HasOne(fs => fs.Survey)
                                               .WithMany(s => s.FilledSurveys)
                                               .HasForeignKey(fs => fs.SurveyId);

            modelBuilder.Entity<FilledSurvey>().HasMany(fs => fs.FilledSurveyOptions)
                                               .WithOne(fso => fso.FilledSurvey)
                                               .HasForeignKey(fso => fso.FilledSurveyId)
                                               .OnDelete(DeleteBehavior.Restrict);

            //Options
            modelBuilder.Entity<Option>().Property(q => q.Title).IsRequired();
            modelBuilder.Entity<Option>().Property(q => q.Title).HasMaxLength(50);

            modelBuilder.Entity<Option>().HasOne(o => o.Question)
                                         .WithMany(q => q.Options)
                                         .HasForeignKey(o => o.QuestionId);

            modelBuilder.Entity<Option>().HasMany(o => o.FilledSurveyOptions)
                                         .WithOne(fso => fso.Option)
                                         .HasForeignKey(fso => fso.OptionId)
                                         .OnDelete(DeleteBehavior.Restrict);


            //n-n relation
            modelBuilder.Entity<FilledSurveyOption>().HasKey("FilledSurveyId", "OptionId");



        }
    }
}
