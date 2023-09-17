using AspNetCoreRateLimit;
using TaskManager.DAL.Seeders;

namespace TaskManager.API.ProgramEntry
{
    /// <summary>
    ///     A static class that provides a extension of services registration after app is build.
    /// </summary>
    public static class AfterAppBuildExtesion
    {
        #region Method

        /// <summary>
        ///     Use all middlewares and custom functions
        ///     after app was builded.
        /// </summary>
        /// <param name="app"> Web app </param>
        /// <param name="Configuration"> A collection of configurations </param>
        /// <returns> Nothing </returns>
        public static async Task Extension
        (
            this WebApplication app,
            IConfiguration Configuration
        )
        {
            // Seed roles 
            await RolesSeeder.SeedRolesAsync(app, Configuration);
            // seed users
            await AccountsSeeder.SeedUsersAsync(app, Configuration);
            // A RateLimitMiddleWare 
            app.UseIpRateLimiting();
        }

        #endregion
    }
}
