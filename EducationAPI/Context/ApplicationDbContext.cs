using EducationAPI.Configurations;
using EducationAPI.Data;
using EducationAPI.Entities;
using EducationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EducationAPI.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUserEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region DbSet
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<AppUserEntity> Users { get; set; }
        public DbSet<CartDetailsEntity> CartDetails { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<LectureEntity> Lectures { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<NotifycationEntity> Notifycations { get; set; }
        public DbSet<OrderDetailsEntity> OrderDetails { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<PromotionEntity> Promotions { get; set; }
        public DbSet<QuizEntity> Quizzes { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<SectionEntity> Sections { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<StudentLessonEntity> StudentLessons { get; set; }
        public DbSet<StudentQuizEntity> StudentQuizzes { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                    entityType.SetTableName(tableName.Substring(6));
            }

            builder.ApplyConfiguration(new AdminConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new CartDetailsConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new CourseConfigugation());
            builder.ApplyConfiguration(new LectureConfiguration());
            builder.ApplyConfiguration(new LessonConfiguration());
            builder.ApplyConfiguration(new NotifycationConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailsConfiguration());
            builder.ApplyConfiguration(new PromotionConfiguration());
            builder.ApplyConfiguration(new QuizConfiguration());
            builder.ApplyConfiguration(new RatingConfiguration());
            builder.ApplyConfiguration(new SectionConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new StudentLessonConfiguration());
            builder.ApplyConfiguration(new StudentQuizConfiguration());
        }
    }
}
