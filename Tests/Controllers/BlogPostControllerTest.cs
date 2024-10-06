// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using SimpleBlog.Controllers;
// using SimpleBlog.DTOs;
// using SimpleBlog.Interfaces;
// using SimpleBlog.Models;
// using Xunit;
//
// namespace SimpleBlog.Tests.Controllers
// {
//     public class BlogPostControllerTests
//     {
//         private readonly Mock<IBlogPostRepository> _mockRepository;
//         private readonly BlogPostController _controller;
//
//         public BlogPostControllerTests()
//         {
//             _mockRepository = new Mock<IBlogPostRepository>();
//             _controller = new BlogPostController();
//         }
//
//         [Fact]
//         public async Task GetBlogPosts_ReturnsOkResult_WithListOfBlogPosts()
//         {
//             // Arrange
//             var blogPosts = new List<BlogPost>
//             {
//                 new BlogPost { BlogPostId = 1, Title = "Test Post 1", Content = "Content 1", AuthorId = "1", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now },
//                 new BlogPost { BlogPostId = 2, Title = "Test Post 2", Content = "Content 2", AuthorId = "2", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }
//             };
//             _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(blogPosts);
//
//             // Act
//             var result = await _controller.GetBlogPosts();
//
//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result.Result);
//             var returnedPosts = Assert.IsAssignableFrom<IEnumerable<BlogPostOutputDto>>(okResult.Value);
//             Assert.Equal(2, returnedPosts.Count());
//             Assert.Equal("Test Post 1", returnedPosts.First().Title);
//             Assert.Equal("Test Post 2", returnedPosts.Last().Title);
//         }
//
//         [Fact]
//         public async Task GetBlogPosts_ReturnsOkResult_WithEmptyList_WhenNoBlogPostsExist()
//         {
//             // Arrange
//             _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<BlogPost>());
//
//             // Act
//             var result = await _controller.GetBlogPosts();
//
//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result.Result);
//             var returnedPosts = Assert.IsAssignableFrom<IEnumerable<BlogPostOutputDto>>(okResult.Value);
//             Assert.Empty(returnedPosts);
//         }
//
//         [Fact]
//         public async Task GetBlogPost_ReturnsOkResult_WithBlogPost_WhenBlogPostExists()
//         {
//             // Arrange
//             var blogPost = new BlogPost { BlogPostId = 1, Title = "Test Post", Content = "Test Content", AuthorId = "1", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now };
//             _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(blogPost);
//
//             // Act
//             var result = await _controller.GetBlogPost(1);
//
//             // Assert
//             var okResult = Assert.IsType<ActionResult<BlogPostOutputDto>>(result);
//             var returnedPost = Assert.IsType<BlogPostOutputDto>(okResult.Value);
//             Assert.Equal(blogPost.BlogPostId, returnedPost.BlogPostId);
//             Assert.Equal(blogPost.Title, returnedPost.Title);
//             Assert.Equal(blogPost.Content, returnedPost.Content);
//         }
//
//         [Fact]
//         public async Task GetBlogPost_ReturnsNotFound_WhenBlogPostDoesNotExist()
//         {
//             // Arrange
//             _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((BlogPost)null);
//
//             // Act
//             var result = await _controller.GetBlogPost(1);
//
//             // Assert
//             Assert.IsType<NotFoundResult>(result.Result);
//         }
//
//         [Fact]
//         public async Task GetBlogPost_HandlesExceptionFromRepository()
//         {
//             // Arrange
//             _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));
//
//             // Act & Assert
//             await Assert.ThrowsAsync<Exception>(() => _controller.GetBlogPost(1));
//         }
//
//         [Fact]
//         public async Task GetBlogPost_ReturnsCorrectlyMappedDto()
//         {
//             // Arrange
//             var blogPost = new BlogPost
//             {
//                 BlogPostId = 1,
//                 Title = "Test Post",
//                 Content = "Test Content",
//                 AuthorId = "1",
//                 CreatedDate = new DateTime(2023, 1, 1),
//                 UpdatedDate = new DateTime(2023, 1, 2)
//             };
//             _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(blogPost);
//
//             // Act
//             var result = await _controller.GetBlogPost(1);
//
//             // Assert
//             var okResult = Assert.IsType<ActionResult<BlogPostOutputDto>>(result);
//             var dto = Assert.IsType<BlogPostOutputDto>(okResult.Value);
//             Assert.Equal(blogPost.BlogPostId, dto.BlogPostId);
//             Assert.Equal(blogPost.Title, dto.Title);
//             Assert.Equal(blogPost.Content, dto.Content);
//             Assert.Equal(blogPost.CreatedDate, dto.CreatedDate);
//             Assert.Equal(blogPost.UpdatedDate, dto.UpdatedDate);
//         }
//     }
// }