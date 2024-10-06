using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Interfaces;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        // GET: /
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostRepository.GetPagedAsync(1, 2);
            return View(blogPosts);
        }

        
    }
}