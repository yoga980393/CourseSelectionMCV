using Microsoft.EntityFrameworkCore;
using CourseSelectionMCV.Models;

namespace CourseSelectionMCV.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EnrolledCourse> EnrolledCourses { get; set; }
        public DbSet<FavoriteCourse> FavoriteCourses { get; set; }
        public DbSet<LotteryCourse> LotteryCourses { get; set; }
        public DbSet<GeneralCoursesWishlist> GeneralCoursesWishlist { get; set; }
        public DbSet<CreditRequirements> CreditRequirements { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
