using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Interfaces;
using SimpleBlog.Models;

namespace SimpleBlog.Repositories;

public class BlogPostRepository(BlogDbContext context) : IBlogPostRepository
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

    public async Task AddAsync(BlogPost blogPost)
    {
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
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await context.BlogPosts.CountAsync();
    }
}