using Koneshgar.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Koneshgar.DataLayer.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private const string DefaultUserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            var defaultuser = new User
            {
                Id = DefaultUserId,
                UserName = "defaultuser",
                NormalizedUserName = "DEFAULTUSER",
                FirstName = "Default",
                LastName = "User",
                Email = "User@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            defaultuser.PasswordHash = PassGenerate(defaultuser);
            builder.HasData(defaultuser);
        }

        public string PassGenerate(User user)
        {
            var passHash = new PasswordHasher<User>();
            return passHash.HashPassword(user, "159357456qW");
        }
    }
}
