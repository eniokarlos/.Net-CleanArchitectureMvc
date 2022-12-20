using CleanArchMvc.Domain.Profiles;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedUsers()
        {
            if(_userManager.FindByEmailAsync("usuario@localhost.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "usuario@localhost.com",
                    Email = "usuario@localhost.com",
                    NormalizedUserName = "USUARIO@LOCALHOST.COM",
                    NormalizedEmail = "USUARIO@LOCALHOST.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                IdentityResult result = _userManager.CreateAsync(user, "Random#2022").Result;

                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }

            }

            if(_userManager.FindByEmailAsync("admin@localhost.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                IdentityResult result = _userManager.CreateAsync(user, "Random#2022").Result;

                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }
        }

        public void SeedRoles()
        {
            if(!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                };
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }

            if(!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }
    }
}