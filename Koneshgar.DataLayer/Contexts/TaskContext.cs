using Koneshgar.Domain.Models.Tasks;
using Koneshgar.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = Koneshgar.Domain.Models.Tasks.TaskEntity;
using TaskStatus = Koneshgar.Domain.Models.Tasks.TaskStatus;

namespace Koneshgar.DataLayer.Contexts
{
    public class TaskContext:IdentityDbContext<User, IdentityRole, string>
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
            builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name:"UserRoles"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name:"UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name:"UserLogins"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name:"UserTokens"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name:"RoleClaims"); });
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }        
        public DbSet<TaskStatus> TaskStatuses { get; set; }
    }
}
