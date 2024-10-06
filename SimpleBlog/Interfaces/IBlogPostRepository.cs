using SimpleBlog.DTOs;
using SimpleBlog.Models;

namespace SimpleBlog.Interfaces;

public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(int id);
    Task AddAsync(BlogPostInputDto blogPost);
    Task UpdateAsync(BlogPost blogPost);
    Task DeleteAsync(int id);
    Task<IEnumerable<BlogPost>> GetPagedAsync(int pageNumber, int pageSize);
    Task<int> GetCountAsync();
    Task<int> GetAuthorPostCountAsync(string authorId);
    Task<IEnumerable<BlogPost>> GetPagedByAuthorIdAsync(string authorId, int pageNumber, int pageSize);
}