/*
    This class represents the database context for the Task Manager application.
    It derives from IdentityDbContext<User> to include user authentication and authorization functionality.

    The ApplicationDbContext class provides access to the underlying database tables through DbSet properties.
    It includes DbSet properties for various entities such as Label, Project, Comment, Task, Category, Priority, User, and Logger.
*/

#region Usings
using TaskManager.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
#endregion

namespace TaskManager.DAL.Context
{
    /// <summary>
    /// Represents the database context for the Task Manager application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        /// <summary>
        /// Gets or sets the database table for labels.
        /// </summary>
        public DbSet<Label> Labels { get; set; }

        /// <summary>
        /// Gets or sets the database table for projects.
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the database table for comments.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the database table for tasks.
        /// </summary>
        public DbSet<Models.Task> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the database table for categories.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the database table for priorities.
        /// </summary>
        public DbSet<Priority> Priorities { get; set; }

        /// <summary>
        /// Gets or sets the database table for users.
        /// </summary>
        public override DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the database table for logs.
        /// </summary>
        public DbSet<Logger> Logs { get; set; }
    }
}