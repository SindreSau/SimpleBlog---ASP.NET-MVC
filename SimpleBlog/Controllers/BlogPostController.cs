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

    // POST: api/BlogPosts
    [HttpPost]
    public async Task<ActionResult<BlogPostOutputDto>> PostBlogPost(BlogPostInputDto blogPostInputDto)
    {
        var blogPost = BlogPostMapper.MapToModel(blogPostInputDto);
        await _blogPostRepository.AddAsync(blogPost);
        return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.BlogPostId }, BlogPostMapper.MapToOutputDto(blogPost));
    }

    // PUT: api/BlogPosts/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlogPost(int id, BlogPostInputDto blogPostInputDto)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        blogPost.Title = blogPostInputDto.Title;
        blogPost.Content = blogPostInputDto.Content;
        await _blogPostRepository.UpdateAsync(blogPost);
        return NoContent();
    }

    // DELETE: api/BlogPosts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        await _blogPostRepository.DeleteAsync(id);
        return NoContent();
    }
}