using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SimpleBlog.Models;

// Model class for a blog post, use annotations to specify the table name
public class BlogPost
{
    public int BlogPostId { get; set; }
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Content is required")]
    [StringLength(400, ErrorMessage = "Content cannot be longer than 400 characters")]
    public string? Content { get; set; }

    [Display(Name = "Created Date")] // Display name can be used to change the label in the UI
    [DataType(DataType.DateTime)] // Data type can be used to specify the type of input in the UI
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Display(Name = "Updated Date")]
    [DataType(DataType.DateTime)]
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    [Display(Name = "Author")]
    public string? AuthorId { get; set; }

    // Navigation property for the author
    public virtual IdentityUser? Author { get; set; }
}