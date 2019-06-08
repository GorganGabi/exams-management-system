using System;
using System.Linq;
using System.Threading.Tasks;
using EMS.Domain;
using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence
{
    public sealed class EMSContext : DbContext, IRepository
    {
        public EMSContext(DbContextOptions<EMSContext> options)
            : base(options) => Database.Migrate();

        internal DbSet<Student> Students { get; private set; }
        internal DbSet<Professor> Professors { get; private set; }
        internal DbSet<StudentCourse> StudentCourse { get; private set; }
        internal DbSet<StudentExam> StudentExam { get; private set; }
        internal DbSet<Exam> Exams { get; private set; }
        internal DbSet<Course> Courses { get; private set; }
        internal DbSet<Grade> Grades { get; private set; }

        public IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : Entity => Set<TEntity>().AsNoTracking();

        public async Task<TEntity> FindByIdAsync<TEntity>(Guid id)
            where TEntity : Entity =>
            await Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);

        public async Task TryUpdateModelAsync<TEntity>(TEntity entityToUpdate, TEntity updatedEntity)
             where TEntity : IUpdatable<TEntity> => entityToUpdate.Update(updatedEntity);   

        public async Task AddNewAsync<TEntity>(TEntity entity)
            where TEntity : Entity => await Set<TEntity>().AddAsync(entity);

        public async Task SaveAsync() => await SaveChangesAsync();

        public async Task RemoveAsync<TEntity>(TEntity entity)
            where TEntity : Entity => Remove(entity);
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
               .HasMany(g => g.Grades)
               .WithOne(e => e.Exam)
               .IsRequired();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(e => e.Course);

            modelBuilder.Entity<Professor>()
                .HasMany(p => p.Course)
                .WithOne(c => c.Professor);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<StudentExam>()
                .HasKey(sc => new { sc.StudentId, sc.ExamId });
            modelBuilder.Entity<StudentExam>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<StudentExam>()
                .HasOne(sc => sc.Exam)
                .WithMany(c => c.StudentExams)
                .HasForeignKey(sc => sc.ExamId);

        }
    }
}
