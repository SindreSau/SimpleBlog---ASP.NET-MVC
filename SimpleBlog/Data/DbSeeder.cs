using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Data;

public static class DbSeeder
{
    public static async Task<IHost> SeedDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BlogDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        try
        {
            await context.Database.MigrateAsync(); // Ensure the database is created and up to date
            await SeedUsers(userManager);
            await SeedBlogPosts(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }

        return host;
    }

    private static async Task SeedUsers(UserManager<IdentityUser> userManager)
    {
        if (await userManager.FindByEmailAsync("admin@example.com") == null)
        {
            var user = new IdentityUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "Admin123!");
        }

        if (await userManager.FindByEmailAsync("user@example.com") == null)
        {
            var user = new IdentityUser
            {
                UserName = "user@example.com",
                Email = "user@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "User123!");
        }
    }

    private static async Task SeedBlogPosts(BlogDbContext context)
    {
        if (!await context.BlogPosts.AnyAsync())
        {
            var admin = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@example.com");
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == "user@example.com");

            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Title = "Getting Started with ASP.NET Core",
                    Content =
                        "ASP.NET Core is a cross-platform, high-performance, open-source framework for building modern, cloud-based, Internet-connected applications.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-5),
                    UpdatedDate = DateTime.Now.AddDays(-5)
                },
                new BlogPost
                {
                    Title = "Introduction to Entity Framework Core",
                    Content =
                        "Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-4),
                    UpdatedDate = DateTime.Now.AddDays(-4)
                },
                new BlogPost
                {
                    Title = "Dependency Injection in .NET",
                    Content =
                        "Dependency Injection is a design pattern and a mechanism for implementing IoC between classes and their dependencies.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-3),
                    UpdatedDate = DateTime.Now.AddDays(-3)
                },
                new BlogPost
                {
                    Title = "Building RESTful APIs with ASP.NET Core",
                    Content =
                        "RESTful APIs are a popular way of building web services. ASP.NET Core provides excellent support for building RESTful APIs.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-2),
                    UpdatedDate = DateTime.Now.AddDays(-2)
                },
                new BlogPost
                {
                    Title = "Unit Testing in C#",
                    Content =
                        "Unit testing is a software testing method by which individual units of source code are tested to determine whether they are fit for use.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-1),
                    UpdatedDate = DateTime.Now.AddDays(-1)
                },
                new BlogPost
                {
                    Title = "Asynchronous Programming in C#",
                    Content =
                        "Asynchronous programming is a powerful feature in C# that allows for non-blocking code execution. This post explores async/await keywords and best practices.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    UpdatedDate = DateTime.Now.AddDays(-7)
                },
                new BlogPost
                {
                    Title = "LINQ: Simplifying Data Queries in .NET",
                    Content =
                        "Language Integrated Query (LINQ) is a powerful feature in .NET that simplifies querying and manipulating data. Learn how to use LINQ to write more expressive and concise code.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-6),
                    UpdatedDate = DateTime.Now.AddDays(-6)
                },
                new BlogPost
                {
                    Title = "Securing Your ASP.NET Core Application",
                    Content =
                        "Security is crucial for web applications. This post covers best practices for securing your ASP.NET Core application, including authentication, authorization, and data protection.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-8),
                    UpdatedDate = DateTime.Now.AddDays(-8)
                },
                new BlogPost
                {
                    Title = "Microservices Architecture with .NET",
                    Content =
                        "Microservices architecture is a popular approach for building scalable and maintainable applications. Learn how to implement microservices using .NET technologies.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-9),
                    UpdatedDate = DateTime.Now.AddDays(-9)
                },
                new BlogPost
                {
                    Title = "Blazor: Building Interactive Web UIs with C#",
                    Content =
                        "Blazor allows developers to build interactive web UIs using C# instead of JavaScript. Discover how Blazor works and how to create your first Blazor application.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-10),
                    UpdatedDate = DateTime.Now.AddDays(-10)
                },
                new BlogPost
                {
                    Title = "Optimizing Entity Framework Core Performance",
                    Content =
                        "Entity Framework Core is powerful, but it's important to optimize its performance for large-scale applications. This post provides tips and tricks for improving EF Core performance.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-11),
                    UpdatedDate = DateTime.Now.AddDays(-11)
                },
                new BlogPost
                {
                    Title = "Containerizing .NET Applications with Docker",
                    Content =
                        "Docker containers provide a consistent and isolated environment for your applications. Learn how to containerize your .NET applications for easier deployment and scalability.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-12),
                    UpdatedDate = DateTime.Now.AddDays(-12)
                },
                new BlogPost
                {
                    Title = "Implementing Clean Architecture in ASP.NET Core",
                    Content =
                        "Clean Architecture is a software design philosophy that separates concerns and makes your application more maintainable. Discover how to implement Clean Architecture in ASP.NET Core projects.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-13),
                    UpdatedDate = DateTime.Now.AddDays(-13)
                },
                new BlogPost
                {
                    Title = "Real-time Web Applications with SignalR",
                    Content =
                        "SignalR is a powerful library for adding real-time web functionality to your applications. Learn how to use SignalR to build interactive, real-time features in your ASP.NET Core apps.",
                    AuthorId = admin.Id,
                    CreatedDate = DateTime.Now.AddDays(-14),
                    UpdatedDate = DateTime.Now.AddDays(-14)
                },
                new BlogPost
                {
                    Title = "Automated Testing Strategies for .NET Applications",
                    Content =
                        "Automated testing is crucial for maintaining code quality and preventing regressions. This post covers various testing strategies and tools for .NET applications, including unit testing, integration testing, and end-to-end testing.",
                    AuthorId = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-15),
                    UpdatedDate = DateTime.Now.AddDays(-15)
                }
            };

            await context.BlogPosts.AddRangeAsync(blogPosts);
            await context.SaveChangesAsync();
        }
    }
}