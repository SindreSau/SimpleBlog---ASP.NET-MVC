using Microsoft.AspNetCore.Identity;
using SimpleBlog.Models;

namespace SimpleBlog.Data;

public static class DbSeeder
{
    public static async Task SeedDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Ensure the database is created
        await context.Database.EnsureCreatedAsync();

        // Seed the database

        // Check if there are any users
        if (!context.Users.Any())
        {
            // Create a new user
            var testUser = new IdentityUser
            {
                UserName = "testUser",
                Email = "test@test.com",
                EmailConfirmed = true
            };

            // Create the user
            await userManager.CreateAsync(testUser, "Test123!");

            // Create a few test posts
            var testPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Title = "Test Post 1",
                    Content = "This is the first test post",
                    AuthorId = testUser.Id
                },
                new BlogPost
                {
                    Title = "Test Post 2",
                    Content = "This is the second test post",
                    AuthorId = testUser.Id
                },
                new BlogPost
                {
                    Title = "Test Post 3",
                    Content = "This is the third test post",
                    AuthorId = testUser.Id
                }
            };

            // Add the posts to the database
            await context.BlogPosts.AddRangeAsync(testPosts);
            await context.SaveChangesAsync();
        }
    }
}