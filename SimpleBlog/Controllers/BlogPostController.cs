using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.DTOs;
using SimpleBlog.Interfaces;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class BlogPostController(
        IBlogPostRepository blogPostRepository,
        UserManager<IdentityUser> userManager,
        ILogger<BlogPostController> logger)
        : Controller
    {
        private const int PageSize = 4; // Number of posts per page

        // GET: /
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalPosts = await blogPostRepository.GetCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalPosts / PageSize);

            var blogPosts = await blogPostRepository.GetPagedAsync(page, PageSize);

            var viewModel = new BlogPostIndexViewModel
            {
                BlogPosts = blogPosts,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogPostCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            logger.LogInformation("User is authenticated: {IsAuthenticated}", User.Identity is { IsAuthenticated: true });
            logger.LogInformation("User identity name: {Name}", User.Identity?.Name);

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                logger.LogWarning("Unable to get user from UserManager");
                return RedirectToAction(nameof(Index));
            }

            logger.LogInformation("User retrieved successfully: {UserId}", user.Id);

            var blogPost = new BlogPostInputDto()
            {
                Title = model.Title,
                Content = model.Content,
                AuthorId = user.Id
            };

            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction(nameof(Index));
        }

        // Get paged posts by a specific author
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AuthorPosts(int page = 1)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var totalPosts = await blogPostRepository.GetAuthorPostCountAsync(user.Id);
            var totalPages = (int)Math.Ceiling((double)totalPosts / PageSize);

            var blogPosts = await blogPostRepository.GetPagedByAuthorIdAsync(user.Id, page, PageSize);

            if (user.UserName != null)
            {
                var viewModel = new BlogPostsByAuthorViewModel
                {
                    AuthorId = user.Id,
                    AuthorName = user.UserName,
                    BlogPosts = blogPosts,
                    CurrentPage = page,
                    TotalPages = totalPages
                };

                return View(viewModel);
            }

            return NotFound("User with UserName not found");
        }
    }
}


