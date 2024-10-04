using Microsoft.AspNetCore.Mvc;
using SimpleBlog.DTOs;
using SimpleBlog.Interfaces;
using SimpleBlog.Mappers;

namespace SimpleBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostController: ControllerBase
{
    private readonly IBlogPostRepository _blogPostRepository;

    public BlogPostController(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    // GET: api/BlogPosts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPostOutputDto>>> GetBlogPosts()
    {
        var blogPosts = await _blogPostRepository.GetAllAsync();
        return Ok(blogPosts.Select(BlogPostMapper.MapToOutputDto));
    }

    // GET: api/BlogPosts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostOutputDto>> GetBlogPost(int id)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        return BlogPostMapper.MapToOutputDto(blogPost);
    }
}