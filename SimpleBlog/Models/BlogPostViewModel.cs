using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models;


public class BlogPostIndexViewModel
{
    public IEnumerable<BlogPost> BlogPosts { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}

public class BlogPostCreateViewModel
{
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
}

public class BlogPostsByAuthorViewModel
{
    public string AuthorId { get; set; }
    public string AuthorName { get; set; }
    public IEnumerable<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}