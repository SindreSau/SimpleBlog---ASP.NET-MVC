using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Data;

public class BlogDbContext(DbContextOptions<BlogDbContext> options, IWebHostEnvironment env)
    : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (env.IsDevelopment())
        {
            // SQLite-specific configurations

        }
        else
        {
            // PostgreSQL-specific configurations

        }
    }

    // Add a DbSet for each entity
    public DbSet<BlogPost> BlogPosts { get; set; }
}