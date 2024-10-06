using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.DTOs;
using SimpleBlog.Interfaces;
using SimpleBlog.Models;
using ILogger = Castle.Core.Logging.ILogger;

namespace SimpleBlog.Repositories;

public class BlogPostRepository(BlogDbContext context, ILogger<BlogPostRepository> logger) : IBlogPostRepository
{

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        // Include the author to also get the author information
        return await context.BlogPosts
            .Include(b => b.Author)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        // Include the author to also get the author information
        return await context.BlogPosts
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.BlogPostId == id);
    }

    public async Task AddAsync(BlogPostInputDto blogPostInputDto)
    {
        var blogPost = new BlogPost
        {
            Title = blogPostInputDto.Title,
            Content = blogPostInputDto.Content,
            AuthorId = blogPostInputDto.AuthorId,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };

        await context.BlogPosts.AddAsync(blogPost);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        context.BlogPosts.Update(blogPost);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var blogPost = await context.BlogPosts.FindAsync(id);
        if (blogPost != null)
        {
            context.BlogPosts.Remove(blogPost);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<BlogPost>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await context.BlogPosts
            .Include(b => b.Author)
            .OrderByDescending(b => b.UpdatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await context.BlogPosts.CountAsync();
    }

    public async Task<int> GetAuthorPostCountAsync(string authorId)
    {
        return await context.BlogPosts.CountAsync(b => b.AuthorId == authorId);
    }

    public async Task<IEnumerable<BlogPost>> GetPagedByAuthorIdAsync(string authorId, int pageNumber, int pageSize)
    {
        return await context.BlogPosts
            .Include(b => b.Author)
            .Where(b => b.AuthorId == authorId)
            .OrderByDescending(b => b.UpdatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}