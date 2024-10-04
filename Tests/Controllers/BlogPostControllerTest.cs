using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleBlog.Controllers;
using SimpleBlog.DTOs;
using SimpleBlog.Interfaces;
using SimpleBlog.Models;

namespace Tests.Controllers;

public class BlogPostControllerTest
{
    private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
    private readonly BlogPostController _blogPostController;

    public BlogPostControllerTest()
    {
        _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        _blogPostController = new BlogPostController(_blogPostRepositoryMock.Object);
    }

    [Fact]
    public async Task GetBlogPosts_ReturnsOk()
    {
        // Arrange
        var blogPosts = new List<BlogPost>
        {
            new BlogPost { BlogPostId = 1, Title = "Title 1", Content = "Content 1", AuthorId = "1" },
            new BlogPost { BlogPostId = 2, Title = "Title 2", Content = "Content 2", AuthorId = "2" }
        };
        _blogPostRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(blogPosts);

        // Act
        var result = await _blogPostController.GetBlogPosts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var blogPostDtos = Assert.IsAssignableFrom<IEnumerable<BlogPostOutputDto>>(okResult.Value);
        Assert.Equal(blogPosts.Count, blogPostDtos.Count());
    }
}