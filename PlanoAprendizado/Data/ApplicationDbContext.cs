
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanoAprendizado.Models;

namespace PlanoAprendizado.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Person> People { get; set; }
        public DbSet<Learning> Learnings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<ActualState> ActualStates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Circle> Circles { get; set; }
        public DbSet<TypeConsultor> TypeConsultors { get; set; }
        public DbSet<PersonLearn> PeopleLearns { get; set; }
        public DbSet<PersonFeedback> PeopleFeedbacks { get; set; }
        public DbSet<DayTime> DayTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActualState>()
                    .HasOne(e => e.Person)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonFeedback>()
                   .HasOne(e => e.Person)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonLearn>()
               .HasOne(e => e.Person)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
        }

    }
}