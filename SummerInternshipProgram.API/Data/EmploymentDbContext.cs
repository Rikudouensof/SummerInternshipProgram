using Microsoft.EntityFrameworkCore;
using SummerInternshipProgram.API.Models.DatabaseModels;
using System.IO.Pipes;

namespace SummerInternshipProgram.API.Data
{
    public class EmploymentDbContext : DbContext
    {
        public EmploymentDbContext(DbContextOptions<EmploymentDbContext> options)
          : base(options)
        {
        }
        public DbSet<Applicant>? Applicants { get; set; }
        public DbSet<DateQuestion>? DateQuestions { get; set; }
        public DbSet<DropdownQuestion>? DropdownQuestions { get; set; }
        public DbSet<Gender>? Genders { get; set; }
        public DbSet<MultipleChoiceQuestion>? MultipleChoiceQuestions { get; set; }
        public DbSet<NumericQuestion>? NumericQuestions { get; set; }
        public DbSet<ParagraphQuestion>? ParagraphQuestions { get; set; }
        public DbSet<InternshipProgram>? Programs { get; set; }
        public DbSet<QuestionType>? QuestionTypes { get; set; }
        public DbSet<YesOrNoQuestion>? YesOrNoQuestions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<DateQuestion>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<DropdownQuestion>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id).OwnsMany(item => item.QuestionOptions);

            modelBuilder.Entity<Gender>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);

            modelBuilder.Entity<MultipleChoiceQuestion>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id).OwnsMany(options => options.QuestionOptions);
            modelBuilder.Entity<NumericQuestion>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<ParagraphQuestion>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<InternshipProgram>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<QuestionType>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<YesOrNoQuestion>().ToContainer("Applicants").HasPartitionKey(partitionkey => partitionkey.Id);
            modelBuilder.Entity<MultipleChoiceQuestion>().OwnsMany(answers => answers.QuestionAnswers);

            modelBuilder.Entity<Applicant>().OwnsMany(x => x.DateAnswers);
            modelBuilder.Entity<Applicant>().OwnsMany(x => x.DropdownAnswers);
            modelBuilder.Entity<Applicant>().OwnsMany(x => x.MultipleChoiceAnswers);
            modelBuilder.Entity<Applicant>().OwnsMany(x => x.NumericAnswers);
            modelBuilder.Entity<Applicant>().OwnsMany(x => x.ParagraphAnswers);
            modelBuilder.Entity<Applicant>().OwnsMany(x => x.YesOrNoAnswers);
        }
    }



}
