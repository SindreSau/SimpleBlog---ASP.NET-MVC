using SimpleBlog.DTOs;
using SimpleBlog.Models;

namespace SimpleBlog.Mappers;

public class BlogPostMapper
{
    public static BlogPostOutputDto MapToOutputDto(BlogPost blogPost)
    {
        return new BlogPostOutputDto
        {
            BlogPostId = blogPost.BlogPostId,
            Title = blogPost.Title ?? string.Empty,
            Content = blogPost.Content ?? string.Empty,
            CreatedDate = blogPost.CreatedDate,
            UpdatedDate = blogPost.UpdatedDate,
            AuthorName = blogPost.Author?.UserName ?? "Unknown"
        };
    }

    public static BlogPost MapToModel(BlogPostInputDto blogPostInputDto)
    {
        return new BlogPost
        {
            Title = blogPostInputDto.Title,
            Content = blogPostInputDto.Content,
            AuthorId = blogPostInputDto.AuthorId
        };
    }
}