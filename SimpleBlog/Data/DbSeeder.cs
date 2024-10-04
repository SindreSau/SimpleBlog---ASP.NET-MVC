using Microsoft.AspNetCore.Identity;
using SimpleBlog.Models;

namespace SimpleBlog.Data;

public class DbSeeder
{
    public static async Task SeedDatabase(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
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

                // Create a new post
                var testPost = new BlogPost
                {
                    Title = "Test Post",
                    Content = "This is a test post",
                    Author = testUser,
                };

                // Add the post to the database
                context.BlogPosts.Add(testPost);
                await context.SaveChangesAsync();
            }
        }
    }
}