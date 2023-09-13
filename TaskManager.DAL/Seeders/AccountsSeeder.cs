#region Usings
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.DAL.Context;
using TaskManager.DAL.Models;
#endregion

namespace TaskManager.DAL.Seeders
{
    /// <summary>
    ///     A Seeder Class for users.
    /// </summary>
    public class AccountsSeeder
    {
        #region Method implementation

        /// <summary>
        ///     Seed Users with roles, users are retrieved from appsettings.json.
        /// </summary>
        /// <param name="applicationBuilder"> App Builder of type <see cref="IApplicationBuilder"/> </param>
        /// <param name="configuration"> Configuration of type <see cref="IConfiguration"/> </param>
        /// <returns> Nothing </returns>
        public static async System.Threading.Tasks.Task
        SeedUsersAsync
        (
            IApplicationBuilder applicationBuilder,
            IConfiguration configuration
        )
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            var _context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (_context is not null)
            {
                _context.Database.EnsureCreated();

                var getUsers = configuration.GetSection(AccountSettings.SectionName).Get<AccountSettings[]>();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                foreach (var item in getUsers!)
                {
                    var User = await userManager.FindByEmailAsync(item.UserName);

                    if (User == null)
                    {
                        var newUser = new User()
                        {
                            UserName = item.UserName,
                            Email = item.UserName,
                            EmailConfirmed = true,
                            CreatedAt = DateTime.Now,
                            FirstName = item.UserName,
                            LastName = item.SurnName,
                            NormalizedEmail = item.UserName.ToUpper(),
                            IsDeleted = false,
                            PhoneNumberConfirmed = true,
                        };

                        await userManager.CreateAsync(newUser, item.Password);

                        foreach (var role in item.Roles)
                        {
                            await userManager.AddToRoleAsync(newUser, role);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
