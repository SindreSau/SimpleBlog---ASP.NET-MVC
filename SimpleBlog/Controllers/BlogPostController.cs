using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Interfaces;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private const int PageSize = 4; // Number of posts per page

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        // GET: /
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalPosts = await _blogPostRepository.GetCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalPosts / PageSize);

            var blogPosts = await _blogPostRepository.GetPagedAsync(page, PageSize);

            var viewModel = new BlogPostIndexViewModel
            {
                BlogPosts = blogPosts,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
    }
}


public class BlogPostIndexViewModel
{
    public IEnumerable<BlogPost> BlogPosts { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
