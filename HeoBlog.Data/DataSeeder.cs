using HeoBlog.Core.Domain.Edentity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeoBlog.Data
{
    public class DataSeeder
    {
        public async Task SeedAsync(HeoBlogContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            //Phan Quyen
            var rootAdminRoleId = Guid.NewGuid();
            if(!context.Roles.Any())
            {
                await context.Roles.AddAsync(new AppRole()
                {
                    Id = rootAdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Quan Tri Vien"
                });
                await context.SaveChangesAsync();
            }
            if(!context.Users.Any())
            {
                var userId = Guid.NewGuid();
                var user = new AppUser()
                {
                    Id = userId,
                    FirstName = "Hung",
                    LastName = "Duong",
                    Email = "Heo@gmail.com",
                    NormalizedEmail = "ADMIN",
                    UserName = "admin",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    DateCreated = DateTime.Now
                };
                user.PasswordHash = passwordHasher.HashPassword(user,"Admin@1234$");
                await context.AddAsync(user);
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    RoleId = rootAdminRoleId,
                    UserId = userId,
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
