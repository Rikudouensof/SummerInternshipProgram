using Microsoft.EntityFrameworkCore;
using SummerInternshipProgram.API.Models.DatabaseModels;

namespace SummerInternshipProgram.API.Data
{
    public class EmploymentDbContext : DbContext
    {

        public DbSet<Applicant>? Applicants { get; set; }
        public DbSet<DateQuestion>? DateQuestions { get; set; }
        public DbSet<DropdownQuestion>? DropdownQuestions { get; set; }
        public DbSet<Gender>? Genders { get; set; }
        public DbSet<MultipleChoiceQuestion>? MultipleChoiceQuestions { get; set; }
        public DbSet<NumericQuestion>? NumericQuestions { get; set; }
        public DbSet<ParagraphQuestion>? ParagraphQuestions { get; set; }
        public DbSet<Program>? Programs { get; set; }
        public DbSet<QuestionType>? QuestionTypes { get; set; }
        public DbSet<YesOrNoQuestion>? YesOrNoQuestions { get; set; }






        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }



}
