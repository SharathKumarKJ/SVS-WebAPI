using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WebAppAngular5.Base;

namespace WebAppAngular5.Models
{
    public class Repository :IdentityDbContext<User>, IRepository
    {
        public Repository() : base("DefaultConnection", false)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<ClassDetail> ClassDetails { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<FeeDetail> FeeDetails { get; set; }

        public DbSet<TeacherSubjectDetail> TeacherSubjectDetails { get; set; }

  

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .ToTable("Users");
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogins");
        }

    
    }
}